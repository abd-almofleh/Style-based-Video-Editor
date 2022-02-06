import sys
import time
import os
import tqdm
import torch
import argparse
import glob
import subprocess
import warnings
import cv2
import pickle
import numpy
import pdb
import math
import python_speech_features

from pathlib import Path
from scipy import signal
from shutil import rmtree
from scipy.io import wavfile
from scipy.interpolate import interp1d
from sklearn.metrics import accuracy_score, f1_score

from scenedetect.video_manager import VideoManager
from scenedetect.scene_manager import SceneManager
from scenedetect.frame_timecode import FrameTimecode
from scenedetect.stats_manager import StatsManager
from scenedetect.detectors import ContentDetector

from TalkNet.faceDetector.s3fd import S3FD
from TalkNet.talkNet import talkNet


class SpeakerVisibilityDetection:
  pretrainModel = "./weights/pretrain_TalkSet.model"
  nDataLoaderThread=10
  facedetScale = 0.25,
  minTrack=10
  numFailedDet=10
  minFaceSize=1
  cropScale=0.40
  def __init__(self, videoPath) -> None:
    video = Path(videoPath)
    self.videoName = video.stem
    self.videoFolder = str(video.parent.absolute().resolve())
    self.videoPath = glob.glob(os.path.join(self.videoFolder, self.videoName + '.*'))[0]
    self.savePath = os.path.join(self.videoFolder, self.videoName)

  # CPU: Scene detection, output is the list of each shot's time duration
  def _scene_detect(self):
    videoManager = VideoManager([self.videoFilePath])
    statsManager = StatsManager()
    sceneManager = SceneManager(statsManager)
    sceneManager.add_detector(ContentDetector())
    baseTimecode = videoManager.get_base_timecode()
    videoManager.set_downscale_factor()
    videoManager.start()
    sceneManager.detect_scenes(frame_source=videoManager)
    sceneList = sceneManager.get_scene_list(baseTimecode)
    if sceneList == []:
      sceneList = [(videoManager.get_base_timecode(),
                    videoManager.get_current_timecode())]
    return sceneList
  
  def _inference_video(self):
    # GPU: Face detection, output is the list contains the face location and score in this frame
    DET = S3FD(device='cpu')
    flist = glob.glob(os.path.join(self.pyframesPath, '*.jpg'))
    flist.sort()
    dets = []
    for fidx, fname in enumerate(flist):
      image = cv2.imread(fname)
      imageNumpy = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
      bboxes = DET.detect_faces(
          imageNumpy, conf_th=0.9, scales=[SpeakerVisibilityDetection.facedetScale])
      dets.append([])
      for bbox in bboxes:
        # dets has the frames info, bbox info, conf info
        dets[-1].append({'frame': fidx, 'bbox': (bbox[:-1]).tolist(), 'conf': bbox[-1]})
    return dets
  
  @staticmethod
  def _bb_intersection_over_union(boxA, boxB, evalCol=False):
    # CPU: IOU Function to calculate overlap between two image
    xA = max(boxA[0], boxB[0])
    yA = max(boxA[1], boxB[1])
    xB = min(boxA[2], boxB[2])
    yB = min(boxA[3], boxB[3])
    interArea = max(0, xB - xA) * max(0, yB - yA)
    boxAArea = (boxA[2] - boxA[0]) * (boxA[3] - boxA[1])
    boxBArea = (boxB[2] - boxB[0]) * (boxB[3] - boxB[1])
    if evalCol == True:
      iou = interArea / float(boxAArea)
    else:
      iou = interArea / float(boxAArea + boxBArea - interArea)
    return iou
  
  @staticmethod
  def _track_shot(sceneFaces):
    # CPU: Face tracking
    iouThres = 0.5     # Minimum IOU between consecutive face detections
    tracks = []
    while True:
      track = []
      for frameFaces in sceneFaces:
        for face in frameFaces:
          if track == []:
            track.append(face)
            frameFaces.remove(face)
          elif face['frame'] - track[-1]['frame'] <= SpeakerVisibilityDetection.numFailedDet:
            iou = SpeakerVisibilityDetection.bb_intersection_over_union(
                face['bbox'], track[-1]['bbox'])
            if iou > iouThres:
              track.append(face)
              frameFaces.remove(face)
              continue
          else:
            break
      if track == []:
        break
      elif len(track) > SpeakerVisibilityDetection.minTrack:
        frameNum = numpy.array([f['frame'] for f in track])
        bboxes = numpy.array([numpy.array(f['bbox']) for f in track])
        frameI = numpy.arange(frameNum[0], frameNum[-1] + 1)
        bboxesI = []
        for ij in range(0, 4):
          interpfn = interp1d(frameNum, bboxes[:, ij])
          bboxesI.append(interpfn(frameI))
        bboxesI = numpy.stack(bboxesI, axis=1)
        if max(numpy.mean(bboxesI[:, 2] - bboxesI[:, 0]), numpy.mean(bboxesI[:, 3] - bboxesI[:, 1])) > SpeakerVisibilityDetection.minFaceSize:
          tracks.append({'frame': frameI, 'bbox': bboxesI})
    return tracks

  def _crop_video(self, track, cropFile):
    # CPU: crop the face clips
    flist = glob.glob(os.path.join(self.pyframesPath, '*.jpg'))  # Read the frames
    flist.sort()
    vOut = cv2.VideoWriter(cropFile + 't.avi', cv2.VideoWriter_fourcc(*'XVID'), 25, (224, 224))  # Write video
    dets = {'x': [], 'y': [], 's': []}
    for det in track['bbox']:  # Read the tracks
      dets['s'].append(max((det[3] - det[1]), (det[2] - det[0])) / 2)
      dets['y'].append((det[1] + det[3]) / 2)  # crop center x
      dets['x'].append((det[0] + det[2]) / 2)  # crop center y
    dets['s'] = signal.medfilt(dets['s'], kernel_size=13)  # Smooth detections
    dets['x'] = signal.medfilt(dets['x'], kernel_size=13)
    dets['y'] = signal.medfilt(dets['y'], kernel_size=13)
    for fidx, frame in enumerate(track['frame']):
      cs = SpeakerVisibilityDetection.cropScale
      bs = dets['s'][fidx]   # Detection box size
      bsi = int(bs * (1 + 2 * cs))  # Pad videos by this amount
      image = cv2.imread(flist[frame])
      frame = numpy.pad(image, ((bsi, bsi), (bsi, bsi), (0, 0)),
                        'constant', constant_values=(110, 110))
      my = dets['y'][fidx] + bsi  # BBox center Y
      mx = dets['x'][fidx] + bsi  # BBox center X
      face = frame[int(my - bs):int(my + bs * (1 + 2 * cs)),
                  int(mx - bs * (1 + cs)):int(mx + bs * (1 + cs))]
      vOut.write(cv2.resize(face, (224, 224)))
    audioTmp = cropFile + '.wav'
    audioStart = (track['frame'][0]) / 25
    audioEnd = (track['frame'][-1] + 1) / 25
    vOut.release()
    command = ("ffmpeg -y -i \"%s\" -async 1 -ac 1 -vn -acodec pcm_s16le -ar 16000 -threads %d -ss %.3f -to %.3f \"%s\" -loglevel panic" %
              (self.audioFilePath, SpeakerVisibilityDetection.nDataLoaderThread, audioStart, audioEnd, audioTmp))
    output = subprocess.call(
        command, shell=True, stdout=None)  # Crop audio file
    _, audio = wavfile.read(audioTmp)
    command = ("ffmpeg -y -i \"%st.avi\" -i \"%s\" -threads %d -c:v copy -c:a copy \"%s.avi\" -loglevel panic" %
              (cropFile, audioTmp, SpeakerVisibilityDetection.nDataLoaderThread, cropFile))  # Combine audio and video file
    output = subprocess.call(command, shell=True, stdout=None)
    os.remove(cropFile + 't.avi')
    return {'track': track, 'proc_track': dets}

  @staticmethod
  def _extract_MFCC(file, outPath):
    # CPU: extract mfcc
    sr, audio = wavfile.read(file)
    # (N_frames, 13)   [1s = 100 frames]
    mfcc = python_speech_features.mfcc(audio, sr)
    featuresPath = os.path.join(
        outPath, file.split('/')[-1].replace('.wav', '.npy'))
    numpy.save(featuresPath, mfcc)

  def _evaluate_network(self, files):
    # GPU: active speaker detection by pretrained TalkNet
    s = talkNet()
    s.loadParameters(SpeakerVisibilityDetection.pretrainModel)

    s.eval()
    allScores = []
    # durationSet = {1,2,4,6} # To make the result more reliable
    # Use this line can get more reliable result
    durationSet = {1, 1, 1, 2, 2, 2, 3, 3, 4, 5, 6}
    for file in tqdm.tqdm(files, total=len(files)):
      fileName = os.path.splitext(file.split(
          '/')[-1])[0]  # Load audio and video
      _, audio = wavfile.read(os.path.join(self.pycropPath, fileName + '.wav'))
      audioFeature = python_speech_features.mfcc(
          audio, 16000, numcep=13, winlen=0.025, winstep=0.010)
      video = cv2.VideoCapture(os.path.join(self.pycropPath, fileName + '.avi'))
      videoFeature = []
      while video.isOpened():
        ret, frames = video.read()
        if ret == True:
          face = cv2.cvtColor(frames, cv2.COLOR_BGR2GRAY)
          face = cv2.resize(face, (224, 224))
          face = face[int(112 - (112 / 2)):int(112 + (112 / 2)),
                      int(112 - (112 / 2)):int(112 + (112 / 2))]
          videoFeature.append(face)
        else:
          break
      video.release()
      videoFeature = numpy.array(videoFeature)
      length = min(
          (audioFeature.shape[0] - audioFeature.shape[0] % 4) / 100, videoFeature.shape[0])
      audioFeature = audioFeature[:int(round(length * 100)), :]
      videoFeature = videoFeature[:int(round(length * 25)), :, :]
      allScore = []  # Evaluation use TalkNet
      for duration in durationSet:
        batchSize = int(math.ceil(length / duration))
        scores = []
        with torch.no_grad():
          for i in range(batchSize):
            inputA = torch.FloatTensor(
                audioFeature[i * duration * 100:(i + 1) * duration * 100, :]).unsqueeze(0).cpu()
            inputV = torch.FloatTensor(
                videoFeature[i * duration * 25: (i + 1) * duration * 25, :, :]).unsqueeze(0).cpu()
            embedA = s.model.forward_audio_frontend(inputA)
            embedV = s.model.forward_visual_frontend(inputV)
            embedA, embedV = s.model.forward_cross_attention(
                embedA, embedV)
            out = s.model.forward_audio_visual_backend(embedA, embedV)
            score = s.lossAV.forward(out, labels=None)
            scores.extend(score)
        allScore.append(scores)
      allScore = numpy.round(
          (numpy.mean(numpy.array(allScore), axis=0)), 1).astype(float)
      allScores.append(allScore)
    return allScores

  def Detect(self):
    self.pyaviPath = os.path.join(self.savePath, 'pyavi')
    self.pyframesPath = os.path.join(self.savePath, 'pyframes')
    self.pyworkPath = os.path.join(self.savePath, 'pywork')
    self.pycropPath = os.path.join(self.savePath, 'pycrop')
    if os.path.exists(self.savePath):
      rmtree(self.savePath)
    # The path for the input video, input audio, output video
    os.makedirs(self.pyaviPath, exist_ok=True)
    os.makedirs(self.pyframesPath, exist_ok=True)  # Save all the video frames
    # Save the results in this process by the pckl method
    os.makedirs(self.pyworkPath, exist_ok=True)
    # Save the detected face clips (audio+video) in this process
    os.makedirs(self.pycropPath, exist_ok=True)

    # Extract video
    self.videoFilePath = os.path.join(self.pyaviPath, 'video.avi')
    command = f"ffmpeg -y -i \"{self.videoPath}\" -qscale:v 2 -threads {SpeakerVisibilityDetection.nDataLoaderThread} -async 1 -r 25 \"{self.videoFilePath}\" -loglevel panic"
    subprocess.call(command, shell=True, stdout=None)

    # Extract audio
    self.audioFilePath = os.path.join(self.pyaviPath, 'audio.wav')
    command = f"ffmpeg -y -i \"{self.videoFilePath}\" -qscale:a 0 -ac 1 -vn -threads {SpeakerVisibilityDetection.nDataLoaderThread} -ar 16000 \"{self.audioFilePath}\" -loglevel panic"
    subprocess.call(command, shell=True, stdout=None)

    # Extract the video frames
    command = f"ffmpeg -y -i \"{self.videoFilePath}\" -qscale:v 2 -threads{SpeakerVisibilityDetection.nDataLoaderThread} -f image2 \"{self.pyframesPath}\" -loglevel panic" 
    subprocess.call(command, shell=True, stdout=None)

    # Scene detection for the video frames
    scene = self._scene_detect()
    
    # Face detection for the video frames
    faces = self.inference_video()

    # Face tracking
    allTracks = []
    for shot in scene:
      # Discard the shot frames less than minTrack frames
      if shot[1].frame_num - shot[0].frame_num >= SpeakerVisibilityDetection.minTrack:
        # 'frames' to present this tracks' timestep, 'bbox' presents the location of the faces
        allTracks.extend(SpeakerVisibilityDetection.track_shot(faces[shot[0].frame_num:shot[1].frame_num]))
    # Face clips cropping
    vidTracks = []
    for ii, track in enumerate(allTracks):
      vidTracks.append(self.crop_video( track, os.path.join(self.pycropPath, '%05d' % ii)))

    # Active Speaker Detection by TalkNet
    files = glob.glob("%s/*.avi" % self.pycropPath)
    files.sort()
    scores = self._evaluate_network(files)

    flist = glob.glob(os.path.join(self.pyframesPath, '*.jpg'))
    flist.sort()
    faces = [[] for i in range(len(flist))]
    for tidx, track in enumerate(vidTracks):
      score = scores[tidx]
      for fidx, frame in enumerate(track['track']['frame'].tolist()):
        # average smoothing
        s = score[max(fidx - 2, 0): min(fidx + 3, len(score) - 1)]
        s = numpy.mean(s)
        faces[frame].append({'track': tidx, 'score': float(s), 's': track['proc_track']['s']
                            [fidx], 'x': track['proc_track']['x'][fidx], 'y': track['proc_track']['y'][fidx]})
    less , more = 0 ,0 
    final_scores = []
    for face in faces:
      if len(face) > 0:
        final_scores.append(face[0]["score"])
        if face[0]["score"] > 0:
          more += 1
        else:
          less += 1
    score = max(final_scores)
    return {"speaking_frames_count": more,"silent_frames_count": less,"score": score}
    


        


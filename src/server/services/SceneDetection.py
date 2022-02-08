from pathlib import Path
from .SpeakerDiarisation import SpeakerDiarisation
from helpers.helper import cut_video, get_frame_form_video, extract_wav_from_video
from .SpeechToText import SpeechToText
from threading import Thread
from .SpeakerVisibilityDetection import SpeakerVisibilityDetection
from .RetinaFace import RetinaFace
import cv2
import os
import uuid


class SceneDetection:

  def __init__(self) -> None:
    pass

  @staticmethod
  def extract_scenes(videos_paths):
    scenes_times = SpeakerDiarisation.speaker_change_detection(videos_paths[0])

    scenes = dict()
    threads = []

    for i, video_path in enumerate(videos_paths):
      video = Path(video_path)
      video_name = video.stem
      scenes[video_name] = [None] * len(scenes_times)
      for i, time in enumerate(scenes_times):
        x = Thread(target=SceneDetection.process_scene,
                   args=(i, scenes, time, video_name, video_path), daemon=True)
        threads.append(x)
        x.start()

    recognizer = SpeechToText()
    result = recognizer.arabic_speech_recognition(videos_paths[0])
    result = SpeechToText.extract_scenes_text(result, scenes_times)
    scenes["scripts"] = result
    for thread in threads:
      thread.join()
    return scenes

  @staticmethod
  def process_scene(scene_num, scenes, time, video_name, video_path):
    scene_info = dict(time)
    scene_path = cut_video(video_path, time["start_time"], time["end_time"])
    scene_info["path"] = str(scene_path.absolute().resolve())
    if time["speaker"] != -1:
      speaker_visible = SpeakerVisibilityDetection(scene_info["path"])
      scene_info.update(speaker_visible.Detect())
    else:
      scene_info["visible_speaker"] = -1
      scene_info["image"] = get_frame_form_video(scene_path, time["length"] / 2)
      scene_info["faces"] = RetinaFace.ExtractFaces(scene_info["image"])
      for i, face in enumerate(scene_info["faces"]):
        scene_info["faces"][i]["bbox"] = face["location_info"]["facial_area"]
        del scene_info["faces"][i]["location_info"]
        p = Path("./temp/thumbnails").joinpath("face[" + str(uuid.uuid4()).replace("-", "") + "].jpg")
        scene_info["faces"][i]["path"] = str(p.absolute().resolve())

    scenes[video_name][scene_num] = scene_info
    image = cv2.imread(scene_info["image"])
    for face in scene_info["faces"]:
      cv2.imwrite(face["path"],
                  image[
          face["bbox"][1]:face["bbox"][3],
          face["bbox"][0]:face["bbox"][2]])

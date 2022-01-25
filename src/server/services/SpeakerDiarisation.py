from resemblyzer import VoiceEncoder
from pathlib import Path
import librosa
from spectralcluster import SpectralClusterer, RefinementOptions
from resemblyzer.audio import sampling_rate
from helpers.helper import extract_wav_from_video
import os
from malaya_speech import Pipeline
import malaya_speech
import numpy as np
import matplotlib.pyplot as plt


class SpeakerDiarisation:
    cut_rate = 5

    def __init__(self) -> None:
        pass

    @staticmethod
    def speaker_change_detection(video_path):
        model_speakernet = malaya_speech.speaker_vector.deep_model(
            'speakernet', validate=False)

        y, sr = malaya_speech.load(
            '../../audio/conversations/2_speakers.en.wav')
        vad = malaya_speech.vad.deep_model(model='vggvox-v2', validate=False)
        frames = list(malaya_speech.utils.generator.frames(y, 30, sr))
        p = Pipeline()
        pipeline = (p.batching(5).foreach_map(vad.predict).flatten())
        result = p.emit(frames)
        frames_vad = [(frame, result['flatten'][no])
                      for no, frame in enumerate(frames)]
        grouped_vad = malaya_speech.utils.group.group_frames(frames_vad)
        grouped_vad = malaya_speech.utils.group.group_frames_threshold(
            grouped_vad, threshold_to_stop=0.3)
        result = malaya_speech.diarization.spectral_cluster(grouped_vad, model_speakernet,
                                                            min_clusters=2,
                                                            max_clusters=3)
        grouped = malaya_speech.group.group_frames(result)
        frames = []
        for frame in grouped:
            start_time = round(frame[0].timestamp, 2)
            end_time = round(start_time + frame[0].duration, 2)
            speaker = -1 if frame[1] == "not a speaker" else frame[1].replace(
                "speaker ", "")
            frames.append({
                "speaker": speaker,
                "start_time": start_time,
                "end_time": end_time
            })

        return frames

    @staticmethod
    def resemblyzer_speaker_change_detection(video_path):
        wav_file = extract_wav_from_video(video_path)
        wav, _ = librosa.load(wav_file, sr=16000)
        encoder = VoiceEncoder("cpu", verbose=False)
        _, cont_embeds, wav_splits = encoder.embed_utterance(
            wav, min_coverage=1, return_partials=True, rate=SpeakerDiarisation.cut_rate)

        refinement_options = RefinementOptions(
            gaussian_blur_sigma=1, p_percentile=0.90,)
        clusterer = SpectralClusterer(
            min_clusters=2, max_clusters=2, refinement_options=refinement_options)

        labels = clusterer.predict(cont_embeds)
        times = [((s.start + s.stop) / 2) / sampling_rate for s in wav_splits]
        count = 0
        labelling = []
        start_time = 0
        for i, time in enumerate(times):
            if i > 0 and labels[i] != labels[i-1]:
                temp = [str(labels[i-1]), start_time, time]
                labelling.append(tuple(temp))
                start_time = time
                count = 0
            else:
                count += 1

            if i == len(times)-1:
                temp = [str(labels[i]), start_time, time]
                labelling.append(tuple(temp))

        os.unlink(str(wav_file.absolute().resolve()))
        return labelling

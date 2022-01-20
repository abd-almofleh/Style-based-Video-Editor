from resemblyzer import VoiceEncoder
from pathlib import Path
import librosa
from spectralcluster import SpectralClusterer, RefinementOptions
from resemblyzer.audio import sampling_rate
from helpers.helper import extract_wav_from_video
import os


class SpeakerDiarisation:
    cut_rate = 5

    def __init__(self) -> None:
        pass

    @staticmethod
    def speaker_change_detection(video_path):
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
        labelling = SpeakerDiarisation._create_labelling(labels, wav_splits)
        os.unlink(str(wav_file.absolute().resolve()))
        return labelling

    @staticmethod
    def _create_labelling(labels, wav_splits):

        times = [((s.start + s.stop) / 2) / sampling_rate for s in wav_splits]
        count = 0

        # for i,time in enumerate(labels):
        #     if i>0 and labels[i]!=labels[i-1]:
        #         if count <= (cut_rate - cut_rate*0.2):
        #             for index in range(count+1):
        #                 labels[i-index-1] = labels[i]
        #         count = 0
        #         continue
        #     count += 1
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

        return labelling

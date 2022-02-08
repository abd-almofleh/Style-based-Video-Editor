import subprocess
import os
import malaya_speech
import numpy as np
from malaya_speech import Pipeline

class VoiceEmotion():

    deep_speaker = malaya_speech.emotion.deep_model(model ="deep-speaker",validate = False)
    vad = malaya_speech.vad.deep_model(model = 'vggvox-v2')

    def __init__(self) -> None:
        
        self.deep_speaker = malaya_speech.emotion.deep_model(model ="deep-speaker",validate = False)
        self.vad = malaya_speech.vad.deep_model(model = 'vggvox-v2')        


    def predict(self,audio_path):

        y, sr = malaya_speech.load(audio_path)

        frames = list(malaya_speech.utils.generator.frames(y, 30,sr))
        p = Pipeline()
        pipeline = (
            p.batching(5)
            .foreach_map(self.vad.predict)
            .flatten()
        )
        result = p.emit(frames)
        result.keys()
        frames_vad = [(frame, result['flatten'][no]) for no, frame in enumerate(frames)]
        grouped_vad = malaya_speech.utils.group.group_frames(frames_vad)
        grouped_vad = malaya_speech.utils.group.group_frames_threshold(grouped_vad, threshold_to_stop = 0.3)
        p_deep_speaker = Pipeline()
        pipeline = (
            p_deep_speaker.foreach_map(self.deep_speaker)
            .flatten()
        )
        samples_vad = [g[0] for g in grouped_vad]
        result_deep_speaker = p_deep_speaker.emit(samples_vad)
        result_deep_speaker.keys()
        samples_vad_deep_speaker = [(frame, result_deep_speaker['flatten'][no]) for no, frame in enumerate(samples_vad)]
        samples_vad_deep_speaker
        return samples_vad_deep_speaker
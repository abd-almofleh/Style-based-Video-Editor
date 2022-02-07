from pathlib import Path
from .SpeakerDiarisation import SpeakerDiarisation
from helpers.helper import cut_video, get_frame_form_video, extract_wav_from_video
from .SpeechToText import SpeechToText
from threading import Thread
# from .SpeakerVisibilityDetection import SpeakerVisibilityDetection

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
        scene_path = cut_video(
            video_path, time["start_time"], time["end_time"])
        scene_info["image"] = get_frame_form_video(
            scene_path, time["length"]/2)
        scene_info["path"] = str(scene_path.absolute().resolve())
        # speaker_visible = SpeakerVisibilityDetection(scene_info["path"])
        # scene_info["speaker"] = speaker_visible.
        scenes[video_name][scene_num] = scene_info

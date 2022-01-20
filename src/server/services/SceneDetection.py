from pathlib import Path
from .SpeakerDiarisation import SpeakerDiarisation
from helpers.helper import cut_video


class SceneDetection:

    def __init__(self) -> None:
        pass

    def extract_scenes(videos_paths):
        scenes_times = SpeakerDiarisation.speaker_change_detection(
            videos_paths[0])
        scenes = dict()
        for i, video_path in enumerate(videos_paths):
            video = Path(video_path)
            video_name = video.stem
            scenes[video_name] = []
            for i, time in enumerate(scenes_times):
                scene_path = cut_video(video_path, time[1], time[2])
                scenes[video_name].append(str(scene_path.absolute().resolve()))
        return scenes

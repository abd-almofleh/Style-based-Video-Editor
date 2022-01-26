from pathlib import Path
from .SpeakerDiarisation import SpeakerDiarisation
from helpers.helper import cut_video, get_frame_form_video


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
                scene_info = dict(time)
                scene_path = cut_video(
                    video_path, time["start_time"], time["end_time"])
                scene_info["thumbnail"] = get_frame_form_video(
                    scene_path, time["length"]/2)
                scene_info["path"] = str(scene_path.absolute().resolve())
                scenes[video_name].append(scene_info)
        return scenes

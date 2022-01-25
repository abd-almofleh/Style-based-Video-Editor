from distutils import command
from pathlib import Path
import subprocess
from datetime import datetime
import uuid


def is_allowed_file(filename, allowed_extensions):
    return '.' in filename and filename.rsplit('.', 1)[1].lower() in allowed_extensions


def extract_wav_from_video(video_path, output_folder_path="./uploads/audios"):
    video = Path(video_path)
    output_folder = Path(output_folder_path).joinpath(video.stem)
    output_folder.mkdir(exist_ok=True)
    output_file = Path.joinpath(
        output_folder, video.stem+datetime.now().strftime("_D[%d-%m-%Y]_T[%H-%M-%S]")+".wav")
    paths = {
        "video_path": str(video.absolute().resolve()),
        "wav_path": str(output_file.absolute().resolve())
    }
    command = 'ffmpeg -i "%(video_path)s" -loglevel quiet -ab 160k -ac 2 -ar 44100 -vn "%(wav_path)s"' % paths
    subprocess.call(command, shell=True)
    return output_file


def cut_video(video_path, start_time, end_time, output_folder_path="./uploads/scenes"):
    video = Path(video_path)
    output_folder = Path(output_folder_path)
    output_file = Path.joinpath(
        output_folder, video.stem+"["+str(uuid.uuid4())+"].mp4")
    paths = {
        "video_path": str(video.absolute().resolve()),
        "cuted_video_path": str(output_file.absolute().resolve()),
        "start_time": start_time,
        "end_time": end_time

    }
    command = 'ffmpeg -i "%(video_path)s" -ss %(start_time)s -loglevel quiet -c copy -to %(end_time)s "%(cuted_video_path)s"' % paths
    subprocess.call(command, shell=True)
    return output_file

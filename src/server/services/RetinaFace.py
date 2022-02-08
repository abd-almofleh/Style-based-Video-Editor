from retinaface import RetinaFace as RF
from retinaface.commons import postprocess
import cv2
from pathlib import Path
from helpers.constant import FACES_PATH
import numpy as np


class RetinaFace:
  def __init__(self) -> None:
    pass

  @staticmethod
  def ExtractFaces(image_path):
    image_path = Path(image_path)
    faces = RetinaFace.DetectFaces(str(image_path.absolute().resolve()))
    root = Path(FACES_PATH)
    counter = 1

    for face in faces:
      image = face["image"]
      path = str((root / f"{image_path.stem}-{counter}.jpg").absolute().resolve())
      counter += 1

      cv2.imwrite(path, image)
      face["path"] = path
      del face["image"]
    return faces

  def DetectFaces(image_path):
    img = cv2.imread(image_path)

    faces = RF.detect_faces(image_path)
    for key in faces:
      identity = faces[key]

      facial_area = identity["facial_area"]
      facial_img = img[facial_area[1]: facial_area[3],
                       facial_area[0]: facial_area[2]]

      landmarks = identity["landmarks"]
      left_eye = landmarks["left_eye"]
      right_eye = landmarks["right_eye"]

      nose = landmarks["nose"]
      facial_img = postprocess.alignment_procedure(
          facial_img, right_eye, left_eye, nose)
      identity["facial_area"] = np.array(
          identity["facial_area"], int).tolist()
      for landmark_key in identity["landmarks"]:
        landmarks = identity["landmarks"]
        landmarks[landmark_key] = np.array(
            landmarks[landmark_key], float).tolist()
      score = identity["score"]
      del identity["score"]
      faces[key] = {
          "score": score,
          "location_info": identity,
          "image": facial_img
      }

    return list(faces.values())

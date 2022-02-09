from unittest import result
import cv2
import dlib
from pathlib import Path
from helpers.constant import FACES_PATH
import numpy as np
from deepface import DeepFace as DF


class DeepFace:
  models = ["VGG-Face", "Facenet", "Facenet512",
            "OpenFace", "DeepFace", "DeepID", "ArcFace", "Dlib"]
  metrics = ["cosine", "euclidean", "euclidean_l2"]
  backends = ['opencv', 'ssd', 'dlib', 'mtcnn', 'retinaface']

  def __init__(self) -> None:
    pass

  @staticmethod
  def AnalyzeFace(image, actions=['emotion', "age", "gender"]):
    result = DF.analyze(img_path=image, actions=actions,
                        detector_backend=DeepFace.backends[2], prog_bar=False, enforce_detection=False)
    return result

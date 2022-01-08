from os import pathsep
import cv2
import dlib
from flask import app, jsonify, request, Response
from flask_restful import Resource
from tensorflow.python.ops.gen_math_ops import imag
from helpers.constant import ALLOWED_UPLOAD_EXTENSIONS, UPLOAD_PATH, COCO_CLASSES
from helpers.helper import is_allowed_file
from helpers.errors import file_is_reqired, extention_allowed_only
from werkzeug.utils import secure_filename

from pathlib import Path


class Face:
    def __init__(self) -> None:
        pass

    @staticmethod
    def convert_and_trim_bb(image, rect):

        startX = rect.left()
        startY = rect.top()
        endX = rect.right()
        endY = rect.bottom()

        startX = max(0, startX)
        startY = max(0, startY)
        endX = min(endX, image.shape[1])
        endY = min(endY, image.shape[0])
        w = endX - startX
        h = endY - startY
        return (startX, startY, w, h)

    @staticmethod
    def detect_faces_and_landmarks(image, cnn=False):
        PREDICTOR_PATH = str(Path(__file__).parent.parent.joinpath(
            "weights/shape_predictor_68_face_landmarks.dat").resolve())
        DETECTOR_PATH = str(Path(__file__).parent.parent.joinpath(
            "weights/mmod_human_face_detector.dat").resolve())

        Detector = dlib.get_frontal_face_detector(
        ) if not cnn else dlib.cnn_face_detection_model_v1(DETECTOR_PATH)

        landmarkDetector = dlib.shape_predictor(PREDICTOR_PATH)

        faceRects = Detector(image, 1)

        landmarksAll = []

        for i in range(0, len(faceRects)):
            newRect = dlib.rectangle(int(faceRects[i].left()),
                                     int(faceRects[i].top()),
                                     int(faceRects[i].right()),
                                     int(faceRects[i].bottom()))
            landmarks = landmarkDetector(image, newRect)
            landmarksAll.append(landmarks)
        boxes = [Face.convert_and_trim_bb(image, r) for r in faceRects]
        return landmarksAll, boxes

    @staticmethod
    def saveFaces(image, boxes, name=""):
        counter = 1
        paths = []
        root = Path("./src/server/images/faces")
        for (x, y, w, h) in boxes:
            crop = image[y:y+h, x:x+w]
            endIndex = name.find(".jpg")
            path = (
                root / f"{name[:endIndex]}-{counter}.jpg").absolute().resolve()
            cv2.imwrite(str(path), crop)
            paths.append(str(path))
            counter += 1
        return paths

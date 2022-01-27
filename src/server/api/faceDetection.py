import numpy
from services.RetinaFace import RetinaFace
from services.DLib import DLib
from flask import app, jsonify, request, Response
from flask_restful import Resource
from helpers.constant import ALLOWED_UPLOAD_EXTENSIONS, UPLOAD_PATH, COCO_CLASSES
from helpers.helper import is_allowed_file
from helpers.errors import file_is_reqired, extention_allowed_only
from werkzeug.utils import secure_filename
from datetime import datetime
from services.DeepFace import DeepFace
import pathlib
import cv2


class FaceDetectionApi(Resource):
    @staticmethod
    def post() -> Response:
        if 'image' not in request.files:
            return file_is_reqired()
        image = request.files['image']
        if image.filename == '':
            return file_is_reqired()
        if not is_allowed_file(image.filename, ALLOWED_UPLOAD_EXTENSIONS):
            return extention_allowed_only(ALLOWED_UPLOAD_EXTENSIONS)

        imagename = secure_filename(image.filename)
        imagePath = UPLOAD_PATH + \
            datetime.now().strftime("%d-%m-%Y [%H-%M-%S]") + imagename
        image.save(imagePath)
        faces = RetinaFace.ExtractFaces(imagePath)
        for face in faces:
            analysis = DeepFace.AnalyzeFace(face["path"])
            face["age"] = analysis["age"]
            face["gender"] = analysis["gender"]
            face["dominant_emotion"] = analysis["dominant_emotion"]
            face["emotion"] = list(analysis["emotion"].items())

        return jsonify({'result': faces})

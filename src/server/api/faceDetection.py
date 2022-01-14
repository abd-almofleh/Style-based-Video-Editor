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
import pathlib
import os
import cv2
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'


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
        print(imagePath)
        faces = RetinaFace.ExtractFaces(imagePath)

        return jsonify({'result': faces})

    def get(self):
        output = request.get_json()

        return jsonify({'result': output})

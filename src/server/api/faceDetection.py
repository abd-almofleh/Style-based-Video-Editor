from services.face import Face
from flask import app, jsonify, request, Response
from flask_restful import Resource
from helpers.constant import ALLOWED_UPLOAD_EXTENSIONS, UPLOAD_PATH, COCO_CLASSES
from helpers.helper import is_allowed_file
from helpers.errors import file_is_reqired, extention_allowed_only
from werkzeug.utils import secure_filename

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
        imagePath = UPLOAD_PATH + imagename
        image.save(imagePath)
        image = cv2.imread(imagePath)
        image = cv2.resize(image, (600, 600))
        rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)

        landmarks, rec = Face.detect_faces_and_landmarks(rgb)
        paths = Face.saveFaces(image, rec, imagename)

        return jsonify({'result': paths})

    def get(self):
        output = request.get_json()

        return jsonify({'result': output})

from flask import jsonify, request, Response
from flask_restful import Resource
from helpers.constant import ALLOWED_UPLOAD_EXTENSIONS, UPLOAD_PATH
from helpers.helper import is_allowed_file
from helpers.errors import file_is_reqired, extention_allowed_only
from werkzeug.utils import secure_filename
from services.YOLOv4 import YOLOv4


class ObjectDetectionApi(Resource):
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
        result = YOLOv4.detect_objects(imagePath)
        return jsonify({'result': result})

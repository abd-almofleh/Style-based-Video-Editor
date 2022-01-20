from flask import app, jsonify, request, Response
from flask_restful import Resource
from helpers.constant import ALLOWED_UPLOAD_EXTENSIONS, UPLOAD_PATH, COCO_CLASSES
from helpers.helper import is_allowed_file
from helpers.errors import file_is_reqired, extention_allowed_only
from werkzeug.utils import secure_filename
import tensorflow as tf
from tf2_yolov4.anchors import YOLOV4_ANCHORS
from tf2_yolov4.model import YOLOv4
import pathlib


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

        weights_path = str(pathlib.Path(__file__).parent.parent.joinpath(
            "weights/yolov4.h5").resolve())
        HEIGHT, WIDTH = (640, 960)
        image = tf.io.read_file(imagePath)
        image = tf.image.decode_image(image)
        image = tf.image.resize(image, (HEIGHT, WIDTH))
        images = tf.expand_dims(image, axis=0) / 255.0
        model = YOLOv4(
            input_shape=(HEIGHT, WIDTH, 3),
            anchors=YOLOV4_ANCHORS,
            num_classes=80,
            training=False,
            yolo_max_boxes=100,
            yolo_iou_threshold=0.5,
            yolo_score_threshold=0.5,
        )

        model.load_weights(weights_path)
        boxes, scores, classes, valid_detections = model.predict(images)
        # return jsonify({'result': classes[0].tolist()})
        result = {}
        for score, cl in zip(scores[0].tolist(), classes[0].astype(int).tolist()):
            if score > 0:
                if COCO_CLASSES[cl] not in result:
                    result[COCO_CLASSES[cl]] = score
                else:
                    classTag = COCO_CLASSES[cl]
                    oldScore = result[classTag]
                    if oldScore < score:
                        result[classTag].update(oldScore)

        return jsonify({'result': result})

    def get(self):
        output = request.get_json()

        return jsonify({'result': output})

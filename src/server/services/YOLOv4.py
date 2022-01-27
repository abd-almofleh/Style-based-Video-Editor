from pathlib import Path
from helpers.constant import COCO_CLASSES

import tensorflow as tf
from tf2_yolov4.model import YOLOv4 as yolov4
from tf2_yolov4.anchors import YOLOV4_ANCHORS
import numpy as np


class YOLOv4:
    def __init__(self) -> None:
        pass

    @staticmethod
    def detect_objects(imagePath):
        weights_path = str(Path("./weights/yolov4.h5").absolute().resolve())
        HEIGHT, WIDTH = (640, 960)
        image = tf.io.read_file(imagePath)
        image = tf.image.decode_image(image)
        image = tf.image.resize(image, (HEIGHT, WIDTH))
        images = tf.expand_dims(image, axis=0) / 255.0
        model = yolov4(
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
        result = {}
        for score, cl in zip(scores[0].tolist(), classes[0].astype(int).tolist()):
            if score > 0:
                if COCO_CLASSES[cl] not in result:
                    result[COCO_CLASSES[cl]] = round(score, 2)
                else:
                    classTag = COCO_CLASSES[cl]
                    old_score = result[classTag]
                    new_score = old_score
                    if old_score < score:
                        new_score = round(score, 2)
                    result[classTag] = new_score
        result = np.array(list(result.items()), dtype=object).tolist()

        return result

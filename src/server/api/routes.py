from api.objectDetection import ObjectDetectionApi
from api.faceDetection import FaceDetectionApi


def create_routes(api):
    api.add_resource(ObjectDetectionApi, '/object-detection')
    api.add_resource(FaceDetectionApi, '/face-detection')

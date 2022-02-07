from api.objectDetection import ObjectDetectionApi
from api.faceDetection import FaceDetectionApi
from api.generateScenes import GenerateScenes


def create_routes(api):
    api.add_resource(ObjectDetectionApi, '/object-detection')
    api.add_resource(FaceDetectionApi, '/face-detection')
    api.add_resource(GenerateScenes, '/generate-scenes')

from api.objectDetection import ObjectDetectionApi


def create_routes(api):
    api.add_resource(ObjectDetectionApi, '/object-detection')

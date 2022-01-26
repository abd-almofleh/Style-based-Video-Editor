
from flask import jsonify, request
from flask_restful import Resource
from services.SceneDetection import SceneDetection


class GenerateScenes(Resource):

    def get(self):
        paths = request.args.getlist("paths")
        scenes = SceneDetection.extract_scenes(paths)
        return jsonify({'result': scenes})

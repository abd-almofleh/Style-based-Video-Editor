
from pydoc import resolve
from flask import app, jsonify, request, Response
from flask_restful import Resource
from services.SceneDetection import SceneDetection
from helpers.helper import cut_video


class GenerateScenes(Resource):

    def get(self):
        paths = request.args.getlist("paths")

        scenes = SceneDetection.extract_scenes(paths)

        return jsonify({'result': scenes})

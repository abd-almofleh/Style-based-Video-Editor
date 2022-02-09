
from flask import jsonify, request
from flask_restful import Resource
from services.SceneDetection import SceneDetection
import json


class GenerateScenes(Resource):

  def get(self):
    paths = request.args.getlist("paths")
    # scenes = SceneDetection.extract_scenes(paths)
    with open('./data/data1.json', encoding='utf-8') as f:
      scenes = json.load(f)
    return jsonify({'result': scenes})

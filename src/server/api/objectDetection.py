from flask import jsonify, request, Response
from flask_restful import Resource


class ObjectDetectionApi(Resource):
    @staticmethod
    def post() -> Response:
        data = request
        output = {'id': "OK"}
        return jsonify({'result': output})

    def get(self):
        output = request.get_json()

        return jsonify({'result': output})

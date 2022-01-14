import os
from flask import Flask, app
from flask_restful import Api
from api.routes import create_routes
import warnings
warnings.filterwarnings("ignore")
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'


def get_flask_app() -> app.Flask:

    flask_app = Flask(__name__)
    flask_app.config['MAX_CONTENT_PATH'] = "20000000"
    api = Api(app=flask_app)

    create_routes(api=api)
    return flask_app


if __name__ == '__main__':
    app = get_flask_app()
    app.run(debug=True)

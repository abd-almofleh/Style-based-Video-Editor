import os
import warnings  # noqa
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'  # noqa
os.environ["CUDA_VISIBLE_DEVICES"] = "-1"  # noqa
warnings.filterwarnings("ignore")  # noqa
from flask import Flask, app, request
from flask_restful import Api
from api.routes import create_routes
from dotenv import load_dotenv
from pathlib import Path

# -------------------------------
dotenv_path = '.env'  # Path to .env file
load_dotenv(dotenv_path)


def get_flask_app() -> app.Flask:

  flask_app = Flask(__name__)
  flask_app.config['MAX_CONTENT_PATH'] = "20000000"
  api = Api(app=flask_app)

  create_routes(api=api)
  return flask_app


def before_request():
  app.logger.info(f"\"{request.method} {request.path}\"")


if __name__ == '__main__':
  app = get_flask_app()
  app.before_request(before_request)
  app.run(debug=True, threaded=True)

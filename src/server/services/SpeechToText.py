import requests
import json
import mimetypes
import os
from helpers.helper import extract_mp3_from_video


class SpeechToText:
    KATEB = {
        "base_url": "https://px.kateb.ai:4040/api",
        "sub_urls": {
            "login": "login",
            "recognize_file": "recognize-file",
            "get_minutes": "getMinutes",
        },
        "login_information": {
            "email": "9ff80e84ee@emailnax.com",
            "apiKey": "94275ec3a9e74e66b32d6a4d5bf7245e",

        },
        "languages_codes": {
            "arabic_egyptian_dialect": "EG",
            "english": "EN",
            "arabic_saudi_dialect": "EN"
        }
    }

    def __init__(self) -> None:
        self.token = None
        connect = self.kateb_login()
        while not connect:
            print("Filed to login to kateb.ai, retrying...")
            connect = self.kateb_login()

    def kateb_login(self) -> bool:
        login_url = SpeechToText.KATEB["base_url"] + \
            "/"+SpeechToText.KATEB["sub_urls"]["login"]
        try:
            response = requests.request(
                "POST", login_url, params=SpeechToText.KATEB["login_information"])
            result = json.loads(response.text)
            if result["status"] != "OK":
                return False
            self.token = result["message"]
            return True
        except BaseException as err:
            return False

    def arabic_speech_recognition(self, file_path, language="arabic_egyptian_dialect"):
        filename, file_extension = os.path.splitext(file_path)
        if file_extension != ".mp3":
            file_path = str(extract_mp3_from_video(
                file_path).absolute().resolve())

        headers = {
            'authorization': f'Bearer {self.token}'
        }
        tempFile = open(file_path, 'rb')
        files = {
            'File': ('file.mp3', tempFile, mimetypes.guess_type(file_path)[0]),
            'LanguageCode': (None, SpeechToText.KATEB["languages_codes"][language]),
        }
        recognize_url = SpeechToText.KATEB["base_url"] + \
            "/"+SpeechToText.KATEB["sub_urls"]["recognize_file"]
        response = None
        while response == None:
            try:
                response = requests.post(
                    recognize_url, headers=headers, files=files)
            except Exception as e:
                print("Filed to get recognition from kateb.ai, retrying...")
                response = None
            prediction = json.loads(response.text)
            text_string = prediction["Text_String"]

        return text_string

    @staticmethod
    def extract_scenes_text(text_data, scenes):
        word_num = 0
        scripts = []
        for i in enumerate(scenes):
            scripts.append(
                {"arabic_text": "", "confidence": float(0), "count": 0})
        for i, scene in enumerate(scenes):
            while word_num < len(text_data) and text_data[word_num]["start"] >= scene["start_time"] and text_data[word_num]["start"] <= scene["end_time"]:
                if text_data[word_num]["text"] != "<SILENCE>":

                    scripts[i]["arabic_text"] += " " + \
                        text_data[word_num]["text"]
                    print(word_num, i, scripts[i]["arabic_text"])

                    scripts[i]["confidence"] += text_data[word_num]["confidence"]
                    scripts[i]["count"] += 1
                word_num += 1

        for i, script in enumerate(scripts):
            scripts[i]["arabic_text"] = scripts[i]["arabic_text"].strip()
            scripts[i]["confidence"] = ((
                script["confidence"] / script["count"]) if script["count"] != 0 else 0) if scenes[i]["speaker"] != -1 else 1
            del scripts[i]["count"]
        return scripts

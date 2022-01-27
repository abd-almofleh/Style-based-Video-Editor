import requests
import json
import mimetypes


class SpeechToText:
    KATEB = {
        "base_url": "https://px.kateb.ai:4040/api",
        "login": "login",
        "recognize_file": "recognize-file",
        "login_information": {
            "email": "9ff80e84ee@emailnax.com",
            "apiKey": "94275ec3a9e74e66b32d6a4d5bf7245e",
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
            "/"+SpeechToText.KATEB["login"]
        response = requests.request(
            "POST", login_url, params=SpeechToText.KATEB["login_information"])
        result = json.loads(response.text)
        if result["status"] != "OK":
            return False
        self.token = result["message"]
        return True

    def arabic_speech_recognition(self, file_path):
        headers = {
            'authorization': f'Bearer {self.token}'
        }
        tempFile = open(file_path, 'rb')
        files = {
            'File': ('1_speaker.mp3', tempFile, mimetypes.guess_type(file_path)[0]),
            'LanguageCode': (None, 'EG'),
        }
        recognize_url = SpeechToText.KATEB["base_url"] + \
            "/"+SpeechToText.KATEB["recognize-file"]

        response = requests.post(recognize_url, headers=headers, files=files)
        prediction = json.loads(response.text,)
        text_string = prediction["Text_String"]
        result = {
            "arabic_text": "",
            "score": 0,
        }
        for word in text_string:
            result["arabic_text"] += " " + word["text"]
            result["score"] += word["confidence"]
        result["score"] = round(result["score"]/2, 2)

        return result

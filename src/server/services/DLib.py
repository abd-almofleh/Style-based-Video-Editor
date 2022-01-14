import cv2
import dlib
from pathlib import Path
from helpers.constant import FACES_PATH


class DLib:
    def __init__(self) -> None:
        pass

    @staticmethod
    def convert_and_trim_bb(image, rect):
        startX = rect.left()
        startY = rect.top()
        endX = rect.right()
        endY = rect.bottom()
        startX = max(0, startX)
        startY = max(0, startY)
        endX = min(endX, image.shape[1])
        endY = min(endY, image.shape[0])
        # w = endX - startX
        # h = endY - startY
        return [startX, startY, endX, endY]

    @staticmethod
    def detect_faces_and_landmarks(imagePath, cnn=False):
        image = cv2.imread(imagePath)

        PREDICTOR_PATH = str(Path(__file__).parent.parent.joinpath(
            "weights/shape_predictor_68_face_landmarks.dat").resolve())
        DETECTOR_PATH = str(Path(__file__).parent.parent.joinpath(
            "weights/mmod_human_face_detector.dat").resolve())

        Detector = dlib.get_frontal_face_detector(
        ) if not cnn else dlib.cnn_face_detection_model_v1(DETECTOR_PATH)

        landmarkDetector = dlib.shape_predictor(PREDICTOR_PATH)

        faceRects = Detector(image, 1)

        landmarksAll = []

        for i in range(0, len(faceRects)):
            newRect = dlib.rectangle(int(faceRects[i].left()),
                                     int(faceRects[i].top()),
                                     int(faceRects[i].right()),
                                     int(faceRects[i].bottom()))
            landmarks = landmarkDetector(image, newRect)
            landmarksAll.append(landmarks)
        boxes = [DLib.convert_and_trim_bb(image, r) for r in faceRects]
        return landmarksAll, boxes

    @staticmethod
    def saveFaces(imagePath, boxes, name=""):
        image = cv2.imread(imagePath)
        counter = 1
        paths = []
        root = Path(FACES_PATH)
        for (x, y, w, h) in boxes:
            crop = image[y:h, x:w]
            endIndex = name.find(".jpg")
            path = (
                root / f"{name[:endIndex]}-{counter}.jpg").absolute().resolve()
            cv2.imwrite(str(path), crop)
            paths.append(str(path))
            counter += 1
        return paths

    @staticmethod
    def ExtractFaces(imagePath):
        imagename = Path(imagePath).name
        landmarks, rectangles = DLib.detect_faces_and_landmarks(imagePath)
        paths = DLib.saveFaces(imagePath, rectangles, imagename)
        result = []
        print()
        for i in range(len(rectangles)):
            rectangle = rectangles[i]
            landmark = landmarks[i].parts()
            face = {}
            face["facial_area"] = rectangle
            landmarksObj = dict()
            landmarksObj["left_eye"] = [
                (landmark[37].x + landmark[40].x)/2.0, (landmark[37].y + landmark[40].y)/2.0]
            landmarksObj["mouth_left"] = [landmark[49].x, landmark[49].y]
            landmarksObj["mouth_right"] = [landmark[55].x, landmark[55].y]
            landmarksObj["nose"] = [landmark[34].x, landmark[34].y]
            landmarksObj["right_eye"] = [
                (landmark[43].x + landmark[46].x)/2.0, (landmark[43].y + landmark[46].y)/2.0]
            face["landmarks"] = landmarksObj
            face["score"] = 1
            face["path"] = paths[i]
            result.append(face)
        return result

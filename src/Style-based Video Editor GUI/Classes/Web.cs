using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp;
using RestSharp.Serialization.Json;
using Newtonsoft.Json.Linq;

namespace Style_based_Video_Editor_GUI.Classes
{
  static class Web
  {
    static RestClient Server = new RestClient($"{Constants.SERVER_URL}:{Constants.SERVER_PORT}");
    public static string ObjectDetectionRoute = "object-detection";
    public static string FaceDetectionRoute = "face-detection";
    public static string GenerateScenesRoute = "generate-scenes";
    
    public static List<Structs.KeyScore> DetectObjects(string imagePath)
    {
      RestRequest request = new RestRequest(ObjectDetectionRoute, DataFormat.Json);
      request.AddFile("image", imagePath);

      Server.UseSerializer(() => new JsonSerializer {RootElement="result" });
      IRestResponse response = Server.Post(request);
      // TODO: add a status to the dash board
      if (!response.IsSuccessful)
      {
        Console.WriteLine(response.ErrorMessage);
        return null;
      }
      dynamic objects = JObject.Parse(response.Content);
      if (objects == null) return null;
      objects = objects.result;

      List<Structs.KeyScore> Objects = new List<Structs.KeyScore>(objects.Count);
      foreach(dynamic item in objects)
      {
        string key = (string)item[0];
        double score = (double) item[1][0];
        double count = (double) item[1][1];
        Dictionary<string, double> d = new Dictionary<string, double>();
        d.Add("count", count);
        Objects.Add(new Structs.KeyScore(key, score, d));
      }
      return Objects;
    }

    public static List<PersonImage> DetectFaces(string imagePath)
    {
      RestRequest request = new RestRequest(FaceDetectionRoute, DataFormat.Json);
      request.AddFile("image", imagePath);

      Server.UseSerializer(() => new JsonSerializer { RootElement = "result" });
      IRestResponse response = Server.Post(request);
      // TODO: add a status to the dash board
      if (!response.IsSuccessful)
      {
        Console.WriteLine(response.ErrorMessage);
        return null;
      }

      dynamic faces = JObject.Parse(response.Content);
      if (faces == null) return null;
      faces = faces.result;
      List<PersonImage>  personImages = new List<PersonImage>(faces.Count);
      foreach (dynamic face in faces)
      {
        int[] facial_Area = Helper.GatArray<int>(face.location_info.facial_area);
        double[] left_eye = Helper.GatArray<double>(face.location_info.landmarks.left_eye);
        double[] mouth_left = Helper.GatArray<double>(face.location_info.landmarks.mouth_left);
        double[] mouth_right = Helper.GatArray<double>(face.location_info.landmarks.mouth_right);
        double[] nose = Helper.GatArray<double>(face.location_info.landmarks.nose);
        double[] right_eye = Helper.GatArray<double>(face.location_info.landmarks.right_eye);
        Structs.KeyScore[] emotion = new Structs.KeyScore[face.emotion.Count];

        for (int i = 0; i < face.emotion.Count; i++)
          emotion[i] = new Structs.KeyScore((string)face.emotion[i][0], (double)face.emotion[i][1]);
        PersonImage p = new PersonImage((string)face.path, (double)face.score, (int)face.age, (string)face.dominant_emotion, (string)face.gender, emotion, facial_Area, left_eye, mouth_left, mouth_right, nose, right_eye); ;
        personImages.Add(p);

      }
      return personImages;
    }

    public static SceneInfo GenerateScenes(View [] views)
    {
      RestRequest request = new RestRequest(GenerateScenesRoute, DataFormat.Json);
      request.Timeout = 10 * 60 * 1000;
      foreach(View v in views)
        request.AddParameter("paths", v.video.FullName);

      Server.UseSerializer(() => new JsonSerializer { RootElement = "result" });
      IRestResponse response = Server.Get(request);
      // TODO: add a status to the dash board
      if (!response.IsSuccessful)
      {
        Console.WriteLine(response.ErrorMessage);
        return null;
      }
      List<Scene>[] all = new List<Scene>[views.Length];
      dynamic scenesData = JObject.Parse(response.Content);
      if (scenesData == null) return null;
      scenesData = scenesData.result;
      for (int i = 0; i < views.Length; i++)
      {
        View view = views[i];
        string viewName = view.video.Name;
        viewName = viewName.Substring(0, viewName.LastIndexOf("."));

        dynamic viewsInfo = scenesData[viewName];
        all[i] = new List<Scene>(viewsInfo.Count);
        for (int j = 0; j < viewsInfo.Count; j++)
        {
          dynamic viewInfo = viewsInfo[j];
          double startTime = (double)viewInfo.start_time;
          double endTime = (double)viewInfo.end_time;
          int speaker = (int)viewInfo.speaker;
          string path = (string)viewInfo.path;
          string image = (string)viewInfo.image;
          TimeSpan timeOfStart = new TimeSpan(0, 0, 0, (int)startTime, (int)(startTime % (int)startTime) * 1000);
          TimeSpan timeOfEnd = new TimeSpan(0, 0, 0, (int)endTime, (int)(endTime % (int)endTime) * 1000);
          Scene s = new Scene(view, path, image, startTime, endTime);
          all[i].Add(s);
        }
      }
      dynamic scriptsObjects = scenesData.scripts;
      Script[] scripts = new Script[scriptsObjects.Count]; 
      for (int i = 0; i < scriptsObjects.Count; i++)
      {
        string ArabicText = (string)scriptsObjects[i].arabic_text;
        double score = (double)scriptsObjects[i].confidence;
        scripts[i] = new Script(ArabicText, score);
      }



      return new SceneInfo(all,scripts);
    }
  }
}

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
    
    public static List<Structs.KeyScore> DetectObjects(string imagePath)
    {
      RestRequest request = new RestRequest(ObjectDetectionRoute, DataFormat.Json);
      request.AddFile("image", imagePath);

      Server.UseSerializer(() => new JsonSerializer {RootElement="result" });
      IRestResponse<Dictionary<string, double>> response = Server.Post<Dictionary<string, double>>(request);
      // TODO: add a status to the dash board
      if (!response.IsSuccessful)
      {
        Console.WriteLine(response.ErrorMessage);
        return null;
      }

      List<Structs.KeyScore> Objects = new List<Structs.KeyScore>(response.Data.Count);
      foreach(string key in response.Data.Keys)
      {
        Objects.Add(new Structs.KeyScore(key, response.Data[key]));
      }
      return Objects;
    }

    public static dynamic DetectFaces(string imagePath)
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
      return JObject.Parse(response.Content);
    }
  }
}

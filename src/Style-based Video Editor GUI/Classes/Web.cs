using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp;
using RestSharp.Serialization.Json;

namespace Style_based_Video_Editor_GUI.Classes
{
  static class Web
  {
    static RestClient Server = new RestClient($"{Constants.SERVER_URL}:{Constants.SERVER_PORT}");
    public static string ObjectDetectionRoute = "object-detection";
    
    public static List<Structs.Tag> post(RestRequest request)
    {
      Server.UseSerializer(() => new JsonSerializer {RootElement="result" });
      IRestResponse<Dictionary<string, double>> response = Server.Post<Dictionary<string, double>>(request);
      // TODO: add a status to the dash board
      if (!response.IsSuccessful)
      {
        Console.WriteLine(response.ErrorMessage);
        return null;
      }

      List<Structs.Tag> Objects = new List<Structs.Tag>(response.Data.Count);
      foreach(string key in response.Data.Keys)
      {
        Objects.Add(new Structs.Tag(key, response.Data[key]));
      }
      return Objects;
    }
  }
}

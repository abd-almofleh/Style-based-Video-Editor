using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace Style_based_Video_Editor_GUI.Classes
{
  public class Scene
  {
    uint sceneNumber;

    uint startFrame;
    uint endFrame;

    TimeSpan startTime;
    TimeSpan endTime;

    FileInfo video;
    FileInfo image;

    public uint SceneNumber { get => sceneNumber; }

    public uint StartFrame { get => startFrame; }
    public uint EndFrame { get => endFrame; }

    public TimeSpan StartTime { get => startTime; }
    public TimeSpan EndTime { get => endTime; }
    public FileInfo Video { get => video; }
    public FileInfo Image { get => image; }

    internal List<Structs.Tag> Objects = new List<Structs.Tag>();

    public Scene(uint sceneNumber, uint startFrame, uint endFrame, TimeSpan startTime, TimeSpan endTime, string scenesDir)
    {
      this.sceneNumber = sceneNumber;

      this.startFrame = startFrame;
      this.endFrame = endFrame;

      this.startTime = startTime;
      this.endTime = endTime;
      this.image = new FileInfo(scenesDir + "/images/" + sceneNumber.ToString("000") + ".jpg");
      this.video = new FileInfo(scenesDir + "/videos/" + sceneNumber.ToString("000") + ".mp4");
    }

    public void DetectObjects()
    {
      Thread t = new Thread(() =>
      {
        RestRequest request = new RestRequest(Web.ObjectDetectionRoute, DataFormat.Json);
        request.AddFile("image", Image.FullName);
        Objects = Web.post(request);
        foreach (var x in Objects)
          Console.WriteLine($"tag: {x.tag}, score: {x.score}");

      });
      t.Start();
    }

  }
}

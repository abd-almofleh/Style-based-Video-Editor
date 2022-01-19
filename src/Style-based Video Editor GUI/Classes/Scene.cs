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

    internal List<Structs.KeyScore> Objects;
    internal List<Person> Persons;
    internal List<PersonImage> personImages; 

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

    public void DetectObjects(Windows.Dashboard window)
    {
      Thread t = new Thread(() =>
      {
        Objects = Web.DetectObjects(Image.FullName);
        window.Dispatcher.Invoke(() =>
        {
          window.showTags(Objects);
        });
      });
      t.IsBackground = true;
      t.Start();
    }

    public void DetectPersons(Windows.Dashboard window)
    {
      Scene that = this;      
      Thread t = new Thread(() =>
      {
        Console.WriteLine(Image.FullName);
        dynamic faces = Web.DetectFaces(Image.FullName);
        if (faces == null) return;
        faces = faces.result;
        List<PersonImage> personImages = new List<PersonImage>(faces.Count);
        foreach (dynamic face in faces)
        {
          int[] facial_Area = Helper.GatArray<int>( face.location_info.facial_area);
          double[] left_eye = Helper.GatArray<double>( face.location_info.landmarks.left_eye);
          double[] mouth_left = Helper.GatArray<double>( face.location_info.landmarks.mouth_left);
          double[] mouth_right = Helper.GatArray<double>( face.location_info.landmarks.mouth_right);
          double[] nose = Helper.GatArray<double>( face.location_info.landmarks.nose);
          double[] right_eye = Helper.GatArray<double>( face.location_info.landmarks.right_eye);
          Structs.KeyScore[] emotion = new Structs.KeyScore[face.emotion.Count];

          for (int i = 0; i < face.emotion.Count; i++)
            emotion[i] = new Structs.KeyScore((string)face.emotion[i][0], (double)face.emotion[i][1]);
          PersonImage p = new PersonImage((string)face.path, (double)face.score,(int)face.age,(string)face.dominant_emotion,(string)face.gender, emotion, facial_Area, left_eye, mouth_left, mouth_right, nose, right_eye); ;
          personImages.Add(p);

        }
        window.Dispatcher.Invoke(() =>
        {
          window.ShowSceneFaces(personImages);
        });
      });
      t.IsBackground = true;
      t.Start();
    }

  }
}

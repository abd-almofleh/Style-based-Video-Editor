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

    internal List<Structs.Tag> Objects;
    internal List<Person> Persons;

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
      Console.WriteLine("Detecting Faces");
      Thread t = new Thread(() =>
      {
        Console.WriteLine(Image.FullName);
        string []paths = Web.DetectFaces(Image.FullName);
        return;
        List<Person> Persons = new List<Person>(paths.Length);
        //foreach (string value in paths)
        //{
        //  Persons.Add(new Person(value,this));
        //}
        foreach (var item in paths)
        {
          Console.WriteLine(item);
        }
        window.Dispatcher.Invoke(() =>
        {
          //window.showTags(Objects);

        });
      });
      t.IsBackground = true;
      t.Start();
    }

  }
}

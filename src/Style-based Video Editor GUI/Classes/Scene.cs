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
  internal class Scene:Video
  {
    uint startFrame;
    uint endFrame;

    TimeSpan startTime;
    TimeSpan endTime;
    View originalVideo;

    public uint SceneNumber { get => VideoNumber; }

    public uint StartFrame { get => startFrame; }
    public uint EndFrame { get => endFrame; }

    public TimeSpan StartTime { get => startTime; }
    public TimeSpan EndTime { get => endTime; }
    public FileInfo Video { get => video; }
    public FileInfo Image { get => image; }
    public View OriginalVideo { get => originalVideo; }

    internal List<Structs.KeyScore> Objects;
    internal List<Person> Persons;
    internal List<PersonImage> personImages; 

    // ! deprecated
    // ! used with scene detection using visual changes
    public Scene(View OriginalVideo, uint sceneNumber, uint startFrame, uint endFrame, TimeSpan startTime, TimeSpan endTime, string scenesDir)
      :base(new FileInfo(scenesDir + "/videos/" + sceneNumber.ToString("000") + ".mp4"),
         new FileInfo(scenesDir + "/images/" + sceneNumber.ToString("000") + ".jpg"),
         new System.Windows.Duration(endTime - startTime))
    {
      this.VideoNumber = sceneNumber;

      this.startFrame = startFrame;
      this.endFrame = endFrame;

      this.startTime = startTime;
      this.endTime = endTime;
      this.originalVideo = OriginalVideo;
    }

    public Scene(View OriginalVideo, string path, double startTime, double endTime)
  : base(new FileInfo(path), endTime - startTime)
    {
      this.originalVideo = OriginalVideo;
      this.startFrame = this.endFrame = 0;
      this.startTime = GetVideoLength(startTime).TimeSpan;
      this.endTime = GetVideoLength(endTime).TimeSpan;
    }

    public void DetectObjects(Windows.Dashboard window)
    {
      Scene that = this;

      Thread t = new Thread(() =>
      {
        Objects = Web.DetectObjects(Image.FullName);
        window.Dispatcher.Invoke(() =>
        {
          window.showTags(that);
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
        this.personImages =  Web.DetectFaces(Image.FullName);
        if (this.personImages == null) return;
        window.Dispatcher.Invoke(() =>
        {
          window.ShowSceneFaces(that);
        });
      });
      t.IsBackground = true;
      t.Start();
    }

  }
}

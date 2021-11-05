using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Classes
{
  class Scene
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

  }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Style_based_Video_Editor_GUI.Classes
{
  internal class View: Video
  {
    public Scene[] scenes;

    public View(string path, FileInfo thumbnail, Duration length) : base(new FileInfo(path), thumbnail, length) { }
    public View(FileInfo video, FileInfo thumbnail, Duration length) : base(video, thumbnail, length) { }

    public static Script[] DetectScenesOnSpeakerChange(View[] views)
    {
      SceneInfo sceneInfo = Web.GenerateScenes(views);
      if (sceneInfo == null) return null;
      List<Scene>[] scenes = sceneInfo.scenes;
      for (int i = 0; i < scenes.Length; i++)
      {
        views[i].scenes = scenes[i].ToArray();
      }
      return sceneInfo.scripts;
    }

    public void DetectScenesOnVisualChange()
    {
      DirectoryInfo directory = new DirectoryInfo($"./temp/{video.Name}");
      if (directory.Exists)
        directory.Delete(true);
      string detectionCommand = "scenedetect -i \"{0}\"  -o \"./temp/{1}\" detect-content split-video -o \"./temp/{1}/videos\" -f $SCENE_NUMBER -q list-scenes -q -s -f times.csv save-images --quality 70 -o \"./temp/{1}/images\" -f $SCENE_NUMBER -n 1 -c 6";
      Helper.RunCMDCommand(String.Format(detectionCommand, video.FullName, video.Name));

      string[] lines = File.ReadAllLines($"./temp/{video.Name}/times.csv").Skip(1).ToArray();
      IEnumerable<Scene> scenesArray = lines.Select(line =>
     {
       string[] data = line.Split(',');
       uint sceneNumber = uint.Parse(data[0]);
       uint startFrame = uint.Parse(data[1]);
       uint endFrame = uint.Parse(data[4]);
       TimeSpan startTime = TimeSpan.Parse(data[2]);
       TimeSpan endTime = TimeSpan.Parse(data[5]);

       return new Scene(this, sceneNumber, startFrame, endFrame, startTime, endTime, $"./temp/{video.Name}");
     });
      scenes = scenesArray.ToArray();
    }

  }
}
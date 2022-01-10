using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Style_based_Video_Editor_GUI.Classes
{
  internal class Video
  {
    public FileInfo video;
    public FileInfo thumbnail;
    public Scene[] scenes;

    public Duration length;

    private static readonly string detectionCommand = "scenedetect -i \"{0}\"  -o \"./temp/{1}\" detect-content split-video -o \"./temp/{1}/videos\" -f $SCENE_NUMBER -q list-scenes -q -s -f times.csv save-images --quality 70 -o \"./temp/{1}/images\" -f $SCENE_NUMBER -n 1 -c 6";
    
    public Video(string path, FileInfo thumbnail,Duration length) :this(new FileInfo(path), thumbnail, length) { }
    public Video(FileInfo video,FileInfo thumbnail, Duration length)
    {
      this.video = video;
      if (!video.Exists) throw new FileNotFoundException();

      string ext = video.Extension.Substring(1).ToLower();
      int IsSupported = Array.FindIndex(Constants.SUPPORTED_VIDEO_TYPES, item => item == ext);
      if (IsSupported < 0) throw new Exceptions.VideoFileNotSupported(ext, Constants.SUPPORTED_VIDEO_TYPES);
      this.thumbnail = thumbnail;
      this.length = length;
    }

    public void detectScenes()
    {
      DirectoryInfo directory = new DirectoryInfo($"./temp/{video.Name}");
      if (directory.Exists)
        directory.Delete(true);
      Helper.RunCMDCommand(String.Format(detectionCommand, video.FullName, video.Name));

      string[] lines = File.ReadAllLines($"./temp/{video.Name}/times.csv").Skip(1).ToArray();
      IEnumerable<Scene> scenesArray =  lines.Select(line =>
      {
        string[] data = line.Split(',');
        uint sceneNumber = uint.Parse(data[0]);
        uint startFrame = uint.Parse(data[1]);
        uint endFrame = uint.Parse(data[4]);
        TimeSpan startTime = TimeSpan.Parse(data[2]);
        TimeSpan endTime = TimeSpan.Parse(data[5]);

        return new Scene(sceneNumber, startFrame, endFrame, startTime, endTime, $"./temp/{video.Name}");
      });
      this.scenes = scenesArray.ToArray();
    }

  }
}
using System;
using System.Collections.Generic;
using System.IO;

namespace Style_based_Video_Editor_GUI.Classes
{
  internal class Video
  {
    public FileInfo video;
    public FileInfo thumbnail;
    public Scene[] scenes;

    private static readonly string detectionCommand = "scenedetect -i \"{0}\"  -o \"./temp/{1}\" detect-content split-video -o \"./temp/{1}/videos\" -f $SCENE_NUMBER -q list-scenes -q -s -f times.csv save-images --quality 70 -o \"./temp/{1}/images\" -f $SCENE_NUMBER -n 1 -c 6";
    
    public Video(string path, FileInfo thumbnail) :this(new FileInfo(path), thumbnail) { }
    public Video(FileInfo video,FileInfo thumbnail)
    {
      this.video = video;
      if (!video.Exists) throw new FileNotFoundException();

      string ext = video.Extension.Substring(1).ToLower();
      int IsSupported = Array.FindIndex(Constants.SUPPORTED_VIDEO_TYPES, item => item == ext);
      if (IsSupported < 0) throw new Exceptions.VideoFileNotSupported(ext, Constants.SUPPORTED_VIDEO_TYPES);
      this.thumbnail = thumbnail;
    }

    public void detectScenes()
    {
      //Console.WriteLine(String.Format(detectionCommand, video.FullName, video.Name));
      //DirectoryInfo directory = new DirectoryInfo($"./temp/{video.Name}");
      //if (directory.Exists)
      //  directory.Delete(true);
      //Helper.RunCMDCommand(String.Format(detectionCommand, video.FullName, video.Name));
      //List<Scene> scenes = new List<Scene>();
      //using (TextFieldParser parser = new TextFieldParser($"./temp/{video.Name}/times.csv"))
      //{
      //  parser.TextFieldType = FieldType.Delimited;
      //  parser.SetDelimiters(",");
      //  parser.ReadFields();
      //  while (!parser.EndOfData)
      //  {
      //    string[] fields = parser.ReadFields();
      //    uint sceneNumber = uint.Parse(fields[0]);
      //    uint startFrame = uint.Parse(fields[1]);
      //    uint endFrame = uint.Parse(fields[4]);

      //    TimeSpan startTime = TimeSpan.Parse(fields[2]);
      //    TimeSpan endTime = TimeSpan.Parse(fields[5]);
      //    scenes.Add(new Scene(sceneNumber, startFrame, endFrame, startTime, endTime, $"./temp/{video.Name}"));
      //  }

      //}
      //this.scenes = scenes.ToArray();

    }

  }
}
using System;
using System.IO;

namespace Style_based_Video_Editor_GUI.Classes
{
  internal class Video
  {
    public FileInfo video;
    public FileInfo[] images;
    public FileInfo[] scenes;

    private static string detectionCommand = "scenedetect -i \"{0}\"  -o \"./temp/{1}\" detect-content split-video -o \"./temp/{1}/videos\" -f $SCENE_NUMBER -q list-scenes -q -s -f times.csv save-images --quality 70 -o \"./temp/{1}/images\" -f $SCENE_NUMBER-$IMAGE_NUMBER -n 1 -c 6";
    public Video(string path)
    {
      string xx = Path.GetFullPath(path);
      video = new FileInfo(path);
      if (!video.Exists) throw new FileNotFoundException();

      string ext = video.Extension.Substring(1).ToLower();
      int IsSupported = Array.FindIndex(Constants.supportedVideoTypes, item => item == ext);
      if (IsSupported < 0) throw new Exceptions.VideoFileNotSupported(ext, Constants.supportedVideoTypes);
    }

    public void detectScenes()
    {
      Console.WriteLine(String.Format(detectionCommand, video.FullName, video.Name));
      Helper.RunCMDCommand(String.Format(detectionCommand, video.FullName, video.Name));
    }
  }
}
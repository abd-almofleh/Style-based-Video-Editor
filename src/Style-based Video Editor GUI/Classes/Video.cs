using System;
using System.IO;

namespace Style_based_Video_Editor_GUI.Classes
{
  internal class Video
  {
    public FileInfo video;
    public FileInfo[] images;
    public FileInfo[] scenes;

    public Video(string path)
    {
      string xx = Path.GetFullPath(path);
      video = new FileInfo(path);
      if (!video.Exists) throw new FileNotFoundException();

      string ext = video.Extension.Substring(1).ToLower();
      int IsSupported = Array.FindIndex(Constants.supportedVideoTypes, item => item == ext);
      if (IsSupported < 0) throw new Exceptions.VideoFileNotSupported(ext, Constants.supportedVideoTypes);
    }
  }
}
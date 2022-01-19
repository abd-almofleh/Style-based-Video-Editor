using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Style_based_Video_Editor_GUI.Classes
{
  internal class Video
  {
    static uint ID = 0;
    public uint VideoNumber;
    public FileInfo video;
    public FileInfo image;
    public Duration length;
    public Video(string path, FileInfo thumbnail, Duration length) : this(new FileInfo(path), thumbnail, length) { }
    public Video(FileInfo video, FileInfo thumbnail, Duration length)
    {
      this.video = video;
      if (!video.Exists) throw new FileNotFoundException();

      string ext = video.Extension.Substring(1).ToLower();
      int IsSupported = Array.FindIndex(Constants.SUPPORTED_VIDEO_TYPES, item => item == ext);
      if (IsSupported < 0) throw new Exceptions.VideoFileNotSupported(ext, Constants.SUPPORTED_VIDEO_TYPES);
      this.image = thumbnail;
      this.length = length;
      this.VideoNumber = Video.ID++;
    }
    public static FileInfo GenrateImage(string VideoPath)
    {
      return GenrateImage(new FileInfo(VideoPath));
    }
    public static FileInfo GenrateImage(FileInfo video)
    {
      DirectoryInfo ThumbnailDirectory = new DirectoryInfo("thumbnail");
      if (!ThumbnailDirectory.Exists) ThumbnailDirectory.Create();

      string ThumbnailPath = DateTime.Now.Ticks.ToString() + ".jpg";
      FileInfo file = new FileInfo(Path.Combine(ThumbnailDirectory.Name, ThumbnailPath));
      if (file.Exists) file.Delete();
      string command = $"ffmpeg -i \"{video.FullName}\" -ss 00:00:07 -vframes 1 -f image2 -vcodec mjpeg \"{file.FullName}\" -y";

      CMDResult result = Helper.RunCMDCommand(command);
      //if (result.ExitCode != 0) throw new Exception($"Error NO. {result.ExitCode}:Command {result.Command},Message: {result.OutputMessage}");

      return file;
      
    }

  }
}

using System;
using System.Diagnostics;
using System.IO;

namespace Style_based_Video_Editor_GUI.Classes
{
  internal static class Helper
  {
    public static CMDResult RunCMDCommand(string command)
    {
      Process process = new Process();
      process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
      process.StartInfo.FileName = "cmd.exe";
      process.StartInfo.Arguments = $"/C {command}";
      process.StartInfo.UseShellExecute = false;
      //process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      process.WaitForExit();// Waits here for the process to exit.
      //string output = process.StandardOutput.ReadToEnd();   
      string output = "";
      //Console.WriteLine(output);
      CMDResult result = new CMDResult(command, output, process.ExitCode);
      return result;

    }
    public static FileInfo GenerateThumbnail(string VideoPath)
    {
      return GenerateThumbnail(new FileInfo(VideoPath));
    }
    public static FileInfo GenerateThumbnail(FileInfo VideoPath)
    {
      DirectoryInfo ThumbnailDirectory = new DirectoryInfo("thumbnail");
      if (!ThumbnailDirectory.Exists) ThumbnailDirectory.Create();

      string ThumbnailPath = DateTime.Now.Ticks.ToString() + ".jpg";
      FileInfo file = new FileInfo(Path.Combine(ThumbnailDirectory.Name, ThumbnailPath));
      if (file.Exists) file.Delete();
      string command = $"ffmpeg -i \"{VideoPath.FullName}\" -ss 00:00:07 -vframes 1 -f image2 -vcodec mjpeg \"{file.FullName}\" -y";

      CMDResult result = RunCMDCommand(command);
      //if (result.ExitCode != 0) throw new Exception($"Error NO. {result.ExitCode}:Command {result.Command},Message: {result.OutputMessage}");
        
      return file;
    }
    public static T[] GatArray<T>(dynamic DArray)
    {
      T[] NewArray = new T[DArray.Count];
      for (int i = 0; i < DArray.Count; i++)
      {
        NewArray[i] = (T)DArray[i];
      }
      return NewArray;
    }
  }
}
using System;

namespace Style_based_Video_Editor_GUI.Classes
{
  internal static class Helper
  {
    public static void VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
    {
      e.VlcLibDirectory = new System.IO.DirectoryInfo(System.IO.Path.Combine(".", "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
    }

    public static void RunCMDCommand(string command)
    {
      System.Diagnostics.Process process = new System.Diagnostics.Process();
      process.StartInfo = new System.Diagnostics.ProcessStartInfo();
      process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
      process.StartInfo.FileName = "cmd.exe";
      process.StartInfo.Arguments = $"/C {command}";
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.RedirectStandardOutput = true;
      process.Start();
      string output = process.StandardOutput.ReadToEnd();
      process.WaitForExit();// Waits here for the process to exit.
      Console.WriteLine(output);
    }
  }
}
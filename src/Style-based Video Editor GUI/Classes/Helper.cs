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
      process.StartInfo.RedirectStandardOutput = true;
      process.StartInfo.CreateNoWindow = true;
      process.Start();
      process.WaitForExit();
      string output = process.StandardOutput.ReadToEnd();   
      CMDResult result = new CMDResult(command, output, process.ExitCode);
      return result;

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
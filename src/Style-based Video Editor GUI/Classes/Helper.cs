using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;

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
		public static Color GetColorByPercentige(double percentage)
		{
			return HSVtoRGB(120 * percentage, 0.9, 0.8);
		}

		public static Color HSVtoRGB(double h, double s, double v)
		{
			int i;
			double f, p, q, t;
			double r, g, b;
			Console.WriteLine($"{h} {s} {v}");

			if (s == 0)
			{
				// achromatic (grey)
				r = g = b = v;
				return Color.FromArgb(0xC8, Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
			}

			h /= 60;      // sector 0 to 5
			i = (int)Math.Floor(h);
			f = h - i;      // factorial part of h
			p = v * (1 - s);
			q = v * (1 - s * f);
			t = v * (1 - s * (1 - f));

			switch (i)
			{
				case 0:
					r = v;
					g = t;
					b = p;
					break;
				case 1:
					r = q;
					g = v;
					b = p;
					break;
				case 2:
					r = p;
					g = v;
					b = t;
					break;
				case 3:
					r = p;
					g = q;
					b = v;
					break;
				case 4:
					r = t;
					g = p;
					b = v;
					break;
				default:    // case 5:
					r = v;
					g = p;
					b = q;
					break;
			}
			Console.WriteLine($"{r} {g} {b}");

			return Color.FromArgb(0xC8, Convert.ToByte(r * 255), Convert.ToByte(g * 255), Convert.ToByte(b * 255));

		}
	}
}
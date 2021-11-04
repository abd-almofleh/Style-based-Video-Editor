namespace Style_based_Video_Editor_GUI.Classes
{
  internal static class Constants
  {
    public static string[] supportedVideoTypes = { "mp4" };

    public static string getString(string[] array)
    {
      return "[" + string.Join(",", array) + "]";
    }
  }
}
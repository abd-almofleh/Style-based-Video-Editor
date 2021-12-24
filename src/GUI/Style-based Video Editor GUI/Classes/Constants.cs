namespace Style_based_Video_Editor_GUI.Classes
{
  internal static class Constants
  {
    public static string[] SUPPORTED_VIDEO_TYPES = { "mp4" };

    public static int MAX_SHOTS = 5;
    public static string getString(string[] array)
    {
      return "[" + string.Join(",", array) + "]";
    }
  }
}
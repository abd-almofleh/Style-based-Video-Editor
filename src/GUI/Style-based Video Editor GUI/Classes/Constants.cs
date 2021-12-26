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
    public static string SERVER_URL = "http://127.0.0.1";
    public static int SERVER_PORT = 5000;

  }
}
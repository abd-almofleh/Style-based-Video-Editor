using System;

namespace Style_based_Video_Editor_GUI.Exceptions
{
  internal class VideoFileNotSupported : Exception
  {
    public VideoFileNotSupported(string notSuppotedType, string[] supportedTypes)
      : base($"File of type '{notSuppotedType}' is not supported. Supported files is {Classes.Constants.getString(supportedTypes)}")
    {
    }

    public VideoFileNotSupported(string notSuppotedType, string[] supportedTypes, Exception inner)
      : base($"File of type '{notSuppotedType}' is not supported. Supported files is {Classes.Constants.getString(supportedTypes)}", inner)
    {
    }
  }
}
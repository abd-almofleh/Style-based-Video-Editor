using System;


namespace Style_based_Video_Editor_GUI.Classes
{
    static class Helper
    {
       public static  void VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            e.VlcLibDirectory = new System.IO.DirectoryInfo(System.IO.Path.Combine(".", "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
        }
    }
}

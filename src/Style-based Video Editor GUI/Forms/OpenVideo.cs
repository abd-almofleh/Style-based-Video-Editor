using System;
using System.IO;
using System.Windows.Forms;

namespace Style_based_Video_Editor_GUI.Forms
{
  public partial class OpenVideo : Form
  {
    FileInfo VideoFile;
    public OpenVideo()
    {
      InitializeComponent();
    }


    private void Open_Click(object sender, System.EventArgs e)
    {
      OpenFileDialog OpenFile = new OpenFileDialog
      {
        Title = "Open video Files",

        CheckFileExists = true,
        CheckPathExists = true,

        DefaultExt = "MP4",
        Filter = "Video files (*.mp4)|*.mp4",
        FilterIndex = 2,
        RestoreDirectory = true,

        ReadOnlyChecked = true,
        ShowReadOnly = true
      };
      OpenFile.ShowDialog();
      if (OpenFile.FileName == "") return;
      VideoFile = new FileInfo(OpenFile.FileName);
      VideoPreview.SetMedia(VideoFile);
      VideoPreview.Play();
      VideoPreview.Video.IsMouseInputEnabled = false;
      VideoPreview.Video.IsKeyInputEnabled = false;
      StartEditing.Enabled = true;
      VideoPreview.Cursor = Cursors.Hand;
    }

    private void VideoPreview_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
    {
      Classes.Helper.VlcLibDirectoryNeeded(sender, e);

    }

    private void VideoPreview_Click(object sender, EventArgs e)
    {
      if (VideoPreview.GetCurrentMedia() == null) return;
      if (VideoPreview.IsPlaying) VideoPreview.Pause();
      else VideoPreview.Play();
    }
  }
}

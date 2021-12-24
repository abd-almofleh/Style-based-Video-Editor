using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Style_based_Video_Editor_GUI.Forms
{
  public partial class OpenVideo : Form
  {
    internal string VideoPath;

    public OpenVideo()
    {
      InitializeComponent();
    }

    private void Open_Click(object sender, System.EventArgs e)
    {
      string filter = Classes.Constants.supportedVideoTypes.Aggregate("", (prev, current) => prev + (prev != "" ? ";" : "") + "*." + current);

      OpenFileDialog OpenFile = new OpenFileDialog
      {
        Title = "Open video file...",

        CheckFileExists = true,
        CheckPathExists = true,

        DefaultExt = "MP4",
        Filter = $"Video files ({filter})|{filter}",
        FilterIndex = 2,
      };

      OpenFile.ShowDialog();

      if (OpenFile.FileName == "") return;

      Classes.Video VideoFile = new Classes.Video(OpenFile.FileName);
      VideoPath = OpenFile.FileName;
      PathText.Text = OpenFile.FileName;
      VideoPreview.SetMedia(VideoFile.video);
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
      Console.WriteLine("");
      if (VideoPreview.GetCurrentMedia() == null) return;
      if (VideoPreview.IsPlaying) VideoPreview.Pause();
      else VideoPreview.Play();
    }


    private void OpenVideo_FormClosing(object sender, FormClosingEventArgs e)
    {
      VideoPreview.Stop();
    }

    private void StartEditing_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }
}
using Style_based_Video_Editor_GUI.Classes;
using System;
using System.Windows.Forms;

namespace Style_based_Video_Editor_GUI.Forms
{
  public partial class Dashboard : Form
  {
    private Video SelectedVideo;

    public Dashboard()
    {
      InitializeComponent();
    }

    private void OpenFile_Click(object sender, EventArgs e)
    {
      OpenVideo openVideo = new OpenVideo();
      DialogResult dialogResult = openVideo.ShowDialog();
      if (dialogResult != DialogResult.OK) return;

      SelectedVideo = openVideo.VideoFile;
      VideoPath.Text = SelectedVideo.video.FullName;
      this.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " - " + SelectedVideo.video.Name;
      VideoPlayer.SetMedia(SelectedVideo.video);
      VideoPlayer.Play();
    }

    private void VideoPlayer_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
    {
      Helper.VlcLibDirectoryNeeded(sender, e);
    }

    private void PlayPause_Click(object sender, EventArgs e)
    {
      Console.WriteLine(VideoPlayer.Length);
      Console.WriteLine(VideoPlayer.Time);

      if (VideoPlayer.IsPlaying)
      {
        PlayPause.BackgroundImage = Properties.Resources.play_button;
        VideoPlayer.Pause();
      }
      else
      {
        if (VideoPlayer.Time > 20000) VideoPlayer.Time = 0;
        PlayPause.BackgroundImage = Properties.Resources.pause_button;
        VideoPlayer.Play();
      }

    }

    private void VideoPlayer_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
    {
      Console.WriteLine(VideoPlayer.Length);
      Console.WriteLine(VideoPlayer.Time);

      PlayPause.BackgroundImage = Properties.Resources.pause_button;
    }
  }
}
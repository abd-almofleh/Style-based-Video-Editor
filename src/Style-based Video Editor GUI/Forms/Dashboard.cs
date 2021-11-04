using Style_based_Video_Editor_GUI.Classes;
using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.Threading;

namespace Style_based_Video_Editor_GUI.Forms
{
  public partial class Dashboard : Form
  {
    public LibVLC _libVLC;
    public MediaPlayer _mp;
    public Media media;

    private Video SelectedVideo;

    public Dashboard()
    {
      InitializeComponent();
      Core.Initialize();
      _libVLC = new LibVLC();
      _mp = new MediaPlayer(_libVLC);
      VideoPlayer.MediaPlayer = _mp;
      _mp.Stopped += (object sender, EventArgs e) => { PlayPause.BackgroundImage = Properties.Resources.play_button; };
    }



    private void OpenFile_Click(object sender, EventArgs e)
    {
      OpenVideo openVideo = new OpenVideo();
      DialogResult dialogResult = openVideo.ShowDialog();
      if (dialogResult != DialogResult.OK) return;

      PreviewVideo(openVideo.VideoPath);
    }

    private void PlayPause_Click(object sender, EventArgs e)
    {
      Console.WriteLine(_mp.State);

      switch (_mp.State)
      {
        case VLCState.Playing:
          PlayPause.BackgroundImage = Properties.Resources.play_button;
          _mp.Pause();
          break;
        case VLCState.Paused:
          PlayPause.BackgroundImage = Properties.Resources.pause_button;
          _mp.Play();
          break;
        case VLCState.Stopped:
        case VLCState.Ended:
          if (SelectedVideo == null) break;
          PlayPause.BackgroundImage = Properties.Resources.pause_button;
          _mp.Media = new Media(_libVLC, SelectedVideo.video.FullName);
          _mp.Play();

          break;

        default:
          break;
      }
    }

    private void OpenExample_Click(object sender, EventArgs e)
    {
      string path = @"D:\Videos\SYRIAN Hardcore Memes (2).mp4";
      PreviewVideo(path);
    }
    private void PreviewVideo(string path)
    {
      SelectedVideo = new Video(path);
      PlayerGroup.Text = SelectedVideo.video.Name.Substring(0, SelectedVideo.video.Name.LastIndexOf('.')) + " - " + "preview";
      Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " - " + SelectedVideo.video.Name;
      DetectScenes();

      PlayPause.BackgroundImage = Properties.Resources.pause_button;
      _mp.Media = new Media(_libVLC, SelectedVideo.video.FullName);
      _mp.Play();

    }
    private void DetectScenes()
    {
      Label detecting = new Label
      {
        Font = new System.Drawing.Font("Microsoft Sans Serif", 18, System.Drawing.FontStyle.Bold),
        Text = "Detecting...",
        AutoSize = true
      };
      Scenes.Controls.Add(detecting);
      detecting.Left = (Scenes.Width / 2) - (detecting.Width / 2);
      detecting.Top = (Scenes.Height / 2) - (detecting.Height / 2);
      detecting.BringToFront();
      new Thread(() =>
      {
        Scenes.Invoke((MethodInvoker)delegate
        {
          SelectedVideo.detectScenes();
          Scenes.Controls.Clear();
        });
        Console.WriteLine("clear");
      }).Start();
    }
  }
}
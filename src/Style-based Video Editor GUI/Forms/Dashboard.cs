using Style_based_Video_Editor_GUI.Classes;
using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.Threading;
using System.Drawing;

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
      ScenesGroup.Controls.Clear();
      Label detecting = new Label
      {
        Font = new System.Drawing.Font("Microsoft Sans Serif", 18, System.Drawing.FontStyle.Bold),
        Text = "Detecting...",
        AutoSize = true
      };
      ScenesGroup.Controls.Add(detecting);
      detecting.Left = (ScenesGroup.Width / 2) - (detecting.Width / 2);
      detecting.Top = (ScenesGroup.Height / 2) - (detecting.Height / 2);
      detecting.BringToFront();
      new Thread(() =>
      {
        SelectedVideo.detectScenes();
        ScenesGroup.Invoke((MethodInvoker)delegate
        {
          FlowLayoutPanel panel = new FlowLayoutPanel
          {
            AutoScroll = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Dock = DockStyle.Fill,
          };
          ScenesGroup.Controls.Clear();
          ScenesGroup.Controls.Add(panel);

          foreach (var item in SelectedVideo.scenes)
          {
            FlowLayoutPanel colmne = new FlowLayoutPanel
            {
              AutoScroll = false,
              FlowDirection = FlowDirection.TopDown,
              WrapContents = false,
              Height = (int)(panel.Height - panel.Height * 0.2),
              AutoSize = true,
              BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox p = new PictureBox();
            p.Image = Image.FromFile(item.Image.FullName);
            p.SizeMode = PictureBoxSizeMode.Zoom;
            p.Height = (int)(panel.Height - panel.Height * 0.2);

            Label label = new Label();
            label.Text = "Scene " + item.SceneNumber;
            label.Width = p.Width;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Microsoft Sans Serif", 12);

            colmne.Controls.Add(label);
            colmne.Controls.Add(p);
            panel.Controls.Add(colmne);


          }
        });
      }).Start();
    }
  }
}
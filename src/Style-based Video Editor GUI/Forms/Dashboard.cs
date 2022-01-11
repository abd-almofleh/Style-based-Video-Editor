﻿using Style_based_Video_Editor_GUI.Classes;
using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Style_based_Video_Editor_GUI.Forms
{
  public partial class Dashboard : Form
  {
    public LibVLC _libVLC;
    public MediaPlayer _mp;
    public Media media;

    private Video SelectedVideo;

    private Video[] exampleVideos;
    public Dashboard()
    {
      InitializeComponent();
      Core.Initialize();
      _libVLC = new LibVLC();
      _mp = new MediaPlayer(_libVLC);
      VideoPlayer.MediaPlayer = _mp;
      _mp.Stopped += (object sender, EventArgs e) => { PlayPause.BackgroundImage = Properties.Resources.play_button; };
      _mp.EndReached += (object sender, EventArgs e) => { PlayPause.BackgroundImage = Properties.Resources.play_button; };
    }

    private void OpenFile_Click(object sender, EventArgs e)
    {
      OpenVideo openVideo = new OpenVideo();
      DialogResult dialogResult = openVideo.ShowDialog();
      if (dialogResult != DialogResult.OK) return;

      SelectVideo(openVideo.VideoPath);
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
        case VLCState.NothingSpecial:
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
      SelectVideo(exampleVideos[0]);

    }

    private void SelectVideo(string path)
    {
      closeVideo();
      SelectedVideo = new Video(path);
      Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " - " + SelectedVideo.video.Name;
      PreviewVideo();
      DetectScenes();


    }
    private void SelectVideo(Video videoFile)
    {
      closeVideo();
      SelectedVideo = videoFile;
      Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " - " + SelectedVideo.video.Name;
      PreviewVideo();
      DetectScenes();


    }

    private void PreviewVideo()
    {
      SceneVideoPreview.Text = SelectedVideo.video.Name.Substring(0, SelectedVideo.video.Name.LastIndexOf('.')) + " - Preview";
      PlayPause.BackgroundImage = Properties.Resources.play_button;
      _mp.Media = new Media(_libVLC, SelectedVideo.video.FullName);
      SceneVideoPreview.Controls.Remove(ScenePreviewMessage);
      ScenePreviewMessage.SendToBack();
      //_mp.Play();

    }
    private void PreviewVideo(Scene scene)
    {
      if (_mp.State != VLCState.Ended) _mp.Stop();
      SceneInfo.Controls.Remove(SceneInfoMessage);

      SceneNumber.Text = scene.SceneNumber.ToString("000");
      StartTime.Text = scene.StartTime.ToString(@"mm\:ss");
      EndTime.Text = scene.EndTime.ToString(@"mm\:ss");
      Length.Text = (scene.EndTime - scene.StartTime).ToString(@"mm\:ss");
      StartFrame.Text = scene.StartFrame.ToString();
      EndFrame.Text = scene.EndFrame.ToString();
      SceneImagePreview.Image = Image.FromFile(scene.Image.FullName);
      _mp.Media = new Media(_libVLC, scene.Video.FullName);
      if (AutoPlay.Checked)
      {
        _mp.Play();
        PlayPause.BackgroundImage = Properties.Resources.pause_button;
      }
      else
        PlayPause.BackgroundImage = Properties.Resources.play_button;


    }

    private void DetectScenes()
    {
      ScenesGroup.Controls.Clear();
      Label detecting = new Label
      {
        Font = new Font("Microsoft Sans Serif", 18, System.Drawing.FontStyle.Bold),
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
            UserContoles.Scene scenePanel = new UserContoles.Scene(item, panel.Height);
            scenePanel.Click += ScenePanelClick;
            panel.Controls.Add(scenePanel);
          }
          closeVideoToolStripMenuItem.Enabled = true;

        });
      }).Start();
    }

    private void ScenePanelClick(object sender, EventArgs e)
    {
      Scene selectedScene = ((UserContoles.Scene)sender).scene;
      PreviewVideo(selectedScene);
    }

    private void Dashboard_Load(object sender, EventArgs e)
    {
      // ! for debugging only
      //OpenExample.PerformClick();
      LoadExample();
    }
    private void closeVideo()
    {
      SceneVideoPreview.Controls.Add(ScenePreviewMessage);
      SceneInfo.Controls.Add(SceneInfoMessage);
      SceneInfoMessage.BringToFront();
      ScenePreviewMessage.BringToFront();
      _mp.Stop();
      _mp.Media = null;
      SceneImagePreview.Image = null;
      ScenesGroup.Controls.Clear();
      ScenesGroup.Controls.Add(ScenesListMessage);
      GC.Collect();
      SelectedVideo = null;
      closeVideoToolStripMenuItem.Enabled = false;


    }

    private void closeVideoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      closeVideo();
    }
    private void LoadExample()
    {
      DirectoryInfo videoDirectory = new DirectoryInfo(@"./Example Videos");
      if (!videoDirectory.Exists) return;
      FileInfo[] files = Constants.supportedVideoTypes.SelectMany(ext => videoDirectory.GetFiles("*." + ext)).ToArray();
      exampleVideos = new Video[files.Length];
      for (int i = 0; i < files.Length; i++)
      {
        exampleVideos[i] = new Video(files[i]);
        ToolStripMenuItem example = new ToolStripMenuItem($"Open {exampleVideos[i].video.Name}");
        example.Tag = i;

        example.Click += (object sender, EventArgs e) =>
        {
          int index = (int)((ToolStripMenuItem)sender).Tag;
          SelectVideo(exampleVideos[index]);
        };
        OpenExample.DropDownItems.Add(example);
      }
      if (exampleVideos.Length > 0) OpenExample.Enabled = true;
    }

    private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
    {
      _mp.Stop();
    }
  }
}
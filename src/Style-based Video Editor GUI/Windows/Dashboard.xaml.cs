using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Style_based_Video_Editor_GUI.Classes;
using System.IO;
using System.Threading;

namespace Style_based_Video_Editor_GUI.Windows
{
  /// <summary>
  /// Interaction logic for Dashboard.xaml
  /// </summary>
  public partial class Dashboard : Window
  {
    bool _isPlaying = false;
    bool isDragging = false;
    DispatcherTimer timer;
    private Video [] videos;

    private int SelectedVideo;
    private int SelectedScene;

    public bool IsPlaying
    {
      get
      {
        return _isPlaying;
      }
      set
      {
        if (value)
        {
          PlayPause.Content = "Pause";
          timer.Start();
          VideoPlayer.Play();
        }
        else
        {
          PlayPause.Content = "Play";
          timer.Stop();
          VideoPlayer.Pause();

        }
        _isPlaying = value;
      }
    }

    public Dashboard()
    {
      InitializeComponent();
      timer = new DispatcherTimer();
      IsPlaying = false;
      timer.Interval = TimeSpan.FromMilliseconds(200);
      timer.Tick += Timer_Tick;
    }


    private void Open_Click(object sender, RoutedEventArgs e)
    {
      OpenVideos openWindow = new OpenVideos();
      bool? x = openWindow.ShowDialog();
      if (x == null || x == false) return;

      videos = openWindow.Videos.ToArray();
      PreviewVideos();
    }
    void PreviewVideos()
    {
      for (int i = 0; i < videos.Length; i++)
      {
        VideoGrid.RowDefinitions.Add(new RowDefinition());
        Contorles.VideoPreview videoPreview = new Contorles.VideoPreview($"Video {i + 1}", videos[i].thumbnail, i,-1);
        videoPreview.SetValue(Grid.RowProperty, i+1);
        Label detecting = new Label
        {
          FontFamily = new FontFamily("Times New Roman"),
          FontSize = 24,
          FontWeight = FontWeights.Bold,
          Content = "Detecting...",
          HorizontalAlignment = HorizontalAlignment.Stretch,
          VerticalAlignment = VerticalAlignment.Stretch,
          HorizontalContentAlignment = HorizontalAlignment.Center,
          VerticalContentAlignment = VerticalAlignment.Center
        };
        detecting.SetValue(Grid.ColumnProperty, 1);
        detecting.SetValue(Grid.RowProperty, i + 1);

        VideoGrid.Children.Add(videoPreview);
        VideoGrid.Children.Add(detecting);


        Thread t = new Thread(detect);
        t.IsBackground = true;
        t.Start(i);
      }

    }
    readonly object OoO = new object();
    readonly object oOo = new object();
    void detect(object param)
    {
      int index = (int)param;
      videos[index].detectScenes();
      this.Dispatcher.Invoke(()=>
      {
        Grid g = new Grid();
        g.ShowGridLines = true;
        lock (OoO){
          foreach (object elemet in VideoGrid.Children)
          {
            Label l = elemet as Label;
            if (l == null) continue;
            if ((int)l.GetValue(Grid.ColumnProperty) == 1 && (int)l.GetValue(Grid.RowProperty) == index + 1)
            {
              VideoGrid.Children.Remove(l);
              VideoGrid.Children.Add(g);
              g.SetValue(Grid.ColumnProperty, 1);
              g.SetValue(Grid.RowProperty, index + 1);
              break;
            }
          }
        }
        Scene [] Scenes = videos[index].scenes;
        for (int i = 0; i < Scenes.Length; i++)
        {
          ColumnDefinition c = new ColumnDefinition();
          c.Width = new GridLength(100, GridUnitType.Pixel); ;
          g.ColumnDefinitions.Add(c);
          Contorles.VideoPreview videoPreview = new Contorles.VideoPreview("", Scenes[i].Image,index, i);
          
          videoPreview.SetValue(Grid.ColumnProperty, i);
          g.Children.Add(videoPreview);
        }

        lock (oOo)
        {
          int SceneCount = Scenes.Length;
          int labelsCount = ScenesLabels.Children.Count;
          if (SceneCount > labelsCount)
            for (int i = labelsCount ; i < SceneCount; i++)
            {
              ColumnDefinition c = new ColumnDefinition();
              c.Width = new GridLength(100, GridUnitType.Pixel); ;
              ScenesLabels.ColumnDefinitions.Add(c);

              Label ll = new Label
              {
                Content = $"Scene { i + 1}",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 14,
                FontWeight = FontWeights.Bold
              };
              ll.SetValue(Grid.ColumnProperty, i);
              ScenesLabels.Children.Add(ll);

            }
        }
      });
    }


    internal void LoadVideo(int videoIndex, int sceneIndex)
    {
      VideoInfoMessage.Visibility = Visibility.Collapsed;
      Info.Visibility = Visibility.Visible;
      SelectedVideo = videoIndex;
      SelectedScene = sceneIndex;
      if (SelectedScene == -1)
      {
        VideoPlayer.Source = new Uri(videos[SelectedVideo].video.FullName);
        SceneInfo.Visibility = Visibility.Collapsed;
        SceneTag.Visibility = Visibility.Collapsed;
        VideoInfo.IsSelected = true;
      }
      else
      {
        Scene scene = videos[SelectedVideo].scenes[SelectedScene];

        SceneInfo.Visibility = Visibility.Visible;
        SceneInfo.IsSelected = true;

        VideoPlayer.Source = new Uri(scene.Video.FullName);
        SceneNumber.Content = sceneIndex + 1;
        SceneNumber.Content = scene.SceneNumber.ToString("000");
        StartTime.Content = scene.StartTime.ToString(@"mm\:ss");
        EndTime.Content = scene.EndTime.ToString(@"mm\:ss");
        Length.Content = (scene.EndTime - scene.StartTime).ToString(@"mm\:ss");
        StartFrame.Content = scene.StartFrame.ToString();
        EndFrame.Content = scene.EndFrame.ToString();
        Tags.RowDefinitions.Clear();
        Tags.Children.Clear();
        SceneTag.Header = $"Scene Tags";
        SceneTag.Visibility = Visibility.Visible;

        if (scene.Objects == null)
          scene.DetectObjects(this);
        else
          showTags(scene.Objects);
      }
      VideoNumber.Content = SelectedVideo + 1;


    }
    internal void showTags(List<Structs.Tag> tags)
    {
      Tags.RowDefinitions.Clear();
      Tags.Children.Clear();
      SceneTag.Header = $"Scene Tags ({tags.Count()} tags)";
      SceneTag.Visibility = Visibility.Visible;
      RowDefinition row = new RowDefinition();
      row.Height = new GridLength(25, GridUnitType.Pixel); ;
      Tags.RowDefinitions.Add(row);

      Label label = new Label
      {
        Content = "Tag",
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        FontSize = 14f,
        FontWeight = FontWeights.Bold
      };
      Tags.Children.Add(label);

      label = new Label
      {
        Content = "Score",
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        FontSize = 14f,
        FontWeight = FontWeights.Bold

      };
      label.SetValue(Grid.ColumnProperty, 1);
      Tags.Children.Add(label);


      foreach (var tag in tags)
      {
        RowDefinition c = new RowDefinition();
        c.Height = new GridLength(25, GridUnitType.Pixel); ;
        Tags.RowDefinitions.Add(c);
        Label key = new Label();
        key.Content = tag.tag;
        key.HorizontalAlignment = HorizontalAlignment.Center;
        key.VerticalAlignment = VerticalAlignment.Center;
        key.FontSize = 14f;
        key.SetValue(Grid.RowProperty, Tags.RowDefinitions.Count() - 1);
        Tags.Children.Add(key);
        Label value = new Label();
        value.Content = tag.score.ToString("0.00");
        value.HorizontalAlignment = HorizontalAlignment.Center;
        value.VerticalAlignment = VerticalAlignment.Center;
        value.FontSize = 14f;
        value.SetValue(Grid.RowProperty, Tags.RowDefinitions.Count() - 1);
        value.SetValue(Grid.ColumnProperty, 1);
        Tags.Children.Add(value);


      }
    }

    #region Player Controls
    private void Timer_Tick(object sender, EventArgs e)
    {
      if (!isDragging)
        Seek.Value = VideoPlayer.Position.TotalSeconds;

    }

    private void Seek_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
    {
      isDragging = true;
    }

    private void Seek_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
    {
      isDragging = false;
      VideoPlayer.Position = TimeSpan.FromSeconds(Seek.Value);

    }
    private void Mute_Click(object sender, RoutedEventArgs e)
    {
      Mute.Content = VideoPlayer.IsMuted ? "Mute" : "Unmute";
      VideoPlayer.IsMuted = !VideoPlayer.IsMuted;
    }

    private void PlayPause_Click(object sender, RoutedEventArgs e)
    {
      if (VideoPlayer.Source == null) return;

      IsPlaying = !IsPlaying;
    }

    private void VideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
    {
      if (VideoPlayer.NaturalDuration.HasTimeSpan)
      {
        TimeSpan ts = VideoPlayer.NaturalDuration.TimeSpan;
        Seek.Maximum = ts.TotalSeconds;
      }
      IsPlaying = true;
      VideoControles.IsEnabled = true;

    }
    #endregion Player Controls

    private void OpenExamples_Click(object sender, RoutedEventArgs e)
    {
      DirectoryInfo dir = new DirectoryInfo(@"./ExampleVideos");
      FileInfo [] files =  dir.GetFiles("*.mp4");
      int exampleCount = files.Length >= 5 ? 5 : files.Length;
      videos = new Video[exampleCount];
      for (int i = 0; i < videos.Length; i++)
      {
        videos[i] = new Video(files[i], Helper.GenerateThumbnail(files[i].FullName), new Duration());
      }
      PreviewVideos();
    }

  }


}

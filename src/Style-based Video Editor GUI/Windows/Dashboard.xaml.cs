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
    private View [] videos;

    private Video SelectedVideo;

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
        Contorles.VideoPreview videoPreview = new Contorles.VideoPreview($"Video {i + 1}", videos[i]);
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



      }
      Thread t = new Thread(detectOnSpeakerChange);
      t.IsBackground = true;
      t.Start();
    }
    readonly object OoO = new object();
    readonly object oOo = new object();

    void detectOnVisualChange(object param)
    {
      int index = (int)param;
      videos[index].DetectScenesOnVisualChange();
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
          Contorles.VideoPreview videoPreview = new Contorles.VideoPreview("", Scenes[i]);
          
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

    void detectOnSpeakerChange()
    {
      View.DetectScenesOnSpeakerChange(videos);
      
      this.Dispatcher.Invoke(() =>
      {
        if (videos[0].scenes == null) return; 
        
        ScenesLabels.Children.Clear();

        List<Label> toremove = new List<Label>();
        foreach (object elemet in VideoGrid.Children)
        {
          Label l = elemet as Label;
          if (l == null) continue;
          if ((int)l.GetValue(Grid.ColumnProperty) == 1 && (int)l.GetValue(Grid.RowProperty) > 0)
            toremove.Add(l);
        }
        foreach (Label item in toremove)
          VideoGrid.Children.Remove(item);

        int SceneCount = videos[0].scenes.Length;
        for (int i = 0; i < SceneCount; i++)
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

        for (int i = 0; i < videos.Length; i++)
        {
          Grid g = new Grid();
          g.ShowGridLines = true;
          VideoGrid.Children.Add(g);
          g.SetValue(Grid.ColumnProperty, 1);
          g.SetValue(Grid.RowProperty, i + 1);
          Scene[] Scenes = videos[i].scenes;
          for (int j = 0; j < Scenes.Length; j++)
          {
            ColumnDefinition c = new ColumnDefinition();
            c.Width = new GridLength(100, GridUnitType.Pixel); ;
            g.ColumnDefinitions.Add(c);
            Contorles.VideoPreview videoPreview = new Contorles.VideoPreview("", Scenes[j]);

            videoPreview.SetValue(Grid.ColumnProperty, j);
            g.Children.Add(videoPreview);
          }
        }
      });
    }
    internal void LoadVideo(Video video)
    {
      if (video == SelectedVideo) return;
      VideoInfoMessage.Visibility = Visibility.Collapsed;
      Info.Visibility = Visibility.Visible;
      SelectedVideo = video;
      if (video is Classes.View)
      {

        VideoPlayer.Source = new Uri(video.video.FullName);
        SceneInfo.Visibility = Visibility.Collapsed;
        SceneTag.Visibility = Visibility.Collapsed;
        PersonsImagesTab.Visibility = Visibility.Collapsed;
        VideoInfo.IsSelected = true;
        VideoNumber.Content = video.VideoNumber;

      }
      else
      {
        Scene scene = (Scene)video;

        VideoNumber.Content = scene.OriginalVideo.VideoNumber;

        SceneInfo.Visibility = Visibility.Visible;
        SceneInfo.IsSelected = true;

        VideoPlayer.Source = new Uri(scene.Video.FullName);
        SceneNumber.Content = scene.VideoNumber.ToString("000");
        StartTime.Content = scene.StartTime.ToString(@"mm\:ss");
        EndTime.Content = scene.EndTime.ToString(@"mm\:ss");
        Length.Content = scene.length.TimeSpan.ToString(@"mm\:ss");
        StartFrame.Content = scene.StartFrame.ToString();
        EndFrame.Content = scene.EndFrame.ToString();

        Tags.RowDefinitions.Clear();
        Tags.Children.Clear();
        PersonsImages.Children.Clear();
        PersonsImages.ColumnDefinitions.Clear();

        SceneTag.Header = $"Scene Tags";
        SceneTag.Visibility = Visibility.Visible;
        PersonsImagesTab.Header = "Scene Persons";
        PersonsImagesTab.Visibility = Visibility.Visible;

        if (scene.personImages == null)
          scene.DetectPersons(this);
        else
          ShowSceneFaces(scene);

        if (scene.Objects == null)
          scene.DetectObjects(this);
        else
          showTags(scene);
      }


    }
    internal void showTags(Scene scene)
    {
      if (scene != SelectedVideo) return;


      List<Structs.KeyScore> tags = scene.Objects;
      if (tags == null)
        return;
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
        key.Content = tag.key;
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

    internal void ShowSceneFaces(Scene scene)
    {
      if (scene != SelectedVideo) return;

      List<PersonImage> personImages = scene.personImages;
      if (personImages == null)
        return;
      PersonsImages.Children.Clear();
      PersonsImages.ColumnDefinitions.Clear();
      PersonsImagesTab.Header = $"Scene Persons ({personImages.Count()} persons)";
      PersonsImagesTab.Visibility = Visibility.Visible;

      for (int i = 0; i < personImages.Count; i++)
      {
        PersonImage perosnImage = personImages[i];
        ColumnDefinition c = new ColumnDefinition();
        c.Width = new GridLength(230, GridUnitType.Pixel); ;
        PersonsImages.ColumnDefinitions.Add(c);


        Image img = new Image();
        img.Source = new BitmapImage(new Uri(perosnImage.image.FullName));
        img.SetValue(Grid.RowProperty, 0);
        img.SetValue(Grid.ColumnProperty, i);
        PersonsImages.Children.Add(img);


        Grid grid = new Grid();
        grid.ShowGridLines = true;
        grid.SetValue(Grid.RowProperty, 1);
        grid.SetValue(Grid.ColumnProperty, i);
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        PersonsImages.Children.Add(grid);
        for (int j = 0; j < 4; j++)
        {
          RowDefinition r = new RowDefinition();
          r.Height = new GridLength(1, GridUnitType.Star);
          grid.RowDefinitions.Add(r);
        }
        RowDefinition rr = new RowDefinition();
        rr.Height = new GridLength(7, GridUnitType.Star);
        grid.RowDefinitions.Add(rr);
        // Score
        Label label = new Label();
        label.Content = "Score";
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.FontWeight = FontWeights.Bold;
        grid.Children.Add(label);

        label = new Label();
        label.Content = perosnImage.score.ToString("00.00%");
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.SetValue(Grid.RowProperty, 0);
        label.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(label);

        // Emotion
        label = new Label();
        label.Content = "Emotion";
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.FontWeight = FontWeights.Bold;
        label.SetValue(Grid.RowProperty, 1);
        grid.Children.Add(label);

        label = new Label();
        label.Content = perosnImage.dominant_emotion;
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.SetValue(Grid.RowProperty, 1);
        label.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(label);

        // Age
        label = new Label();
        label.Content = "Age";
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.FontWeight = FontWeights.Bold;
        label.SetValue(Grid.RowProperty, 2);
        grid.Children.Add(label);

        label = new Label();
        label.Content = perosnImage.age;
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.SetValue(Grid.RowProperty, 2);
        label.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(label);

        // Age
        label = new Label();
        label.Content = "Gender";
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.FontWeight = FontWeights.Bold;
        label.SetValue(Grid.RowProperty, 3);
        grid.Children.Add(label);

        label = new Label();
        label.Content = perosnImage.gender;
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.SetValue(Grid.RowProperty, 3);
        label.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(label);

        // Emotions
        label = new Label();
        label.Content = "Emotions";
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Center;
        label.FontSize = 14f;
        label.FontWeight = FontWeights.Bold;
        label.SetValue(Grid.RowProperty, 4);
        grid.Children.Add(label);


        StackPanel s = new StackPanel();
        s.Orientation = Orientation.Vertical;
        s.SetValue(Grid.RowProperty, 4);
        s.SetValue(Grid.ColumnProperty, 1);
        s.HorizontalAlignment = HorizontalAlignment.Stretch;
        s.VerticalAlignment = VerticalAlignment.Stretch;
        grid.Children.Add(s);
        foreach (Structs.KeyScore item in perosnImage.emotions)
        {
          label = new Label();
          label.Content = $"{item.key} ({item.score/100:P})";
          label.HorizontalAlignment = HorizontalAlignment.Center;
          label.VerticalAlignment = VerticalAlignment.Center;
          label.FontSize = 14f;
          s.Children.Add(label);
        }


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
      videos = new View[exampleCount];
      for (int i = 0; i < videos.Length; i++)
      {
        videos[i] = new View(files[i], Video.GenrateImage(files[i].FullName,7), new Duration());
      }
      PreviewVideos();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      OpenExamples_Click(null, null);
    }
  }


}

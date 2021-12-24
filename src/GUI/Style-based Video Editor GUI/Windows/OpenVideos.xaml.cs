using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Style_based_Video_Editor_GUI.Windows
{
  /// <summary>
  /// Interaction logic for OpenVideos.xaml
  /// </summary>
  public partial class OpenVideos : Window
  {
    bool _isPlaying = false;
    bool isDragging = false;
    DispatcherTimer timer;
    List<Classes.Video> videos = new List<Classes.Video>(Classes.Constants.MAX_SHOTS); 
    public bool IsPlaying { 
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
    
    public OpenVideos()
    {
      InitializeComponent();
      timer = new DispatcherTimer();
      IsPlaying = false;
      timer.Interval = TimeSpan.FromMilliseconds(200);
      timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      if (!isDragging)
        Seek.Value = VideoPlayer.Position.TotalSeconds;
      
    }

    private void Open_Click(object sender, RoutedEventArgs e)
    {
      goto A;
      string[] s = {
        "Facebook 262328884292048",
        "Facebook 263415364135135",
        "Facebook 274891436369126",
        "Facebook 324464314745171",
        "Facebook 789740457864064",
      };
      for (int i = 0; i < 5; i++)
      {
        string videoPath = $@"G:\UN\conputer\Videos\Funny\{s[i]}.mp4";
        Shots.ColumnDefinitions.Add(new ColumnDefinition());
        FileInfo Thumbnail = Classes.Helper.GenerateThumbnail(videoPath);
        Contorles.Shot shot = new Contorles.Shot(Thumbnail, "".ToString(), videos.Count);
        shot.SetValue(Grid.ColumnProperty, videos.Count);
        Shots.Children.Add(shot);
        videos.Add(new Classes.Video(videoPath, Thumbnail));

      }
      return;
      A:
      string filter = Classes.Constants.SUPPORTED_VIDEO_TYPES.Aggregate("", (prev, current) => prev + (prev != "" ? ";" : "") + "*." + current);

      OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Title = "Open video file...",

        CheckFileExists = true,
        CheckPathExists = true,

        Filter = $"Video files ({filter})|{filter}",
      };
      if (openFileDialog.ShowDialog() == false) 
        return;
      
      VideoPath.Text = openFileDialog.FileName;
      Add.IsEnabled = true;
      VideoPlayer.Source = new Uri( VideoPath.Text);


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

    private void Seek_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
    {
      isDragging = true;
    }

    private void Seek_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
    {
      isDragging = false;
      VideoPlayer.Position = TimeSpan.FromSeconds(Seek.Value);

    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
      if (videos.Count >= Classes.Constants.MAX_SHOTS)
      {
        MessageBox.Show("You can select only 5 shots.");
        return;
      }
      try
      {
        Shots.ColumnDefinitions.Add( new ColumnDefinition());
        FileInfo Thumbnail = Classes.Helper.GenerateThumbnail(VideoPath.Text);
        Contorles.Shot shot = new Contorles.Shot( Thumbnail, VideoPlayer.NaturalDuration.TimeSpan.ToString(), videos.Count);
        shot.SetValue(Grid.ColumnProperty, videos.Count);
        Shots.Children.Add(shot);
        videos.Add(new Classes.Video(VideoPath.Text, Thumbnail));

        VideoPlayer.Source = null;
        PlayPause.Content = "Play";
        Seek.Value = 0;
        VideoPath.Text = "";
        Mute.Content = "Mute";
        VideoPlayer.IsMuted = VideoControles.IsEnabled = Add.IsEnabled = false;
        Finish.IsEnabled = videos.Count > 0;

      }
      catch (Exception error)
      {
        MessageBox.Show(error.Message);
        Console.WriteLine(error.StackTrace);
        Console.WriteLine(error.Message);
      }

    }
    public void DeleteShot(int index)
    {
      Shots.Children.RemoveAt(index);
      Shots.ColumnDefinitions.RemoveAt(index);

      videos.RemoveAt(index);
      for (int i = index; i < Shots.Children.Count; i++)
      {
        ((Contorles.Shot)Shots.Children[i]).UpdateIndex(i);
        ((Contorles.Shot)Shots.Children[i]).SetValue(Grid.ColumnProperty, i);

      }
      Finish.IsEnabled = videos.Count > 0;
    }

    private void Mute_Click(object sender, RoutedEventArgs e)
    {
      Mute.Content = VideoPlayer.IsMuted ? "Mute" : "Unmute";
      VideoPlayer.IsMuted = !VideoPlayer.IsMuted;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      videos.Clear();
      this.DialogResult = false;
      Close();
    }

    private void Finish_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
      Close();

    }
  }



}

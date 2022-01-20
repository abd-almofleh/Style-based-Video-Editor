﻿using System;
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

namespace Style_based_Video_Editor_GUI.Contorles
{
  /// <summary>
  /// Interaction logic for Shot.xaml
  /// </summary>
  public partial class VideoPreview : UserControl
  {

    Classes.Video video;
    
    internal VideoPreview(string title, Classes.Video video)
    {
      InitializeComponent();
      if (title == "")
      {
        Title.Visibility = Visibility.Collapsed;
        PreviewImage.SetValue(Grid.RowProperty, 0);
        Grid.SetRowSpan(PreviewImage, 2);
      }
      else
        this.Title.Content = title;
      this.video = video;
      this.PreviewImage.Source = new BitmapImage(new Uri(video.image.FullName));
    }

    private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
      ((Windows.Dashboard)Window.GetWindow(this)).LoadVideo(video);

    }
  }
}

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

namespace Style_based_Video_Editor_GUI.Contorles
{
  /// <summary>
  /// Interaction logic for Shot.xaml
  /// </summary>
  public partial class Shot : UserControl
  {
    int index;
    public Shot(System.IO.FileInfo Thumbnail, string Length, int index)
    {
      InitializeComponent();
      this.Title.Content = $"Shot {index + 1}";
      this.PreviewImage.Source = new BitmapImage(new Uri(Thumbnail.FullName));
      this.Length.Content = Length;
      this.index = index;
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
      ((Windows.OpenVideos) Window.GetWindow(this)).DeleteShot(index);
    }
    public void UpdateIndex(int index)
    {
      this.Title.Content = Title.Content = $"Shot {index + 1}";
      this.index = index;


    }
  }
}

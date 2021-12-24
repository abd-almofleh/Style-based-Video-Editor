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

namespace Style_based_Video_Editor_GUI.Windows
{
  /// <summary>
  /// Interaction logic for Dashboard.xaml
  /// </summary>
  public partial class Dashboard : Window
  {

    public Dashboard()
    {
      InitializeComponent();
    }

    private void Open_Click(object sender, RoutedEventArgs e)
    {
      OpenVideos openWindow = new OpenVideos();
      bool? x = openWindow.ShowDialog();


    }
  }
}

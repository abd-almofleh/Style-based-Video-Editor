using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Style_based_Video_Editor_GUI.UserContoles
{
  public partial class Scene : UserControl
  {
    public readonly Classes.Scene scene;
    public Scene(Classes.Scene scene, float height)
    {
      InitializeComponent();
      Title.Text = "Scene " + scene.SceneNumber;
      ImageBox.Image = Image.FromFile(scene.Image.FullName);
      ImageBox.Height = (int)(height - height * 0.65);
      Title.Width = ImageBox.Width;
      this.scene = scene;
      ImageBox.Click += eventClick;
      flowLayoutPanel1.Click += eventClick;

    }

    private void eventClick(object sender, EventArgs e)
    {
      this.OnClick(new EventArgs());
    }
  }
}

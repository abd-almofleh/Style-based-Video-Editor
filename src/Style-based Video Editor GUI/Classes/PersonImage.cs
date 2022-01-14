using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Classes
{
  class PersonImage
  {
    public FileInfo image;
    public double score;
    public int[] FacialArea;
    public double[] LeftEye;
    public double[]mouth_left;
    public double[] mouth_right;
    public double[] right_eye;

    public PersonImage(string image, double score, int[] FacialArea, double[] LeftEye, double[] mouth_left, double[] mouth_right, double[] nose, double[] right_eye):this(new FileInfo( image), score, FacialArea, LeftEye, mouth_left, mouth_right, nose, right_eye)
    {
      
    }
    public PersonImage(FileInfo image, double score, int[] FacialArea, double[] LeftEye, double[] mouth_left, double[] mouth_right, double[] nose, double[] right_eye)
    {
      this.image = image;
      this.score = score;
      this.FacialArea = FacialArea;
      this.LeftEye = LeftEye;
      this.mouth_left = mouth_left;
      this.mouth_right = mouth_right;
      this.right_eye = right_eye;
    }
  }
}

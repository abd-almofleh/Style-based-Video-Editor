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
    public double[] nose;
    public double[] right_eye;
    public int age;
    public string dominant_emotion;
    public Structs.KeyScore[] emotions;
    public string gender;
    
    public PersonImage(string image, double score, int age, string dominant_emotion, string gender, Structs.KeyScore[] emotions, int[] FacialArea, double[] LeftEye, double[] mouth_left, double[] mouth_right, double[] nose, double[] right_eye):this(new FileInfo( image), score,age,dominant_emotion,gender,emotions, FacialArea, LeftEye, mouth_left, mouth_right, nose, right_eye)
    {
      
    }
    public PersonImage(FileInfo image, double score,int age,string dominant_emotion, string gender,Structs.KeyScore[] emotions, int[] FacialArea, double[] LeftEye, double[] mouth_left, double[] mouth_right, double[] nose, double[] right_eye)
    {
      this.image = image;
      this.score = score;
      this.FacialArea = FacialArea;
      this.LeftEye = LeftEye;
      this.mouth_left = mouth_left;
      this.mouth_right = mouth_right;
      this.nose = nose;
      this.right_eye = right_eye;
      this.age = age;
      this.dominant_emotion = dominant_emotion;
      this.gender = gender;
      this.emotions = emotions;
    }
  }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Classes
{
  class Person
  {
    static int counter = 1;
    public static int id {
      get { return counter++; }
    }
    string name;
    string dbPath;
    List<FileInfo> images = new List<FileInfo>();
    List<Scene> appearances = new List<Scene>();
    public Person()
    {
      this.name = $"Person {id}";
      this.dbPath = $"./database/{name}";
    }
    public Person(string image):this()
    {
      images.Add(new FileInfo(image));
    }
    public Person(string image,Scene scene) : this(image)
    {
      appearances.Add(scene);
    }
    public override string ToString()
    {
      return $"Name: {name}, Number of Images: {images.Count}, Number of Appearances in Scenes: {appearances.Count}";
    }
  }
}

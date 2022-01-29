using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Classes
{
  class SceneInfo
  {
    public List<Classes.Scene>[] scenes;
    public Classes.Script[] scripts;
    public SceneInfo(List<Classes.Scene>[] scenes, Classes.Script[] scripts)
    {
      this.scenes = scenes;
      this.scripts = scripts;
    }

  }
}

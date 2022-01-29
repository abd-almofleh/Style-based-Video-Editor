using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Classes
{
  class SceneInfo
  {
    public List<Scene>[] scenes;
    public Script[] scripts;
    public SceneInfo(List<Scene>[] scenes, Script[] scripts)
    {
      this.scenes = scenes;
      this.scripts = scripts;
    }

  }
}

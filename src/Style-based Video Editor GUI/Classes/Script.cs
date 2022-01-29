using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Classes
{
  public class Script
  {
    public string arabic;
    public string OriginalArabic;
    public double score;
    public Script(string arabic,double score)
    {
      this.arabic = arabic;
      this.score = score;
    }
    public void Update(string text)
    {
      if (text == arabic) return;
      if (text == OriginalArabic)
      {
        OriginalArabic = null;
        arabic = text;
      }
      else
        if (OriginalArabic == null)
          OriginalArabic = arabic;
      arabic = text;
    }
  }
}

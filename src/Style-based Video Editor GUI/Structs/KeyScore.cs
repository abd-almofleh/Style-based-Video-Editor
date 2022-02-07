using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Structs
{
    struct KeyScore
    {
        public string key;
        public double score;
        public Dictionary<string, double> ExtraValues;
        public KeyScore(string key, double score, Dictionary<string,double> d = null)
        {
            this.key = key;
            this.score = score;
            if (d != null)
              ExtraValues = d;
            else
              ExtraValues = new Dictionary<string, double>();
        }
        public override string ToString()
        {
          return $"Key: {this.key}, Score: {this.score}";
        }
  }
}

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
        public KeyScore(string key, double score)
        {
            this.key = key;
            this.score = score;
        }
        public override string ToString()
        {
          return $"Key: {this.key}, Score: {this.score}";
        }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Structs
{
    struct Tag
    {
        public string tag;
        public double score;
        public Tag(string tag, double score)
        {
            this.tag = tag;
            this.score = score;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Style_based_Video_Editor_GUI.Classes
{
  class CMDResult
  {
    public readonly string Command;
    public readonly string OutputMessage;
    public readonly int ExitCode;
    
    public CMDResult(string Command, string OutputMessage, int ExitCode)
    {
      this.Command = Command;
      this.OutputMessage = OutputMessage;
      this.ExitCode = ExitCode;
    }
  }
}

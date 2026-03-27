using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.Core.AvaloniaGUI.Models
{
    public class Command
    {
        public string Name { get; set; }
        public string HelpText { get; set; }
        public Action Action { get; set; }
    }
}

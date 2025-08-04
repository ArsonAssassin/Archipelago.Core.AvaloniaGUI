using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.Core.AvaloniaGUI.Models
{
    public class ArchipelagoCommandEventArgs : EventArgs
    {
        public string Command { get; set; }
    }
}

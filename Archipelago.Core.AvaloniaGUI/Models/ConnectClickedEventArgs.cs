using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archipelago.Core.AvaloniaGUI.Models
{
    public class ConnectClickedEventArgs : EventArgs
    {
        public string Host { get; set; }
        public string Slot { get; set; }
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummonersWar.Model
{
    public class Script_data
    {
        public Image img { get; set; }
        public int delaytime { get; set; }
        public bool IsForced { get; set; }
        public Point SearchPoints { get; set; }
    }
}

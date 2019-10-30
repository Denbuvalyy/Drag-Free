using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicObjects8
{    
    [Serializable]
   public abstract class Shape
    {        
        public abstract void Draw(PaintEventArgs e);
    }
}

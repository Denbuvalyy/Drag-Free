using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicObjects8
{
    /// <summary>
    /// class that holds game state
    /// </summary>
    [Serializable]
    public class GameDocument
    {
        public string UserName { get; set; }
        public bool Intersection { get; set; }

        public List<Shape> Shapes = new List<Shape>();
    }
}

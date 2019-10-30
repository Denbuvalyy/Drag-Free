using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicObjects8
{
    [Serializable]
    class Circle:Shape
    {
        /// <summary>
        /// defining class Circle, it consists of: 
        /// fields for center of the cirlce and size of the circle
        /// properties that contain two lines conected to the circle
        /// </summary>
        public Point centCircle;
        public Size sizeCircle;
        public Line BackLine
        { get; set; }
        public Line FrontLine
        { get; set; }  

        public Circle(Size sizeCircle, Point centCircle)
        {
            this.centCircle = centCircle;
            this.sizeCircle = sizeCircle;
        }
        /// <summary>
        /// method that checks if certain point is whithin the circle
        /// </summary>       
        public bool Contains(Point currentPoint)
        {
            if (Math.Sqrt(Math.Pow(Math.Abs(centCircle.X - currentPoint.X+25), 2) + Math.Pow(Math.Abs(centCircle.Y - currentPoint.Y+25), 2)) > sizeCircle.Height / 2)
            {
                return false;
            }
            else return true;
        }
        /// <summary>
        /// method that draws the circle
        /// </summary>        
        public override void Draw(PaintEventArgs e)
        {
            Pen thisPen = new Pen(Color.BlueViolet, 2);
            e.Graphics.DrawEllipse(thisPen, centCircle.X, centCircle.Y, sizeCircle.Width, sizeCircle.Height);
            Fill(e);
        }
        /// <summary>
        /// method tha fills the circle with choosen color
        /// </summary>        
        public void Fill(PaintEventArgs e)
        {
            SolidBrush myBrush = new SolidBrush(Color.BurlyWood);
            e.Graphics.FillEllipse(myBrush, this.centCircle.X, this.centCircle.Y, this.sizeCircle.Width, this.sizeCircle.Height);
        }
    }
}

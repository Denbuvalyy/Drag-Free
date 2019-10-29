using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GraphicObjects8
{
    [Serializable]
    class Line:Shape
    {
        /// <summary>
        /// each line has two points for it's begining and it's end 
        /// </summary>
        public Point startPoint;
        public Point endPoint;
        /// <summary>
        /// each line is connected to two circles
        /// </summary>
        public Circle BackCircle
        { get; set; }
        public Circle FrontCircle
        { get; set; }
              
        public Line(Point startPoint, Point endPoint)
        {
            this.startPoint.X = startPoint.X + 25;
            this.startPoint.Y = startPoint.Y + 25;
            this.endPoint.X = endPoint.X + 25;
            this.endPoint.Y = endPoint.Y + 25;
        } 
        
        /// <summary>
        /// method for changing line position according to two circles connected to that line
        /// </summary>
        
        public void NewPosition(Size CrlSize)
        {           
                double Gipoten;
                int ip, jp;                
                Gipoten = Math.Sqrt(Math.Pow((BackCircle.centCircle.X -FrontCircle.centCircle.X), 2) + Math.Pow((BackCircle.centCircle.Y - FrontCircle.centCircle.Y), 2));
                ip = Convert.ToInt32((CrlSize.Width / 2) * (BackCircle.centCircle.X - FrontCircle.centCircle.X) / Gipoten);
                jp = Convert.ToInt32((CrlSize.Height / 2) * (BackCircle.centCircle.Y - FrontCircle.centCircle.Y) / Gipoten);
                this.startPoint.X = BackCircle.centCircle.X - ip+25;
                this.startPoint.Y = BackCircle.centCircle.Y - jp+25;                
                this.endPoint.X = FrontCircle.centCircle.X + ip+25;
                this.endPoint.Y = FrontCircle.centCircle.Y + jp+25;            
        }


        /// <summary>
        /// method that checks if lines intersects
        /// </summary>
        
        public bool FindIntersection(Line thatLine)
        {
            // Get the segments' parameters.
            float dx12 = endPoint.X - startPoint.X;//  p2.X - p1.X;
            float dy12 = endPoint.Y - startPoint.Y;// p2.Y - p1.Y;
            float dx34 = thatLine.endPoint.X - thatLine.startPoint.X;// p4.X - p3.X;
            float dy34 = thatLine.endPoint.Y - thatLine.startPoint.Y;// p4.Y - p3.Y;
            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);
            float t1 =
                ((startPoint.X - thatLine.startPoint.X) * dy34 + (thatLine.startPoint.Y - startPoint.Y) * dx34)
                    / denominator;
            if (float.IsInfinity(t1))
            {
                // The lines are parallel (or close enough to it).
                return false;
            }
            float t2 =
                ((thatLine.startPoint.X - startPoint.X) * dy12 + (startPoint.Y - thatLine.startPoint.Y) * dx12)
                    / -denominator;
            // Find the point of intersection.
            //intersectionPoint = new PointF(startPoint.X+ dx12 * t1,startPoint.Y+ dy12 * t1);
            // The segments intersect if t1 and t2 are between 0 and 1.                
            return ((t1 >= 0) && (t1 <= 1) &&
                (t2 >= 0) && (t2 <= 1));
        }
        /// <summary>
        /// method that draws line 
        /// </summary>        
        public override void Draw(PaintEventArgs e)
        {
            Pen linePen = new Pen(Color.Chocolate, 2);
            e.Graphics.DrawLine(linePen, startPoint, endPoint);           
        }
    }
}

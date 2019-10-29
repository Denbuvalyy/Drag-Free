using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicObjects8
{
    public class GameLevelProduser
    {
        static Point tempFirst = new Point();
        static Point tempLast = new Point();
        static Point imageCenter;
        static int A, B;
        static Point[] myCenters;
        static public Size CrlSize = new Size(50, 50);        
        static public List<Shape> Shapes = new List<Shape>();
        /// <summary>
        /// creating centers of circles and keeping them in array myCenters
        /// creating objects of circle and line types and 
        /// keeping them in array of abstract type shape  
        /// </summary>
        
        public static List<Shape> GenerateGameObjects(int level, int _a, int _b, Point imgCenter)
        {
            A = _a;
            B = _b;
            imageCenter = imgCenter;

            Shapes.Clear();              
            myCenters = new Point[level];
            Random tempRand = new Random();
            for (int i = 0; i < myCenters.Length; i++)
            {
                int a, b;
                a = tempRand.Next(A + 1);
                b = tempRand.Next(B + 1);
                float beta = tempRand.Next(361);
                myCenters[i].X = Convert.ToInt32(imageCenter.X + a * Math.Cos(beta * Math.PI / 180));
                myCenters[i].Y = Convert.ToInt32(imageCenter.Y - b * Math.Sin(beta * Math.PI / 180));
            }

            for (int i = 0, j = 1,k=2; i < myCenters.Length; i++,j++)
            {
                double Gipoten;
                int ip, jp;

                if (j == myCenters.Length)
                { j = 0; }
                Gipoten = Math.Sqrt(Math.Pow((myCenters[i].X - myCenters[j].X), 2) + Math.Pow((myCenters[i].Y - myCenters[j].Y), 2));
                ip = Convert.ToInt32((CrlSize.Width / 2) * (myCenters[i].X - myCenters[j].X) / Gipoten);
                jp = Convert.ToInt32((CrlSize.Height / 2) * (myCenters[i].Y - myCenters[j].Y) / Gipoten);
                tempFirst.X = myCenters[i].X - ip;
                tempFirst.Y = myCenters[i].Y - jp;
                tempLast.X = myCenters[j].X + ip;
                tempLast.Y = myCenters[j].Y + jp;
                Line myLine = new Line(tempLast, tempFirst);
                Circle backCircle = new Circle(CrlSize, myCenters[i]);               
                Circle frontCircle = new Circle(CrlSize, myCenters[j]);
                               
                if(j!=0&&i!=0)
                {
                    myLine.BackCircle = (Circle)Shapes[k];
                    if (Shapes[k] is Circle tempCircle)
                    { tempCircle.FrontLine = myLine; }
                    k += 2;
                    frontCircle.BackLine = myLine;
                    myLine.FrontCircle = frontCircle;
                }
                else if (i == 0)
                {
                    myLine.BackCircle = backCircle;
                    backCircle.FrontLine = myLine;                    
                    Shapes.Add(backCircle);
                    frontCircle.BackLine = myLine;
                    myLine.FrontCircle = frontCircle;
                }                
                else
                { 
                    myLine.FrontCircle = (Circle)Shapes[0];
                    if (Shapes[0] is Circle tempCircle)
                    { tempCircle.BackLine = myLine; }
                    myLine.BackCircle =(Circle) Shapes[k];
                    if(Shapes[k] is Circle tempCirc)
                    { tempCirc.FrontLine=myLine;}
                    Shapes.Add(myLine);
                    continue;
                }
                Shapes.Add(myLine);
                Shapes.Add(frontCircle);
            }          

            return Shapes;
            
        }
    }
}

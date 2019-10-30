using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphicObjects8
{
    public partial class GameForm : Form
    {           
        Point imageCenter;
        int A, B,myIndex,count, level=10;        
        public Size CrlSize = new Size(50,50);
        /// <summary>
        /// variables that hold values if a circle is dragged and if lines intersect  
        /// </summary>
        bool isdraging, intersection=false;
        public List<Shape > Shapes =new List<Shape>();        
        /// <summary>
        /// variable that holds game data for serialization
        /// </summary>
        GameDocument GDocument = new GameDocument();

        public GameForm()
        {
            InitializeComponent();             
        }     

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {          
            foreach(Shape myShape in Shapes)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                myShape.Draw(e);               
            }          
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isdraging = false;
            if(intersection==false)
            {
                string message = "You can get on higher level now";
                string title = "Changing the level";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {                   
                        level += 2;
                        lowestLevelToolStripMenuItem_Click(lowestLevelToolStripMenuItem, EventArgs.Empty);
                }
                else
                {
                    lowestLevelToolStripMenuItem_Click(lowestLevelToolStripMenuItem, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// checking if mouse is on a circle, keeping index of that circle in myIndex 
        /// </summary>
        
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {            
            for (int i=0;i<Shapes.Count;i++)
            {                
                if (Shapes[i] is Circle tempCircle)
                {
                    if (tempCircle.Contains(e.Location))
                    {                        
                        isdraging = true;
                        myIndex = i;
                    }
                }
            }          
        }
        /// <summary>
        /// changing position of choosen circle, lines connected to it 
        /// and not letting the circle to be moved outside of the picturebox 
        /// checking if lines intersect
        /// </summary>
        
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {            
            if (isdraging)
            {
                Circle tempCircle = Shapes[myIndex] as Circle;
                tempCircle.centCircle.X = e.Location.X - CrlSize.Height / 2;
                tempCircle.centCircle.Y = e.Location.Y - CrlSize.Height / 2;               
                tempCircle.BackLine.NewPosition(CrlSize);
                tempCircle.FrontLine.NewPosition(CrlSize);
                for (int i = 0; i < Shapes.Count; i++)
                {
                    if (i == myIndex)
                    { continue; }
                    if (Shapes[i] is Circle thatCircle)
                    {
                        if (tempCircle.centCircle == thatCircle.centCircle)
                        {
                            tempCircle.centCircle.X += 1;
                            tempCircle.centCircle.Y += 1;
                            tempCircle.BackLine.NewPosition(CrlSize);
                            tempCircle.FrontLine.NewPosition(CrlSize);
                        }
                    }
                }                
                if (tempCircle.centCircle.X + CrlSize.Width > pictureBox1.Width)
                {
                    tempCircle.centCircle.X = pictureBox1.Width - CrlSize.Width - 1;
                    tempCircle.BackLine.NewPosition(CrlSize);
                    tempCircle.FrontLine.NewPosition(CrlSize);
                    isdraging = false;
                }
                if (tempCircle.centCircle.X < 0)
                {
                    tempCircle.centCircle.X = 1;
                    tempCircle.BackLine.NewPosition(CrlSize);
                    tempCircle.FrontLine.NewPosition(CrlSize);
                    isdraging = false;
                }
                if (tempCircle.centCircle.Y + CrlSize.Height > pictureBox1.Height)
                {
                    tempCircle.centCircle.Y = pictureBox1.Height - CrlSize.Height - 1;
                    tempCircle.BackLine.NewPosition(CrlSize);
                    tempCircle.FrontLine.NewPosition(CrlSize);
                    isdraging = false;
                }
                if (tempCircle.centCircle.Y < 0)
                {
                    tempCircle.centCircle.Y = 1;
                    tempCircle.BackLine.NewPosition(CrlSize);
                    tempCircle.FrontLine.NewPosition(CrlSize);
                    isdraging = false;
                }
                
                lblIntersection.Show();
                for(int i=0;i<Shapes.Count-1;i++)
                {
                    if(Shapes[i] is Line tempLine)
                    {
                        for(int j=i+1;j<Shapes.Count;j++)
                        {
                            if(Shapes[j] is Line thatLine)
                            {
                                intersection = tempLine.FindIntersection(thatLine);
                                if(intersection)
                                { break; }
                            }
                        }
                        if(intersection)
                        { break; }
                    }
                }
               
                if (intersection == true)
                { lblIntersection.Text = "Present!"; }
                else
                { lblIntersection.Text = "All clear!"; }
            }
                Refresh();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pictureBox1.Width = this.Width - 25;
            imageCenter.X = (pictureBox1.Width - pictureBox1.Location.X) / 2+pictureBox1.Location.X-25;
            imageCenter.Y = (pictureBox1.Height - pictureBox1.Location.Y) / 2 + pictureBox1.Location.Y-25;
            A = (pictureBox1.Width - pictureBox1.Location.X)/2-30;
            B = (pictureBox1.Height - pictureBox1.Location.Y)/2-25;            
            lblIntersection.Hide();
            count = level;

        }              

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       /// <summary>
       /// saving state of the game in a file 
       /// </summary>
       
        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream = File.Open("mydata.bin", FileMode.Create);
            BinaryFormatter bin = new BinaryFormatter();            
            GDocument.Intersection = intersection;
            GDocument.Shapes = Shapes;
            bin.Serialize(stream,GDocument);
            stream.Close();
        }

       

        private void sameLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = count;
            lowestLevelToolStripMenuItem_Click(lowestLevelToolStripMenuItem, EventArgs.Empty);
        }

        private void topLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level += 100;
            lowestLevelToolStripMenuItem_Click(lowestLevelToolStripMenuItem, EventArgs.Empty);
        }
        /// <summary>
        /// restoring state of the game from a file
        /// </summary>
        
        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Stream stream = File.Open("mydata.bin", FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                GDocument=(GameDocument)bin.Deserialize(stream);
                Shapes.Clear();                
                Shapes = GDocument.Shapes;
                intersection =GDocument.Intersection;
                level = Shapes.Count()/2;
            }
            Refresh();
        }

        private void creatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// loading certain level of the game; where level is level 
        /// </summary>
        
        private void lowestLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shapes = GameLevelProduser.GenerateGameObjects(level,A,B, imageCenter);
            lblIntersection.Hide();           
            Refresh();
        }

      
    }
}

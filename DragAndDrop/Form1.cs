using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DragAndDrop
{
    public partial class Form1 : Form
    {
        Canvas mainCanvas;                  
        Book mainBook = new Book("");       
        Point oldPoint;                     
        Page curPage;                       


        public Form1()
        {
            InitializeComponent();
            mainCanvas = new Canvas(pictureBox1);
            pictureBox1.Image = mainCanvas.BMP;
        }

        public void RefreshBitmap()
        {
            Bitmap bmp = mainCanvas.BMP;
            if (bmp != null) bmp.Dispose();
            bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            
            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (Page page in mainBook.pages)
                {
                    g.DrawPath(page.fig.Pen, page.fig.Path);
                }
            }
            pictureBox1.Image = bmp;
        }


        //
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
                //Start moving object

                oldPoint = e.Location;
                foreach (Page page in mainBook.pages)
                {
                    if (page.fig.Path.GetBounds().Contains(e.Location))
                    {
                        curPage = page;
                        curPage.fig.Pen.Width += 1;
                        return;
                    }
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (curPage != null)
                {
                    //End of moving

                    curPage.fig.Pen.Width -= 1;
                    curPage = null;
                }

                else
                {
                    //Creating new pages

                    string curPic = listBox1.SelectedItem != null ? listBox1.SelectedItem.ToString() : "square";
                    Figure fig = new Figure(new Pen(Color.Black, 3), curPic, new GraphicsPath());  
                    fig.AddPic(e.X, e.Y);                                                 
                    mainBook.pages.Add(new Page(fig));                                                                               
                } 
            }

            //Deleting

            if (e.Button == MouseButtons.Right)
            {
                foreach (Page page in mainBook.pages)
                {
                    if (page.fig.Path.GetBounds().Contains(e.Location))
                    {
                        page.fig.Path.Reset();
                        break;
                    }
                }
            }

            RefreshBitmap();
        }

        //Moving objects

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (curPage != null)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        int deltaX, deltaY;
                        deltaX = e.Location.X - oldPoint.X;
                        deltaY = e.Location.Y - oldPoint.Y;
                        curPage.fig.Path.Transform(new Matrix(1, 0, 0, 1, deltaX, deltaY));
                        oldPoint = e.Location;
                        break;
                    default:
                        break;
                }
                RefreshBitmap();
            }
        }
    }



    public class Canvas
    {
        Graphics myGraphics;
        private Bitmap bmp = new Bitmap(200, 200);

        public Bitmap BMP
        {
            get { return bmp; }
            set { bmp = value; }
        }

        public Canvas(PictureBox pictureBox)
        {
            myGraphics = pictureBox.CreateGraphics();
        }

    }
}

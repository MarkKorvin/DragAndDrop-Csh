using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDrop
{
    //graphical component of the Page
    public class Figure
    {
        private GraphicsPath path;
        private Pen pen;
        private string pic = "square";

        public Figure(Pen pen, string pic, GraphicsPath path)
        {
            this.path = path;
            this.pen = pen;
            this.pic = pic;
        }

        public GraphicsPath Path
        {
            get { return path; }
            set { path = value; }
        }
        public Pen @Pen
        {
            get { return pen; }
            set { pen = value; }
        }

        public void AddPic(int X, int Y)
        {
            switch (pic)
            {
                case "square":
                    Rectangle rect = Rectangle.FromLTRB(X - 10, Y - 10, X + 10, Y + 10);
                    this.Path.AddRectangle(rect);
                    //this.Path.AddString("1", FontFamily.GenericMonospace, 0, 15, rect, StringFormat.GenericDefault );
                    return;
                case "circle": this.Path.AddEllipse(Rectangle.FromLTRB(X - 12, Y - 12, X + 12, Y + 12)); return;

            }
        }
    }


    //Figure with any additions
    public class Page
    {
        int ID = 0;
        string text = "";
        public Figure fig;

        public Page(Figure fig)
        {
            this.fig = fig;
        }
    }

    //bank of pages
    public class Book
    {
        string Name;
        public List<Page> pages;

        public Book(string Name)
        {
            this.Name = Name;
            pages = new List<Page>();
        }
    }

}




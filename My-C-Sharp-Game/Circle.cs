using System;
using System.Drawing;
using System.Windows.Forms;

namespace My_C_Sharp_Game
{
    class Circle
    {
        public float x, y, r;

        public Circle(Point ori, float rad)
        {
            x = ori.X;
            y = ori.Y;
            r = rad;
        }

        public void draw(Pen pen, PaintEventArgs canvas)
        {
            canvas.Graphics.DrawEllipse(pen, x - r, y - r, 2 * r, 2 * r);
        }

        public float oriDis(Point p)
        {
            return (float)Math.Sqrt(Math.Pow(x - p.X, 2) + Math.Pow(y - p.Y, 2));
        }

        public float oriDis(Circle cir)
        {
            return (float)Math.Sqrt(Math.Pow(x - cir.x, 2) + Math.Pow(y - cir.y, 2));
        }

        public float boundDis(Circle cir)
        {
            return (float)Math.Sqrt(Math.Pow(x - cir.x, 2) + Math.Pow(y - cir.y, 2)) - r - cir.r;
        }
    }
}

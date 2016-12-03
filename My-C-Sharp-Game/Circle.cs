using System;
using System.Drawing;
using System.Windows.Forms;

namespace My_C_Sharp_Game
{
    class Circle
    {
        public Point vec;
        public int hp;

        private Point ori;
        private int rad;

        private Pen p;

        public Circle(Point o, int r, Point v)
        {
            ori = o;
            rad = r;
            vec = v;
            hp = r * 100;

            p = new Pen(Color.Black, 2.0f);
        }

        public void Draw(PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(p, ori.X - rad, ori.Y - rad, 2 * rad, 2 * rad);
        }
    }
}

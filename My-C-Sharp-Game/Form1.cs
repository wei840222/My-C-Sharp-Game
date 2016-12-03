using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_C_Sharp_Game
{
    public partial class Form1 : Form
    {
        Circle Player;

        public Form1()
        {
            InitializeComponent();
            Player = new Circle(new Point(Width / 2, Height / 2), 20, new Point(0, 0));
        }

        private void GameDraw(object sender, PaintEventArgs e)
        {
            Player.Draw(e);
        }

        private void GameTimer(object sender, EventArgs e)
        {

        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {

        }

        private void onMouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}

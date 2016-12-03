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
        private const float FPS = 5.0f;
        private InputState KeyW;

        private Circle Player;

        public Form1()
        {
            InitializeComponent();

            Timer.Interval = 1000 / (int)FPS;
            Timer.Start();

            KeyW = new InputState();

            Player = new Circle(new Point(Width / 2, Height / 2), 20, new Point(0, 0));
        }

        private void GameDraw(object sender, PaintEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("KeyW " + "Down: " + KeyW.down + "  Press: " + KeyW.press);

            Player.Draw(e);
        }

        private void GameTimer(object sender, EventArgs e)
        {
            KeyW.onTimer();
            Invalidate();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    KeyW.onDown();
                    break;
            }
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    KeyW.onUp();
                    break;
            }
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

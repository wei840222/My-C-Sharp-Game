using System.Drawing;

namespace My_C_Sharp_Game
{
    class Bullet : Circle
    {
        public int atk;
        private float mx, my;
        private const float BULLET_SPEED = 20;

        public Bullet(Point ori, float rad, Point mousePos) : base(ori, rad)
        {
            x = ori.X;
            y = ori.Y;
            r = rad;

            atk = (int)(rad * 25);

            mx = (mousePos.X - x) / oriDis(mousePos) * BULLET_SPEED;
            my = (mousePos.Y - y) / oriDis(mousePos) * BULLET_SPEED;
        }

        public void move()
        {
            x += mx;
            y += my;
        }
    }
}

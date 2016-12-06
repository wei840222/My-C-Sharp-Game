using System.Drawing;

namespace My_C_Sharp_Game
{
    class Monster : Circle
    {
        public int hp;
        private float mx, my;

        public Monster(Point ori, float rad, float mx, float my) : base(ori, rad)
        {
            x = ori.X;
            y = ori.Y;
            r = rad;

            hp = (int)(rad * 100);

            this.mx = mx;
            this.my = my;
        }

        public void move()
        {
            x += mx;
            x += my;
        }

        public void follow(Point aim)
        {
            float dis = oriDis(aim);

            if (dis >= 1)
            {
                x += (aim.X - x) / dis * mx;
                y += (aim.Y - y) / dis * my;
            }
            else
            {
                x = aim.X;
                y = aim.Y;
            }
        }
    }
}
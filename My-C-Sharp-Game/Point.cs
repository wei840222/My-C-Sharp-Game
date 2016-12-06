using System;

namespace My_C_Sharp_Game
{
    class Point
    {
        //        static public float staticData = 1.7f;//共享的,只有一份

        //public,公開的
        //private,私有的
        public float x, y;//x, y座標

        //方法(method),成員函式
        //float是回傳變數的形態
        public float getDistance(Point pnt)
        {
            //x,y是指,呼叫者的x,y
            //pnt是指傳進來的物件
            //pnt是對應到傳進來的物件
            float L = (float)Math.Sqrt(
                        (x - pnt.x) * (x - pnt.x)
                        + (y - pnt.y) * (y - pnt.y));

            return L;//回傳一個float,距離
        }

        //void 沒有回傳值
        public void move(Point target)
        {
            float L = getDistance(target);

            float tx, ty;
            if (L > 1)
            {
                tx = x + (target.x - x) * 1 / L;
                ty = y + (target.y - y) * 1 / L;

                x = tx;//設定
                y = ty;
            }
            else
            {
                //距離很近
                x = target.x;
                y = target.y;
            }
        }
    }
}

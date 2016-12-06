using System.Windows.Forms;

namespace My_C_Sharp_Game
{
    //按鍵狀態(指定針對某個按鍵來做偵測)
    class KeyState
    {
        //Keys不是一個類別
        //Keys是列舉(enum)
        private Keys theKey;//存放一個對應的按鍵編號

        public bool down;//目前這次是壓著的
        public bool preDown;//上次onTimer是否壓著
        public bool press;//剛剛壓下去

        public KeyState(Keys k)
        {
            theKey = k;
            press = false;
            down = false;
            preDown = false;
        }

        public bool isPress()//回報是否剛剛按下去
        {
            return press;
        }

        public void onTimer()//timer通知時呼叫
        {
            //bPress的偵測
            if (down == true)//此時是壓著
            {
                if (preDown == false)//上次是放開的
                    press = true;
                else//上次是壓著的
                    press = false;
            }
            else
            {
                //這次是放開的
                press = false;
            }

            //把這次的狀態記下來,下次就可以用這個狀態
            preDown = down;
        }

        public void onKeyDown(Keys k)//偵測
        {
            if (theKey == k)
            {
                //偵測到同一個按鍵,按下去
                /*
                if (bDown == false)//是否原本是放開的
                {
                    //剛剛壓下去的通知
                    bPress = true;
                }
                else
                {
                    //原本就是壓著的,持續壓著的通知
                    bPress = false;
                }
                */

                down = true;
            }
        }

        public void onKeyUp(Keys k)//偵測
        {
            if (theKey == k)
            {
                //偵測到同一個按鍵,放開
                down = false;
                press = false;
            }
        }

        public bool isDown()//回報是否壓著
        {
            return down;
        }
    }
}

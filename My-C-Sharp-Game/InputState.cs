using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_C_Sharp_Game
{
    public class InputState
    {
        public bool down;
        public bool press;

        private bool pre_down;

        public InputState()
        {
            down = false;
            press = false;
            pre_down = false;
        }

        public void onDown()
        {
            down = true;
        }

        public void onUp()
        {
            down = false;
            pre_down = false;
        }

        public void onTimer()
        {
            if (down)
                pre_down = true;
            if (down && !pre_down)
                press = true;
            else if (down && pre_down)
                press = false;
            
        }
    }
}

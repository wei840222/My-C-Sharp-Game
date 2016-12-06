namespace My_C_Sharp_Game
{
    class InputState
    {
        public bool isDown;
        public bool isPress;
        public bool preDown;
        
        public InputState()
        {
            isDown = false;
            isPress = false;
            preDown = false;
        }

        public void onTimer()
        {
            if (isDown == true)
            {
                if (preDown == false)
                    isPress = true;
                else
                    isPress = false;
            }
            else
                isPress = false;

            preDown = isDown;
        }

        public void onKeyDown()
        {
            isDown = true;
        }

        public void onKeyUp()
        {
            isDown = false;
            isPress = false;
        }
    }
}
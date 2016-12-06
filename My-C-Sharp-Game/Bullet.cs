namespace My_C_Sharp_Game
{
    //繼承
    //定義類別時加入 : Point
    //Bullet 子類別
    //Point 父類別
    //子類別會擁有父類別所有的功能(方法),資料
    //加強,修改父類別
    class Bullet : Point
    {
        private Point moveDir;//移動的向量

        private const float BULLET_SPEED = 10;

        //建構方法
        //方法名稱跟類別名稱相同
        //當new某個物件(實體)時候
        //會呼叫這個物件(實體)的建構
        //new Bullet () ;
        public Bullet(Point pos, Point mousePos)//建構
        {
            x = pos.x;
            y = pos.y;

            float dist = getDistance(mousePos);

            moveDir = new Point();
            moveDir.x = (mousePos.x - x) / dist * BULLET_SPEED;
            moveDir.y = (mousePos.y - y) / dist * BULLET_SPEED;
        }

        public void move()
        {
            //base. 使用父類別的move功能
            x += moveDir.x;
            y += moveDir.y;
        }
    }
}

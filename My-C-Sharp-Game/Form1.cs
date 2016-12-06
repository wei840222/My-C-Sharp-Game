using System;
using System.Drawing;
using System.Windows.Forms;

namespace My_C_Sharp_Game
{
    public partial class Form1 : Form
    {
        //宣告參照(考)
        private Point player;//玩家的座標

        private const int MAX_BULLET = 50;
        //        private Point[] vBullet;//子彈的座標
        private Bullet[] vBullet;//子彈的座標

        private Point[] vMonster;

        private Random random;

        private Graphics wndGraphics;//視窗畫布
        private Graphics backGraphics;//背景頁的畫布
        private Bitmap backBmp;//背景頁的點陣圖

        private const int VIEW_W = 1024;
        private const int VIEW_H = 768;

        private const float MAKE_MONSTER_TIME = 3;
        private float makeMonsterTime;//生怪的時間

        private const float MAKE_BULLET_TIME = 0.1f;
        private float makeBulletTime;//再過多久生子彈的時間

        //預期的畫面更新率(每秒呼叫幾次OnTimer)
        private const float REQUIRE_FPS = 60;

        //const 常數
        private const int MAX_ENEMY = 20;
        private float BALL_SIZE = 25;

        private Point mousePos;

        //bool 布林值
        //只有true, false兩種可能
        //true, yes
        //false, no
        //        private bool bSpaceDown;//空白鍵是否壓著
        private KeyState keySpace;//空白鍵
        private KeyState keyUp;//上
        private KeyState keyDown;//下
        private KeyState keyLeft;//左
        private KeyState keyRight;//右

        //method,方法
        //Form1,名稱
        public Form1()
        {
            InitializeComponent();

            keySpace = new KeyState(Keys.Space);
            keyUp = new KeyState(Keys.Up);
            keyDown = new KeyState(Keys.Down);
            keyLeft = new KeyState(Keys.Left);
            keyRight = new KeyState(Keys.Right);

            makeBulletTime = 0;

            makeMonsterTime = MAKE_MONSTER_TIME;

            wndGraphics = CreateGraphics();//建立視窗的畫布

            backBmp = new Bitmap(VIEW_W, VIEW_H);//建立點陣圖
            backGraphics = Graphics.FromImage(backBmp);//建立背景的畫布

            mousePos = new Point();

            random = new Random();

            //            bullet = new Point();
            vBullet = new Bullet[MAX_BULLET];

            //new : 產生一個Point物件(實體)
            //player = new Point () ;
            //player會對應到產生的這個物件
            player = new Point();
            player.x = 200;
            player.y = 100;

            //            Point.staticData = 2.0f;

            vMonster = new Point[MAX_ENEMY];

            //迴圈, 反覆的做某件事情
            for (int i = 0; i < MAX_ENEMY; i++)
            {
                vMonster[i] = new Point();

                vMonster[i].x = random.Next(0, VIEW_W);
                vMonster[i].y = random.Next(0, VIEW_H);
            }

            //timer1是一個計時器(鬧鐘)物件
            //timer1.Interval 多久響
            Timer.Interval = 1000 / (int)REQUIRE_FPS;//1/30秒
            Timer.Start();
        }

        //method,方法,功能
        //onPaint, 名稱
        private void GameDraw(object sender, PaintEventArgs e)
        {
            drawGame();
        }

        private void moveMonster()
        {
            //讓藍色球靠近紅色球
            for (int i = 0; i < MAX_ENEMY; i++)
            {
                if (vMonster[i] != null)
                {
                    //怪物向玩家移動,移動長度是1
                    vMonster[i].move(player);

                    /*
                    if (vMonster[i].getDistance(player) <
                        BALL_SIZE + BALL_SIZE)
                    {
                        //重疊
                        vMonster[i] = null;
                    }
                    */
                }
            }

        }

        private void moveBullet_KillMonster()
        {
            for (int i = 0; i < MAX_BULLET; i++)
            {
                if (vBullet[i] != null)
                {
                    //                    vBullet[i].x += 3;
                    vBullet[i].move();

                    //有沒有打到怪
                    for (int m = 0; m < MAX_ENEMY; m++)
                    {
                        if (vMonster[m] != null)
                        {
                            //子彈,跟怪都是存在的
                            //vMonster : Point
                            //vBullet : Bullet
                            //getDistance (傳入子類別)
                            if (vMonster[m].getDistance(vBullet[i])
                                < BALL_SIZE + BALL_SIZE)
                            {
                                vBullet[i] = null;
                                vMonster[m] = null;
                                break;
                            }
                        }
                    }

                    if (vBullet[i] != null)
                    {
                        if (vBullet[i].x > VIEW_W)//出界
                            vBullet[i] = null;
                        else if (vBullet[i].y > VIEW_H)//出界
                            vBullet[i] = null;
                        else if (vBullet[i].x < 0)//出界
                            vBullet[i] = null;
                        else if (vBullet[i].y < 0)//出界
                            vBullet[i] = null;
                    }
                }
            }
        }

        private void drawGame()
        {

            //方法
            backGraphics.FillRectangle(Brushes.White, 0, 0, VIEW_W, VIEW_H);
            backGraphics.DrawEllipse(Pens.Blue,
                            player.x, player.y,
                            BALL_SIZE * 2, BALL_SIZE * 2);


            int total = 0;
            for (int i = 0; i < MAX_BULLET; i++)
            {
                if (vBullet[i] != null)
                {
                    total++;
                    backGraphics.DrawEllipse(Pens.Black,
                            vBullet[i].x, vBullet[i].y,
                            BALL_SIZE * 2, BALL_SIZE * 2);
                }
            }

            //子彈的數量
            //字串,儲存文字
            //String是字串類別
            //"子彈數量"一段文字
            String str = "子彈數量" + total;
            backGraphics.DrawString(str, SystemFonts.CaptionFont,
                                    Brushes.Black, 0, 0);

            //畫出怪
            total = 0;
            for (int i = 0; i < MAX_ENEMY; i++)
            {
                if (vMonster[i] != null)
                {
                    total++;
                    backGraphics.DrawEllipse(Pens.Red,
                                vMonster[i].x, vMonster[i].y,
                                BALL_SIZE * 2, BALL_SIZE * 2);
                }
            }

            //怪物的數量
            str = "怪物數量" + total;
            backGraphics.DrawString(str, SystemFonts.CaptionFont,
                                    Brushes.Black, 0, 20);

            //            if (bSpaceDown == true)
            if (keySpace.isDown())
                backGraphics.DrawString("space down" + makeBulletTime, SystemFonts.CaptionFont,
                                        Brushes.Black, 0, 40);
            else
                backGraphics.DrawString("space up" + makeBulletTime, SystemFonts.CaptionFont,
                                        Brushes.Black, 0, 40);

            //把背景頁,畫到視窗上面
            wndGraphics.DrawImageUnscaled(backBmp, 0, 0);
        }

        //定時,呼叫onTimer
        //時間到了,就會呼叫onTimier
        private void onTimer(object sender, EventArgs e)
        {
            //就是主迴圈main loop
            keySpace.onTimer();//bPress
            keyUp.onTimer();
            keyDown.onTimer();
            keyLeft.onTimer();
            keyRight.onTimer();

            if (keySpace.isPress())//剛剛壓下去
            {
                //也要做發射
                fireBullet();
            }

            if (keySpace.isDown())//持續壓著
            {
                //壓著空白鍵
                makeBulletTime -= 1.0f / REQUIRE_FPS;//1/30秒
                if (makeBulletTime <= 0)
                {
                    //發射子彈時間到了
                    fireBullet();
                }
            }

            if (keyUp.isDown())//下
                player.y -= 5;
            if (keyDown.isDown())
                player.y += 5;
            if (keyLeft.isDown())
                player.x -= 5;
            if (keyRight.isDown())
                player.x += 5;

            //
            makeMonsterTime -= 1.0f / REQUIRE_FPS;//1/30秒
            if (makeMonsterTime <= 0)
            {
                //生怪時間到了
                for (int i = 0; i < MAX_ENEMY; i++)
                {
                    if (vMonster[i] == null)//
                    {
                        vMonster[i] = new Point();

                        vMonster[i].x = random.Next(0, VIEW_W);
                        vMonster[i].y = random.Next(0, VIEW_H);
                        break;
                    }
                }

                makeMonsterTime = MAKE_MONSTER_TIME;
            }

            moveMonster();

            moveBullet_KillMonster();

            drawGame();
        }

        void fireBullet()
        {
            for (int i = 0; i < MAX_BULLET; i++)
            {
                if (vBullet[i] == null)
                {
                    vBullet[i] = new Bullet(player, mousePos);
                    vBullet[i].x = player.x;
                    vBullet[i].y = player.y;

                    makeBulletTime = MAKE_BULLET_TIME;

                    break;
                }
            }
        }

        //按下某個按鍵的時候
        //會呼叫這個方法
        private void onKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        //滑鼠移動的通知
        private void onMouseMove(object sender, MouseEventArgs e)
        {
            mousePos.x = e.X;
            mousePos.y = e.Y;
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            //按鍵放開的通知
            keySpace.onKeyUp(e.KeyCode);
            keyUp.onKeyUp(e.KeyCode);
            keyDown.onKeyUp(e.KeyCode);
            keyLeft.onKeyUp(e.KeyCode);
            keyRight.onKeyUp(e.KeyCode);
        }

        //按下某個按鍵
        private void onKeyDown(object sender, KeyEventArgs e)
        {
            keySpace.onKeyDown(e.KeyCode);
            keyUp.onKeyDown(e.KeyCode);
            keyDown.onKeyDown(e.KeyCode);
            keyLeft.onKeyDown(e.KeyCode);
            keyRight.onKeyDown(e.KeyCode);
        }
    }

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

    //按鍵狀態(指定針對某個按鍵來做偵測)
    class KeyState
    {
        //Keys不是一個類別
        //Keys是列舉(enum)
        private Keys theKey;//存放一個對應的按鍵編號

        bool bPress;//剛剛壓下去
        bool bDown;//目前這次是壓著的
        bool pPreDown;//上次onTimer是否壓著

        public KeyState(Keys k)
        {
            theKey = k;
            bPress = false;
            bDown = false;
            pPreDown = false;
        }

        public bool isPress()//回報是否剛剛按下去
        {
            return bPress;
        }

        public void onTimer()//timer通知時呼叫
        {
            //bPress的偵測
            if (bDown == true)//此時是壓著
            {
                if (pPreDown == false)//上次是放開的
                    bPress = true;
                else//上次是壓著的
                    bPress = false;
            }
            else
            {
                //這次是放開的
                bPress = false;
            }

            //把這次的狀態記下來,下次就可以用這個狀態
            pPreDown = bDown;
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

                bDown = true;
            }
        }

        public void onKeyUp(Keys k)//偵測
        {
            if (theKey == k)
            {
                //偵測到同一個按鍵,放開
                bDown = false;
                bPress = false;
            }
        }

        public bool isDown()//回報是否壓著
        {
            return bDown;
        }
    };
}
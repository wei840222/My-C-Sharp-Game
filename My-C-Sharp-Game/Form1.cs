using System;
using System.Drawing;
using System.Windows.Forms;

namespace My_C_Sharp_Game
{
    public partial class Form1 : Form
    {
        //宣告參照
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
        private const float FPS = 60;

        //const 常數
        private const int MAX_ENEMY = 20;
        private float BALL_SIZE = 25;

        private Point mousePos;

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
            Timer.Interval = 1000 / (int)FPS;//1/30秒
            Timer.Start();
        }

        //method,方法,功能
        //onPaint, 名稱
        private void GameDraw(object sender, PaintEventArgs e)
        {
            //方法
            e.Graphics.FillRectangle(Brushes.White, 0, 0, VIEW_W, VIEW_H);
            e.Graphics.DrawEllipse(Pens.Blue, player.x, player.y, BALL_SIZE * 2, BALL_SIZE * 2);


            int total = 0;
            for (int i = 0; i < MAX_BULLET; i++)
            {
                if (vBullet[i] != null)
                {
                    total++;
                    e.Graphics.DrawEllipse(Pens.Black,
                            vBullet[i].x, vBullet[i].y,
                            BALL_SIZE * 2, BALL_SIZE * 2);
                }
            }

            //子彈的數量
            //字串,儲存文字
            //String是字串類別
            //"子彈數量"一段文字
            string str = "子彈數量" + total;
            e.Graphics.DrawString(str, SystemFonts.CaptionFont, Brushes.Black, 0, 0);

            //畫出怪
            total = 0;
            for (int i = 0; i < MAX_ENEMY; i++)
            {
                if (vMonster[i] != null)
                {
                    total++;
                    e.Graphics.DrawEllipse(Pens.Red, vMonster[i].x, vMonster[i].y, BALL_SIZE * 2, BALL_SIZE * 2);
                }
            }

            //怪物的數量
            str = "怪物數量" + total;
            e.Graphics.DrawString(str, SystemFonts.CaptionFont, Brushes.Black, 0, 20);

            if (keySpace.isDown())
                e.Graphics.DrawString("space down" + makeBulletTime, SystemFonts.CaptionFont,
                                        Brushes.Black, 0, 40);
            else
                e.Graphics.DrawString("space up" + makeBulletTime, SystemFonts.CaptionFont,
                                        Brushes.Black, 0, 40);
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

        //定時,呼叫onTimer
        //時間到了,就會呼叫onTimier
        private void onTimer(object sender, EventArgs e)
        {
            //就是主迴圈main loop
            keySpace.onTimer();
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
                makeBulletTime -= 1.0f / FPS;//1/30秒
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
            makeMonsterTime -= 1.0f / FPS;//1/30秒
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

            Invalidate();
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
}
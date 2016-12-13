using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace My_C_Sharp_Game
{
    public partial class Game : Form
    {
        private const int VIEW_W = 783;
        private const int VIEW_H = 560;

        private static Random rnd = new Random(Guid.NewGuid().GetHashCode());
        private static Timer gameTimer = new Timer();
        //預期的畫面更新率(每秒呼叫幾次OnTimer)
        private const float FPS = 60.0f;
       
        //玩家
        private static Circle player = new Circle(new Point(200, 100), 20);
        private static int score = 0;

        //子彈
        private static List<Bullet> bullets = new List<Bullet>();
        private const float NEW_BULLET_TIME = 0.025f;
        private static float newBulletTime = 0.0f;

        //怪物
        private static List<Monster> monsters = new List<Monster>();
        private const float NEW_MONSTER_TIME = 3.0f;
        private static float newMonsterTime = 0.0f;

        //輸入
        private static InputState mouseLeft = new InputState();
        private static Point mousePos = new Point();
        
        private static InputState keyW = new InputState();
        private static InputState keyS = new InputState();
        private static InputState keyA = new InputState();
        private static InputState keyD = new InputState();

        public Game()
        {
            InitializeComponent();

            gameTimer.Interval = (int)(1000.0f / FPS);
            gameTimer.Tick += new EventHandler(GameLoop);
        }

        private void GameDraw(object sender, PaintEventArgs e)
        {
            //清空畫布
            e.Graphics.Clear(Color.White);

            //繪製玩家
            player.draw(Pens.Blue, e);

            //繪製子彈
            bullets.TrimExcess();
            for (int i = 0; i < bullets.Count; i++)
                bullets[i].draw(Pens.Black, e);

            //繪製怪物
            monsters.TrimExcess();
            for (int i = 0; i < monsters.Count; i++)
                monsters[i].draw(Pens.Red, e);

            //更新記分板
            e.Graphics.DrawString("得分: " + score, SystemFonts.CaptionFont, Brushes.Black, 5, 5);

            //繪製資訊文字
            e.Graphics.DrawString("子彈數量: " + bullets.Count, SystemFonts.CaptionFont, Brushes.Black, 5, 520);
            e.Graphics.DrawString("怪物數量: " + monsters.Count, SystemFonts.CaptionFont, Brushes.Black, 5, 540);

            if (!gameTimer.Enabled)
                e.Graphics.Clear(Color.White);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            //偵測鍵盤
            mouseLeft.onTimer();
            keyW.onTimer();
            keyS.onTimer();
            keyA.onTimer();
            keyD.onTimer();

            //移動玩家
            if (keyW.isDown)
            {
                player.y -= 5;
                if (player.y - player.r < 0)
                    player.y = 0 + player.r;
            }  
            if (keyS.isDown)
            {
                player.y += 5;
                if (player.y + player.r > VIEW_H)
                    player.y = VIEW_H - player.r;
            }   
            if (keyA.isDown)
            {
                player.x -= 5;
                if (player.x - player.r < 0)
                    player.x = 0 + player.r;
            }
            if (keyD.isDown)
            {
                player.x += 5;
                if (player.x + player.r > VIEW_W)
                    player.x = VIEW_W - player.r;
            }

            //發射子彈
            if (mouseLeft.isDown)
            {
                newBulletTime -= 1.0f / FPS;
                if (newBulletTime <= 0)
                    mouseLeft.isPress = true;
            }
            if (mouseLeft.isPress)
            {
                bullets.Add(new Bullet(new Point((int)player.x, (int)player.y), 5, mousePos));
                newBulletTime = NEW_BULLET_TIME;
            }

            //產生怪物
            newMonsterTime -= 1.0f / FPS;
            if (newMonsterTime <= 0)
            {
                int news = rnd.Next(10, 30);
                for (int i = 0; i <= news; i++)
                {
                    monsters.Add(new Monster(new Point(rnd.Next(0, VIEW_W), rnd.Next(0, VIEW_H)),
                    rnd.Next(10, 50), rnd.Next(1, 10), rnd.Next(1, 10)));
                    newMonsterTime = NEW_MONSTER_TIME;
                }
            }

            //移動子彈
            bullets.TrimExcess();
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].move();
                if (bullets[i].x + bullets[i].r < 0 || bullets[i].x - bullets[i].r > VIEW_W ||
                    bullets[i].y + bullets[i].r < 0 || bullets[i].y - bullets[i].r > VIEW_H)
                    bullets.Remove(bullets[i]);
            }

            //移動怪物
            monsters.TrimExcess();
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].follow(new Point((int)player.x, (int)player.y));

                bullets.TrimExcess();
                for (int j = 0; j < bullets.Count; j++)
                    if (monsters[i].boundDis(bullets[j]) <= 0)
                    {
                        monsters[i].hp -= bullets[j].atk;
                        bullets.Remove(bullets[j]);
                    }

                if (monsters[i].boundDis(player) <= 0)
                {
                    score -= (int)monsters[i].r;
                    monsters.Remove(monsters[i]);
                    continue;
                }
                if (monsters[i].hp <= 0)
                {
                    score += (int)monsters[i].r;
                    monsters.Remove(monsters[i]);
                }
            }

            Invalidate();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    keyW.onKeyDown();
                    break;
                case Keys.S:
                    keyS.onKeyDown();
                    break;
                case Keys.A:
                    keyA.onKeyDown();
                    break;
                case Keys.D:
                    keyD.onKeyDown();
                    break;
            }
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    keyW.onKeyUp();
                    break;
                case Keys.S:
                    keyS.onKeyUp();
                    break;
                case Keys.A:
                    keyA.onKeyUp();
                    break;
                case Keys.D:
                    keyD.onKeyUp();
                    break;
            }
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouseLeft.onKeyDown();
        }

        private void onMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouseLeft.onKeyUp();
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = e.X;
            mousePos.Y = e.Y;
        }

        private void label_start_Click(object sender, EventArgs e)
        {
            Controls.Remove(label_title);
            Controls.Remove(label_start);
            Controls.Remove(label_exit);
            Controls.Remove(label_description);
            gameTimer.Start();
        }

        private void label_exit_Click(object sender, EventArgs e)
        {
            Close();
            Environment.Exit(Environment.ExitCode);
            InitializeComponent();
        }
    }
}
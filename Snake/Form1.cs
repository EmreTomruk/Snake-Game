using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Daire> Snake = new List<Daire>();

        private Daire food = new Daire();

        public Form1()
        {
            InitializeComponent();
            new Ayarlar();

            GameTimer.Interval = 1000 / Ayarlar.Speed;
            GameTimer.Tick += EkraniGuncelle;
            GameTimer.Start();

            StartGame();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;
            new Ayarlar();
            Snake.Clear();
            Daire head = new Daire { X = 10, Y = 5 };
            Snake.Add(head);
            lbl2Score.Text = Ayarlar.Score.ToString();
            YiyecekYarat();
        }

        private void YiyecekYarat()
        {
            int maxXpos = pictureBox1.Size.Width / Ayarlar.Width;
            int maxYpos = pictureBox1.Size.Height / Ayarlar.Height;
            Random rnd = new Random();
            food = new Daire { X = rnd.Next(0, maxXpos), Y = rnd.Next(0, maxYpos) };
        }
        private void EkraniGuncelle(object sender, EventArgs e)
        {
            if (Input.BasiliTus(Keys.Enter)) { StartGame(); }

            else
            {
                if (Input.BasiliTus(Keys.Right) && Ayarlar.Direction != Direction.Left)
                    Ayarlar.Direction = Direction.Right;

                if (Input.BasiliTus(Keys.Left) && Ayarlar.Direction != Direction.Right)
                    Ayarlar.Direction = Direction.Left;

                if (Input.BasiliTus(Keys.Up) && Ayarlar.Direction != Direction.Down)
                    Ayarlar.Direction = Direction.Up;

                if (Input.BasiliTus(Keys.Down) && Ayarlar.Direction != Direction.Up)
                    Ayarlar.Direction = Direction.Down;

                OyuncuHareket();
            }
            pictureBox1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if(!Ayarlar.GameOver)
            {
                for (int i = 0; i < Snake.Count; i++)
                {
                    Brush snakeColour;
                    if (i==0)
                    {
                        snakeColour = Brushes.Black;
                    }
                    else 
                    {
                        snakeColour = Brushes.Green;
                    }

                    canvas.FillEllipse(snakeColour, 
                        new Rectangle(Snake[i].X * Ayarlar.Width, 
                        Snake[i].Y * Ayarlar.Height,
                        Ayarlar.Width, Ayarlar.Height));

                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Ayarlar.Width,
                        food.Y * Ayarlar.Height,
                        Ayarlar.Width, Ayarlar.Height));
                }
            }
            else
            {
                string gameOver = "Oyun Bitti \n Final Skoru: "+ Ayarlar.Score+ "\n Devam etmek için Enter tuşuna basın...";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void OyuncuHareket()
        {
            for (int i=Snake.Count-1; i>=0; i--)
            {
                if (i==0)
                {
                        switch (Ayarlar.Direction)
                        {
                            case Direction.Right:
                            Snake[i].X++;
                            break;
                            case Direction. Left:
                            Snake[i].X--;
                            break;
                            case Direction.Up:
                            Snake[i].Y--;
                            break;
                            case Direction.Down:
                            Snake[i].Y++;
                            break;
                        }
                    int maxXPos = pictureBox1.Size.Width / Ayarlar.Width;
                    int maxYPos = pictureBox1.Size.Height / Ayarlar.Height;

                    if (Snake[i].X<0 ||Snake[i].Y<0 || Snake[i].X >=maxXPos || Snake[i].Y >= maxYPos)
                    {
                        Die();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y ==Snake[j].Y)
                        {
                            Die();
                        }
                    }

                    if (Snake[0].X ==food.X && Snake[0].Y==food.Y )
                    {
                        Eat();
                    }
                }

                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.DurumDegisti(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.DurumDegisti(e.KeyCode, false);
        }

        private void Eat()
        {
            Daire circle = new Daire()
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);
            Ayarlar.Score += Ayarlar.Points;
            lbl2Score.Text = Ayarlar.Score.ToString();
            YiyecekYarat();
        }

        private void Die() { Ayarlar.GameOver = true; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Direction { Up, Down, Left, Right}
    public class Ayarlar
    {
        public static int Width { get; set; }

        public static int Height { get; set; }

        public static int Speed { get; set; }

        public static int Score { get; set; }

        public static int Points { get; set; }

        public static bool GameOver { get; set; }

        public static Direction Direction { get; set; }

        public Ayarlar()
        {
            Width = 20;
            Height = 20;
            Speed = 10;
            Score = 0;
            Points = 100;
            GameOver = false;
            Direction = Direction.Down;
        }
    }
}

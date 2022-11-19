using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bees
{
    class Bee : Entity
    {
        const int MAX_SPEED = 5;
        public static int MaxX;
        public static int MaxY;
        double vectorX, vectorY;
        static Random rand = new Random();
        static int startID = 0;
        static Image im1 = Image.FromFile("bee1.png");
        static Image im2 = Image.FromFile("bee2.png");
        static Image im3 = Image.FromFile("beeH1.png");
        static Image im4 = Image.FromFile("beeH2.png");
        bool isFull;
        
        public enum State
        {
            Search, MoveTo, TakeHoney, GiveHoney, Birth
        };
        State state;
        Entity target;

        public Bee(Point p) : base(p)
        {
            image = im1;           
            vectorX = rand.Next(-MAX_SPEED, MAX_SPEED);
            vectorY = rand.Next(-MAX_SPEED, MAX_SPEED);
            ID = ++startID;
            state = State.Search;
            isFull = false;
            imRadius = 10;
        }
        public override void Draw(Graphics g)
        {
            if (vectorX < 0)
            {
                if (isFull)
                    image = im4;
                else
                image = im2;
            }                
            else
            {
                if (isFull)
                    image = im3;
                else
                image = im1;
            }
                           
            g.DrawImage(image, new Rectangle(coords.X - 64 / 2, coords.Y - 64 / 2, 60, 50));
        }        
        
        public override void Live()
        {
            switch (state)
            {
                case State.Search:
                    Search();
                    break;
                case State.MoveTo:
                    MoveTo();
                    break;
                case State.TakeHoney:
                    TakeHoney();
                    break;
                case State.GiveHoney:
                    GiveHoney();
                    break;
            }

            coords.X =  Convert.ToInt32(coords.X + vectorX);
            coords.Y = Convert.ToInt32(coords.Y + vectorY);
            
            File.AppendAllText("file.txt", $"Пчела летит, векторы {vectorX}, {vectorY}\n");
            if (coords.X <= 20)
                vectorX = -vectorX;
            if (coords.X >= MaxX-50)
                vectorX = -vectorX;
            if (coords.Y <= 20)
                vectorY = -vectorY;
            if (coords.Y >= MaxY-50)
                vectorY = -vectorY;
        }

        void Search()
        {
            Flower f = Flower.Find(this);
            if (f != null && target == null)//если цветок нашелся
            {
                File.AppendAllText("file.txt", $"------------------Пчела увидела цветок\n");
                SetTarget(f); //поменять вектор в направлении к цветку
                state = State.MoveTo;
            }
            else
            {
                //продолжать летать
                File.AppendAllText("file.txt", $"Пчела не нашла цветок\n");
            }
        }
        void SetTarget(Entity e) //e - entity(цветок/улей)
        {
            File.AppendAllText("file.txt", "Вызван метод SetTarget\n");
            //находим разность координат - координаты нового вектора
            Point p = e.GetCoords();
            double vx = p.X - coords.X;
            double vy = p.Y - coords.Y;
            double dist = Math.Sqrt(vx * vx + vy * vy);
            if (dist >= 0.01)
            {
                target = e;
                vectorX = vx / dist * MAX_SPEED;
                vectorY = vy / dist * MAX_SPEED;
            }
            else
            {
                vectorX = 0;
                vectorY = 0;
            }
        }
        void MoveTo()
        {
            File.AppendAllText("file.txt", "Вызван метод MoveTo\n");

            if (target != null && target.InRadius(GetCoords(), target.imRadius)) 
            {
                coords = target.GetCoords();                
                vectorX = 0;
                vectorY = 0;
                if (isFull)
                    state = State.GiveHoney;
                else
                    state = State.TakeHoney;
                //target = null;
            }
        }
        void TakeHoney()
        {
            File.AppendAllText("file.txt",$"----------Пчела берет мед\n");
            isFull = true;
            SetTarget(hive);
            state = State.MoveTo;
        }
        void GiveHoney()
        {
            File.AppendAllText("file.txt", $"-------------Пчела кладет мед\n");
            vectorX = 0;
            vectorY = 0;
            isFull = false;
        }
    }

}

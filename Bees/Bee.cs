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
        int vectorX, vectorY;
        static Random rand = new Random();
        static int startID = 0;
        public static string textF = "";
        public enum State
        {
            Search, MoveTo, NoticeFlower, TakingHoney, GivingHoney, Birth
        };
        public State state;

        public Bee(Point p) : base(p)
        {
            image = Image.FromFile("bee1.gif");           
            vectorX = rand.Next(-MAX_SPEED, MAX_SPEED);
            vectorY = rand.Next(-MAX_SPEED, MAX_SPEED);
            ID = ++startID;
            state = State.Search;
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, new Rectangle(coords.X - 64 / 2, coords.Y - 64 / 2, 64, 64));
        }
        public override void Live()
        {

            //if (state == State.Search)
            //{
            //    coords.X += vectorX;
            //    coords.Y += vectorY;

            //    File.AppendAllText("file.txt", $"Пчела летит, векторы {vectorX}, {vectorY}\n");
            //    if (coords.X <= 0)
            //        vectorX = -vectorX;
            //    if (coords.X >= MaxX)
            //        vectorX = -vectorX;
            //    if (coords.Y <= 0)
            //        vectorY = -vectorY;
            //    if (coords.Y >= MaxY)
            //        vectorY = -vectorY;
            //    if ((Flower.Find(this) is null) == false) //если цветок нашелся
            //    {                    
            //        state = State.MoveTo;                    
            //    }
            //    else
            //    {
            //        //продолжать летать
            //        state = State.Search;
            //        File.AppendAllText("file.txt", $"Пчела не нашла цветок\n");
            //    }
            //}
            //if (state == State.MoveTo)
            //{
            //    File.AppendAllText("file.txt", $"Пчела увидела цветок\n");
            //    Point f = Flower.Find(this).GetCoords(); //поиск ближайшего цветка
            //    SetTarget(f); //поменять вектор в направлении к цветку
            //}




            coords.X += vectorX;
            coords.Y += vectorY;
            if (vectorX < 0)
                image = Image.FromFile("bee2.gif");
            else image = Image.FromFile("bee1.gif");

            File.AppendAllText("file.txt", $"Пчела летит, векторы {vectorX}, {vectorY}\n");
            if (coords.X <= 0)
                vectorX = -vectorX;
            if (coords.X >= MaxX)
                vectorX = -vectorX;
            if (coords.Y <= 0)
                vectorY = -vectorY;
            if (coords.Y >= MaxY)
                vectorY = -vectorY;

            if ((Flower.Find(this) is null) == false) //если цветок нашелся
            {
                File.AppendAllText("file.txt", $"Пчела увидела цветок\n");
                Point f = Flower.Find(this).GetCoords(); //поиск ближайшего цветка
                SetTarget(f); //поменять вектор в направлении к цветку
            }
            else
            {
                //продолжать летать
                File.AppendAllText("file.txt", $"Пчела {coords.X}, {coords.Y} не нашла цветок\n");
            }

        }

        void SetTarget(Point d) //d - координаты цветка
        {
            File.AppendAllText("file.txt", $"Пчела {coords.X}, {coords.Y} собирается лететь к цветку {d.X}, {d.Y}\n");
            //находим разность модулей - координаты нового вектора
            double x = Math.Abs(coords.X - d.X);
            double y = Math.Abs(coords.Y - d.Y);
            int vecX = Convert.ToInt32(Math.Round(x));
            int vecY = Convert.ToInt32(Math.Round(y));
            Point point = new Point(vecX, vecY);
            if ((vecX > MAX_SPEED) | (vecY > MAX_SPEED)) //если координаты больше MAX_SPEED
            {
                point = CutVector(new Point(vecX, vecY));
                vecX = point.X;
                vecY = point.Y;
            }
            //если пчела слева от цветка то значение положительное
            if (coords.X - d.X > 0) //если пчела справа от цветка
            {
                vecX = -vecX; //то значение отрицательное
            }
            //если пчела сверху от цветка то значение положительное
            if (coords.Y - d.Y > 0) //если пчела снизу цветка
            {
                vecY = -vecY; //то значение отрицательное
            }
            vectorX = vecX;
            vectorY = vecY;
            File.AppendAllText("file.txt", $"Пчела меняет вектор на {vectorX}, {vectorY}\n");
            //условия столкновения с цветком
            if (coords.X == d.X)
                TakeHoney();
            if (coords.Y == d.Y)
                TakeHoney();

        }
        Point CutVector(Point p) //укорочение вектора до величины MAX_SPEED
        {
            if (p.X > p.Y) //если вектор x больше вектора y
            {
                //поделить оба вектора на наименьшее значение                    
                p.X = p.X/ p.Y;
                p.Y = p.Y / p.Y;
            }
            else if (p.X < p.Y) //если вектор y больше вектора x
            {
                //поделить оба вектора на наименьшее значение
                p.Y = p.Y / p.X;
                p.X = p.X / p.X;                  
            }
            else if ((p.X == p.Y)) //если координаты равны
            {
                //поделить оба вектора на них же
                p.X = p.X / p.X;
                p.Y = p.Y / p.Y;
            }
            return p;
        }
        void TakeHoney()
        {
            state = State.TakingHoney;
            File.AppendAllText("file.txt",$"Пчела берет мед\n");
            vectorX = 0;
            vectorY = 0;
            //время сбора нектара
        }
    }

}

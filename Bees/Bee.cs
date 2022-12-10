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
        float vectorX, vectorY;
        static Random rand = new Random();        
        static Image im1 = Image.FromFile("bee1.gif");
        static Image im2 = Image.FromFile("bee2.gif");
        static Image im3 = Image.FromFile("beeH1.gif");
        static Image im4 = Image.FromFile("beeH2.gif");        
        bool isFull;
        public static int deathTime; //жизненный цикл рабочей пчелы
        public static int birthTime; //время возрождения пчел/размножения матки
        public static int countBees; //кол-во рабочих пчел
        public static int growTime; //время превращения яйца в пчелу
        public static int countEggs; //кол-во яиц пчел
        public int tempDeathTime;
        public int tempBirthTime;
        public int tempGrowTime;
        public int time = 0;
        HoneyComb comb;
        public enum State
        {
            Search, MoveTo, TakeHoney, GiveHoney, Birth, Death
        };
        State state;
        Entity target;

        public Bee(Point p) : base(p)
        {
            image = im2;
            ImageAnimator.Animate(image, null);
            image = im1;
            ImageAnimator.Animate(image, null);
            image = im3;
            ImageAnimator.Animate(image, null);
            image = im4;
            ImageAnimator.Animate(image, null);
            vectorX = rand.Next(-MAX_SPEED, MAX_SPEED);
            vectorY = rand.Next(-MAX_SPEED, MAX_SPEED);
            ID = startID++;
            state = State.Search;
            isFull = false;
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
            ImageAnimator.UpdateFrames();
            g.DrawImage(image, new RectangleF(coords.X - 32, coords.Y - 32, 64, 64));
        }                
        public override void Live()
        {
            Grow();
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
                case State.Death:
                    Death();
                    break;
            }

            coords.X += vectorX;
            coords.Y += vectorY;
            
            if (coords.X <= 20)
                vectorX = -vectorX;
            if (coords.X >= MaxX-50)
                vectorX = -vectorX;
            if (coords.Y <= 20)
                vectorY = -vectorY;
            if (coords.Y >= MaxY-50)
                vectorY = -vectorY;
            if (vectorX == 0 | vectorY == 0)
            {
                vectorX = rand.Next(-MAX_SPEED, MAX_SPEED);
                vectorY = rand.Next(-MAX_SPEED, MAX_SPEED);
            }
        }
        void Search()
        {
            Flower f = Flower.Find(this);
            if (f != null && target == null)//если цветок нашелся
            {            
                //File.AppendAllText("file.txt", $"------------------Пчела увидела цветок\n");
                SetTarget(f); //поменять вектор в направлении к цветку
                state = State.MoveTo;
            }
        }
        void SetTarget(Entity e) //e - entity(цветок/сота)
        {
            if (e == null) return;            
            PointF p = e.GetCoords();
            float vx = p.X - coords.X;
            float vy = p.Y - coords.Y;
            double dist = Math.Sqrt(vx * vx + vy * vy);
            if (dist >= 0.01)
            {
                target = e;
                vectorX = (float)(vx / dist * MAX_SPEED);
                vectorY = (float)(vy / dist * MAX_SPEED);
            }
            else
            {
                vectorX = 0;
                vectorY = 0;
            }
        }
        void MoveTo()
        {

            if (target != null && target.InRadius(GetCoords(), imRadius))
            {
                //File.AppendAllText("file.txt", $"-----Пчела {ID} садится на {target.name} №{target.ID} {coords.X} {coords.Y}-------\n");
                coords = target.GetCoords();                
                vectorX = 0;
                vectorY = 0;
                if (isFull)
                    state = State.GiveHoney;
                else
                    state = State.TakeHoney;
                target = null;
            }
            
        }        
        void TakeHoney()
        {
            isFull = true;
            comb = HoneyComb.FindFreeComb(); //находим свободную соту, запоминаем
            SetTarget(comb); //устанаввливаем путь к ней
            //File.AppendAllText("file.txt", $"-----Пчела {ID} бронирует {target.name} №{target.ID} пустые? {comb.k < 3} k={comb.k}\n"); 
            comb.isRezerved = true; //бронируем соту
            state = State.MoveTo;
        }
        void GiveHoney()
        {
            //File.AppendAllText("file.txt", $"------Пчела {ID} кладет мед {comb.name} №{comb.ID} пустые?{comb.k < 3} k={comb.k}\n");
            vectorX = 0;
            vectorY = 0;
            isFull = false;
            HoneyComb.PutHoney(comb);
            state = State.Search;
            
            vectorX = rand.Next(-MAX_SPEED, MAX_SPEED);
            vectorY = rand.Next(-MAX_SPEED, MAX_SPEED);
        }        
        void Grow()
        {
            time++;
            if (time > rand.Next(10,50)) state = State.Death;
        }
        void Death()
        {
            im1 = im2 = im3 = im4 = Image.FromFile("Egg.png");
            vectorX = 0;
            vectorY = 0;
            target = null;
            if (time > rand.Next(50, 100))
                entities.Remove(this);
        }
    }

}

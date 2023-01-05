using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Bees
{
    class Bee : Entity
    {
        public static List<Bee> bees = new List<Bee>();
        const int MAX_SPEED = 5;
        public static int MaxX;
        public static int MaxY;
        protected float vectorX, vectorY;
        static Random rand = new Random();        
        static Image im1 = Image.FromFile("bee1.gif");
        static Image im2 = Image.FromFile("bee2.gif");
        static Image im3 = Image.FromFile("beeH1.gif");
        static Image im4 = Image.FromFile("beeH2.gif");
        static Image im5 = Image.FromFile("sleep.png");
        static Image im6 = Image.FromFile("dead.png");
        bool isFull;
        public static int deathTime; //жизненный цикл рабочей пчелы       
        public static int countBees; //кол-во рабочих пчел              
        public int tempDeathTime;
        protected bool sleep;
        static int time = 0;
        public static bool timerTick = true;
        HoneyComb comb;
        public enum State
        {
            Search, MoveTo, TakeHoney, GiveHoney, Death, Sleep
        };
        State state;
        internal Entity target;

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
            tempDeathTime = rand.Next(50, deathTime);
            bees.Add(this);
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

            if (bees.Count > 25)
            {
                while(bees.Count == 25)
                {
                    entities.Remove(this);
                }
            }
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
                case State.Sleep:
                    Sleep();
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
            if (vectorX == 0 | vectorY == 0 && state != State.Sleep && state != State.MoveTo)
            {
                vectorX = rand.Next(-MAX_SPEED, MAX_SPEED);
                vectorY = rand.Next(-MAX_SPEED, MAX_SPEED);
            }
        }
        /// <summary>
        /// Поиск цветка
        /// </summary>
        protected void Search()
        {
            Flower f = Flower.Find(this);
            if (f != null && target == null)//если цветок нашелся
            {                          
                SetTarget(f); //поменять вектор в направлении к цветку
                state = State.MoveTo;
            }
        }
        /// <summary>
        /// Вычисляет значения векторов движения пчелы в направлении цели е 
        /// </summary>
        /// <param name="e">Объект Entity - цветок или сота</param>
        protected void SetTarget(Entity e) //e - entity(цветок/сота)
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
        /// <summary>
        /// "Сажает" пчелу на цель и вызывает следующий метод в зависимости от текущего состояния пчелы
        /// </summary>
        protected void MoveTo()
        {
            if (target != null && target.InRadius(GetCoords(), imRadius))
            {
                coords = target.GetCoords();                
                vectorX = 0;
                vectorY = 0;
                if (isFull)
                {
                    state = State.GiveHoney;
                }                   
                else
                {
                    state = State.TakeHoney;                   
                }                
                if (sleep)
                    state = State.Sleep;
                target = null;
            }           
        }        
        /// <summary>
        /// Вызывется для того чтобы пчела забрала мед из цветка
        /// </summary>
        void TakeHoney()
        {
            File.AppendAllText("file.txt", $"Вызван NakeHoney()\n ");
            isFull = true;
            comb = HoneyComb.FindFreeComb(); //находим свободную соту, запоминаем
            if (comb == null)
            {
                isFull = false;
                return;
            }
            SetTarget(comb); //устанавливаем путь к ней         
            comb.isRezerved = true; //бронируем соту
            state = State.MoveTo;
        }
        /// <summary>
        /// Вызывается для того чтобы пчела положила мед в соту
        /// </summary>
        void GiveHoney()
        {
            vectorX = 0;
            vectorY = 0;
            isFull = false;
            HoneyComb.PutHoney(comb);
            state = State.Search;
            
            vectorX = rand.Next(-MAX_SPEED, MAX_SPEED);
            vectorY = rand.Next(-MAX_SPEED, MAX_SPEED);
        }        
        /// <summary>
        /// Счетчик, который вычисляет время смерти пчелы
        /// </summary>
        void Grow()
        {
            time++;
            tempDeathTime--;
            if (tempDeathTime == 10)
                state = State.Death;                
        }
        /// <summary>
        /// Останавливает движение пчелы и удаляет ее
        /// </summary>
        void Death()
        {
            image = im6;
            vectorX = 0;
            vectorY = 0;
            target = null; //обнулить цель
            if (comb is null == false) 
                comb.isRezerved = false; //убрать бронь
            if (tempDeathTime == 0)
            {
                entities.Remove(this);
            }
            //if (time > rand.Next(50, 100))                   
        }
        /// <summary>
        /// Дает команду пчеле искать место для сна и лететь к нему
        /// </summary>
        public virtual void GoToSleep()
        {
            sleep = true;         
            comb = HoneyComb.FindPlaceSleep(); //находим свободную соту, запоминаем
            SetTarget(comb); //устанавливаем путь к ней             
            state = State.MoveTo;
            MoveTo();
            isFull = false;
            if (comb == null)
                return;                    
        }
        /// <summary>
        /// Останавливает движение пчелы и меняет ее изображение
        /// </summary>
        public void Sleep()
        {
            image = im5;
            vectorX = 0;
            vectorY = 0;
            state = State.Sleep;
        }        
    }

}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bees
{
    class Queen : Bee
    {
        Image im = Image.FromFile("queen.png");
        public enum State
        {
            Reproduce, Live
        };
        State state;
        public int k = 0;
        public Queen(Point p) : base(p)
        {
            image = im;
            state = State.Live;
        }
        public override void Live()
        {
            time++;
            if (time == 50) state = State.Reproduce;
            if (state == State.Reproduce)
                Reproduce();
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, new RectangleF(coords.X - 32, coords.Y - 32, 64, 64));
        }
        public void Reproduce() //рождение нового потомства
        {
            time++;
            Pairing(); //спаривание
            SetTarget(HoneyComb.combs[k]); //установить цель - сота
            MoveTo();  //лететь к цели
            PutEgg(); //выложить яйцо
            if (k < 3) //если не все яйца выложены
            {
                k++;
                return;
            }               
            state = State.Live;            
        }
        public void Pairing() //спаривание королевы с трутнем
        {

        }
        void MoveTo()
        {
            if (target != null && target.InRadius(GetCoords(), imRadius))
            {
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
        public void PutEgg() //выкладка яиц
        {
             new Egg(new Point(Convert.ToInt32(HoneyComb.GetEggsPlaces()[k].X), Convert.ToInt32(HoneyComb.GetEggsPlaces()[k].Y)));         
        }
    }
}

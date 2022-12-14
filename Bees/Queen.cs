using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bees
{
    class Queen : Bee
    {
        Image im = Image.FromFile("queen.gif");
        public enum State
        {
            Reproduce, Live, Sleep
        };
        State state;
        public int tempCountEggs = 1; //настоящее кол-во яиц
        public static int countEggs; //установленное кол-во яиц
        public int tempBirthTime;
        public static int birthTime; //время возрождения пчел/размножения матки
        public Queen(Point p) : base(p)
        {
            image = im;
            ImageAnimator.Animate(image, null);
            state = State.Live;
            tempBirthTime = birthTime;
            bees.Add(this);
        }
        public override void Live()
        {    
            tempBirthTime--;
            if (tempBirthTime == 0 && sleep == false)
            {
                state = State.Reproduce;                
                tempCountEggs = 1;
            }
            if (state == State.Reproduce)
                Reproduce();
        }
        public override void Draw(Graphics g)
        {
            ImageAnimator.UpdateFrames();
            g.DrawImage(image, new RectangleF(coords.X - 50, coords.Y - 50, 100, 100));
        }
        /// <summary>
        /// Вызывается для создания новых пчел
        /// </summary>
        public void Reproduce()
        {            
            PutEgg();
            if (tempCountEggs < countEggs)
            {
                tempCountEggs++;
                return;
            }
            state = State.Live;
            tempBirthTime = birthTime;
        }
        /// <summary>
        /// Создает объект яйцо
        /// </summary>
        public void PutEgg() //выкладка яиц
        {
            new Egg(new Point((int)(HoneyComb.GetEggsPlaces()[tempCountEggs].X), (int)(HoneyComb.GetEggsPlaces()[tempCountEggs].Y)));         
        }
        /// <summary>
        /// Останавливает цикл размножения матки и вводит ее в "спячку"
        /// </summary>
        public override void GoToSleep()
        {
            sleep = true;
            state = State.Live;
        }
    }
}

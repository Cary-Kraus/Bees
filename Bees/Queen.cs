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
        public Queen(Point p) : base(p)
        {
            image = im;
            state = State.Live;
        }
        public override void Live()
        {
            time++;
            if (time == 100) state = State.Reproduce;
            if (state == State.Reproduce)
                Reproduce();
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, new RectangleF(coords.X - 50, coords.Y - 50, 64, 64));
        }
        public void Reproduce() //рождение нового потомства
        {
            time++;            
            Pairing();
            PutEggs();
            state = State.Live;
        }
        public void Pairing() //спаривание королевы с трутнем
        {

        }
        public void PutEggs() //выкладка яиц
        {
            for (int i = 0; i < 3; i++)
            {
                new Egg(new Point(1270, 500 - i * 50));
            }
        }
    }
}

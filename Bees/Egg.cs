using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Bees
{
    class Egg : Bee
    {
        public static int growTime;
        int tempGrowTime;
        int id = 0;
        public static bool isBorn = true;
        public enum State
        {
            Impregnate, UnImpregnate, //оплодотворенные - рабочие, неоплодотворенные - трутни
            Grow, Done
        };
        State state;
        Image im = Image.FromFile("Egg.png");
        public Egg(Point p) : base(p)
        {
            image = im;
            id++;
            tempGrowTime = growTime;
        }
        public override void Live()
        {
            tempGrowTime--;
            if (tempGrowTime == 0)
            {
                Transformation();
            }
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(im, new RectangleF(coords.X - 12, coords.Y - 12, 24, 24));
        }
        /// <summary>
        /// Удаляет объект egg и вместо него создает объект bee
        /// </summary>
        void Transformation()
        {
            Point p = new Point((int)coords.X, (int)coords.Y );
            entities.Remove(this);
            new Bee(p);
            isBorn = true;
        }
    }
}

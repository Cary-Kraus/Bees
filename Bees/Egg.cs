using System;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Bees
{
    class Egg : Bee
    {
        public static int growTime;
        int id = 0;
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
        }
        public override void Live()
        {
            time++;
            if (time == 100) state = State.Done;
            if (state == State.Done)
                Transformation();
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(im, new RectangleF(coords.X - 12, coords.Y - 12, 24, 24));
        }
        void Transformation()
        {
            entities.Remove(this);
            new Bee(new Point(Convert.ToInt32(HoneyComb.GetEggsPlaces()[id].X), Convert.ToInt32(HoneyComb.GetEggsPlaces()[id].Y)));
        }
    }
}

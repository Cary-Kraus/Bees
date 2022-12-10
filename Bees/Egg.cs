using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bees
{
    class Egg : Bee
    {
        public enum State
        {
            Impregnate, UnImpregnate //оплодотворенные - рабочие, неоплодотворенные - трутни
        };
        State state;
        Image im = Image.FromFile("Egg.png");
        public Egg(Point p) : base(p)
        {
            image = im;
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(im, new RectangleF(coords.X - 22, coords.Y - 22, 44, 44));
        }
    }
}

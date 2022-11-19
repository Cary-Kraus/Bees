using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bees
{
    class BeeHive : Entity
    {
        static Image im = Image.FromFile("r.jpg");
        public BeeHive(Point p) :base(p)
        {
            image = im;
            imRadius = 50;
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, new Rectangle(coords.X - 64 / 2, coords.Y - 64 / 2, 100, 100));
        }

    }
}

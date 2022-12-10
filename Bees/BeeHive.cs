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
        //Image im = Image.FromFile("r.jpg");
        public BeeHive(Point p) :base(p)
        {
            //image = im;
        }
        public override void Draw(Graphics g)
        {
            //g.DrawImage(image, new Rectangle(coords.X - 64 / 2, coords.Y - 64 / 2, 100, 100));
            g.FillRectangle(new SolidBrush(Color.Moccasin), new RectangleF(coords.X, coords.Y, 250, 550));
        }

    }
}

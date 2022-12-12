using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bees
{
    class Drone : Bee
    {
        public static Image IM = Image.FromFile("bee1.gif");
        public Drone(Point p) : base(p)
        {
            image = IM;
            ImageAnimator.Animate(image, null);
        }
        void Live()
        {

        }
        public override void Draw(Graphics g)
        {
            ImageAnimator.UpdateFrames();
            g.DrawImage(image, new RectangleF(coords.X - 50, coords.Y - 50, 150, 150));          
        }
    }
}

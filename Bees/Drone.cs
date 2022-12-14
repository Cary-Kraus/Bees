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
        public static Image IM = Image.FromFile("bee2.gif");
        public Drone(Point p) : base(p)
        {
            image = IM;
            ImageAnimator.Animate(image, null);
        }
        public override void Live()
        {
            vectorX = 0;
            vectorY = 0;
        }
        public override void Draw(Graphics g)
        {
            ImageAnimator.UpdateFrames();
            g.DrawImage(image, new RectangleF(coords.X - 32, coords.Y - 32, 64, 64));          
        }
    }
}

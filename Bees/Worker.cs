using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bees
{
    class Worker : Bee
    {
        public static Image image = Image.FromFile("bee1.gif");
        public Worker(Point p) : base(p)
        {
            //image = im;
            //state = State.Live;
        }
        public override void Live()
        {

        }
        void Draw(Graphics g)
        {

        }

    }
}

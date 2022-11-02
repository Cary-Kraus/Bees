using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bees
{
    class Bee : Entity
    {
        const int MAX_SPEED = 5;
        int vectorX, vectorY;
        static Random rand = new Random();
        static int startID = 0;
        public Bee(Point p) : base(p)
        {
            image = Image.FromFile("bee1.gif");           
            vectorX = rand.Next(-MAX_SPEED, MAX_SPEED);
            vectorY = rand.Next(-MAX_SPEED, MAX_SPEED);
            ID = ++startID;
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, new Rectangle(coords.X, coords.Y, 64, 64));
        }
        public override void Live()
        {
            coords.X += vectorX;
            coords.Y += vectorY;

            if (coords.X <= 0)
            {
                vectorX = -vectorX;
            }
            if (coords.X >= 1100)
            {
                vectorX = -vectorX;
            }
            if (coords.Y <= 0)
            {
                vectorY = -vectorY;
            }
            if (coords.Y >= 700)
            {
                vectorY = -vectorY;
            }

            Flower.Find(this); //поиск ближайшего цветка
        }

    }

}

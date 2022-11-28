using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bees
{
    class HoneyComb : BeeHive
    {
        //static bool[] honey = new bool[19];
        static HoneyComb[] combs = new HoneyComb[90];
        bool isEmpty;
        bool isRezerved;
        static int l = 0;
        int k = 0;
        Image imComb0 = Image.FromFile("comb0.png");
        Image imComb1 = Image.FromFile("comb1.png");
        Image imComb2 = Image.FromFile("comb2.png");
        Image imComb3 = Image.FromFile("comb3.png");
        
        public HoneyComb(Point p) : base(p)
        {
            image = imComb0;
            imRadius = 5;
            combs[l] = this;
            l++;
            isEmpty = true;
            ID = startID++;
        }

        public override void Draw(Graphics g)
        {
            switch (k)
            {
                case 0:
                    image = imComb0;
                    break;
                case 1:
                    image = imComb1;
                    break;
                case 2:
                    image = imComb2;
                    break;
                case 3:
                    image = imComb3;
                    break;
            }
            g.DrawImage(image, new RectangleF(coords.X, coords.Y, 44, 44));
        }
        public override void Live()
        {
            
        }
        public static HoneyComb FindEmptyComb()
        {
            foreach (var item in combs)
            {
                if (item.isEmpty)
                {
                    return item;
                }
            }
            return null;
        }
        public static HoneyComb FindFreeComb()
        {
            foreach (var item in combs)
            {
                if (!item.isRezerved)
                {
                    item.isRezerved = true;
                    return item;
                }
            }
            return null;
        }
        public static void PutHoney(HoneyComb honeyComb) //положить мед в соты
        {
            if (honeyComb.k > 3)
                honeyComb.isEmpty = false; //полностью заполнить текущие соты
            else honeyComb.k++;
        }

    }
}

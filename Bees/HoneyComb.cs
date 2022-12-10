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
        public bool isRezerved;
        public bool isBusy;
        static int l = 0;
        public int k = 0;
        Image[] imComb = new Image[] {Image.FromFile("comb0.png"),
        Image.FromFile("comb1.png"),Image.FromFile("comb2.png"),Image.FromFile("comb3.png")};
        Queue<HoneyComb> queueCombs = new Queue<HoneyComb>();

        public HoneyComb(Point p) : base(p)
        {
            combs[l] = this;
            l++;
            ID = startID++;
            name = "Соты";
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(imComb[k], new RectangleF(coords.X - 22, coords.Y - 22, 44, 44));
        }
        public static HoneyComb FindFreeComb() //поиск не занятой другой пчелой соты
        {
             foreach (var item in combs)
             {               
                if (item.k < 3 && !item.isRezerved)
                    return item;
             }
            return null;
        }
        public static void PutHoney(HoneyComb honeyComb) //положить мед в соты
        {
            honeyComb.isRezerved = false;
            if (honeyComb.k < 3)
            {
                honeyComb.k++;
            }
        }

    }
}

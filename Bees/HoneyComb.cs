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
        internal static HoneyComb[] combs = new HoneyComb[84];
        public bool isRezerved;
        public bool isBusy;
        static int l = 0;
        public int k = 0;
        Image[] imComb = new Image[] {Image.FromFile("comb0.png"),
        Image.FromFile("comb1.png"),Image.FromFile("comb2.png"),Image.FromFile("comb3.png")};
        static PointF[] places = new PointF[3];

        public HoneyComb(Point p) : base(p)
        {
            combs[l] = this;
            l++;
            ID = startID++;
            name = "Соты";           
            if (ID > 41)
                isRezerved = true;
            if (ID > 42 && ID < 49 && ID % 2 == 1)
                isRezerved = false;
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
                honeyComb.k++;
            //if (honeyComb.k == 3 && honeyComb.ID > 5)
            //    Bee.timerTick = false;
        }
        public static PointF[] GetEggsPlaces()
        {
            places[0] = combs[65].coords;
            places[1] = combs[66].coords;
            places[2] = combs[67].coords;
            return places;
        }
        public static PointF GetQueenPlace()
        {
            return combs[73].coords;
        }
    }
}

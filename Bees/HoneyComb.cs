using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public bool ISRezerved;
        public static bool isAllFull;
        static int l = 0;
        public int k = 0;
        static int s = 0;
        Image[] imComb = new Image[] {Image.FromFile("comb0.png"),
        Image.FromFile("comb1.png"),Image.FromFile("comb2.png"),Image.FromFile("comb3.png")};
        static PointF[] places = new PointF[4];
        //int[] dronesPlaces = new int[] { 70, 71, 75, 76};

        public HoneyComb(Point p) : base(p)
        {
            combs[l] = this;
            l++;
            ID = startID++;
            name = "Соты";
            ISRezerved = false;
            isBusy = false;
            if (ID > 41)
                isRezerved = true;
            if (ID > 42 && ID < 49 && ID % 2 == 1)
                isRezerved = false;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(imComb[k], new RectangleF(coords.X - 22, coords.Y - 22, 44, 44));
        }
        public override void Live()//проверка всех сот на заполненность
        {
            
            if (s == 2)
            {
                foreach (var item in Bee.bees)
                {
                    item.GoToSleep();
                }               
            }                
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
            if (honeyComb.k == 3)
                s++;
            //File.AppendAllText("file.txt", $"соты {honeyComb.ID} заполнены до {honeyComb.k} уровня\n ");
            //File.AppendAllText("file.txt", $"s = {s}\n ");
        }
        public static PointF[] GetEggsPlaces()
        {
            places[1] = combs[67].coords;
            places[2] = combs[68].coords;
            places[3] = combs[75].coords;
            return places;
        }
        public static PointF GetQueenPlace()
        {
            return combs[73].coords;
        }
        public static PointF[] GetDronesPlaces()
        {
            places[0] = combs[64].coords;
            places[1] = combs[71].coords;
            places[2] = combs[78].coords;
            return places;
        }
        public static HoneyComb FindPlaceSleep()
        {
            foreach (var item in combs)
            {
                if (!item.ISRezerved && item.ID <= 49)
                {
                    //File.AppendAllText("file.txt", $"Найдена свободная сота {item.ID}\n ");
                    item.ISRezerved = true;
                    return item;
                }                    
            }
            return null;
        }
    }
}

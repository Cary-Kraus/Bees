using System.Drawing;

namespace Bees
{
    class HoneyComb : Entity
    {
        internal static HoneyComb[] combs = new HoneyComb[84];
        public bool isRezerved;
        public bool isBusy;
        public bool ISRezerved;
        static int l = 0;
        public int k = 0;
        static int s = 0;
        Image[] imComb = new Image[] {Image.FromFile("comb0.png"),
        Image.FromFile("comb1.png"),Image.FromFile("comb2.png"),Image.FromFile("comb3.png")};
        static PointF[] places = new PointF[4];

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
        public override void Live()
        {            
                          
        }
        /// <summary>
        /// Ищет свободную соту и возвращает ее. Если сота не найдена, возвращает null
        /// </summary>
        /// <returns></returns>
        public static HoneyComb FindFreeComb() //поиск не занятой другой пчелой соты
        {
             foreach (var item in combs)
             {
                if (item.k < 3 && !item.isRezerved)
                    return item;
             }
            return null;
        }
        /// <summary>
        /// Заполняет соту медом на определенный уровень и подсчитывает кол-во заполненных сот
        /// </summary>
        public static void PutHoney(HoneyComb honeyComb) //положить мед в соты
        {
            honeyComb.isRezerved = false;
            if (honeyComb.k < 3)
                honeyComb.k++;
            if (honeyComb.k == 3)
                s++;
            if (s == 41)
                foreach (var item in Bee.bees)
                    item.GoToSleep();
        }
        /// <summary>
        /// Возвращает массив с номерами сот, на которых должны находиться яйца
        /// </summary>
        public static PointF[] GetEggsPlaces()
        {
            places[1] = combs[67].coords;
            places[2] = combs[68].coords;
            places[3] = combs[75].coords;
            return places;
        }
        /// <summary>
        /// Возвращает координаты соты, на которой должна находиться матка
        /// </summary>
        public static PointF GetQueenPlace()
        {
            return combs[73].coords;
        }
        /// <summary>
        /// Возвращает массив с номерами сот, на которых должны находиться трутни
        /// </summary>
        public static PointF[] GetDronesPlaces()
        {
            places[0] = combs[64].coords;
            places[1] = combs[71].coords;
            places[2] = combs[78].coords;
            return places;
        }
        /// <summary>
        /// Находит свободную для сна пчелы соту и возвращает ее.
        /// Если такая сота не найдена, возвращает null
        /// </summary>
        public static HoneyComb FindPlaceSleep()
        {
            foreach (var item in combs)
            {
                if (!item.ISRezerved && item.ID <= 49)
                {
                    item.ISRezerved = true;
                    return item;
                }                  
            }
            return null;
        }
        /// <summary>
        /// Обнуляет статические переменные
        /// </summary>
        public static void Reset()
        {
            l = s = 0;
        }
    }
}

using System.Collections.Generic;
using System.Drawing;

namespace Bees
{
    class Flower : Entity
    {
        static List<Flower> flowers = new List<Flower>();
        static int radius;
        static int startID = 0;
        public bool isBusy;
        
        public Flower(Point p, string picName) : base(p)
        {
            radius = 100;
            image = Image.FromFile(picName);
            flowers.Add(this);
            ID = ++startID;
            isBusy = false;
            name = "Цветок";
        }
        public override void Live()
        {
            
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, new RectangleF(coords.X - 50, coords.Y - 50, 100, 100));            
        }
        /// <summary>
        /// Ищет ближайший к пчеле цветок и возвращает его.
        /// Если цветок не найден, возвращает null
        /// </summary>
        public static Flower Find(Bee bee)
        {
            PointF p = bee.GetCoords(); //координаты пчелы
            foreach (var flower in flowers) //вычисление минимального расстояния от пчелы до цветка из списка цветов
            {
                if (flower.InRadius(p, radius))
                    return flower;
            }
            return null;//цветок не найден
        }
    }
}

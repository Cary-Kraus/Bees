using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bees
{
    class Flower : Entity
    {
        static List<Flower> flowers = new List<Flower>();
        int radius;
        static int startID = 0;

        public Flower(Point p, string picName, int radius) :base(p)
        {
            this.radius = radius;
            image = Image.FromFile(picName);
            flowers.Add(this);
            ID = ++startID;
        }
        public override void Live()
        {
            
        }
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, new Rectangle(coords.X, coords.Y, 100, 100));
        }

        bool IsEmpty()
        {
            return true;
        }
        public static Flower Find(Bee bee) //поиск ближайшего к пчеле цветка
        {
            Point p = bee.GetCoords();
            double minDistance = double.MaxValue;
            Flower flowerMin = null;

            foreach (var flower in flowers) //вычисление минимального расстояния от пчелы до цветка из списка цветов
            {
                double x = (p.X - flower.GetCoords().X);
                double y = (p.Y - flower.GetCoords().Y);
                double distance = Math.Sqrt(x * x + y * y);
                if (distance < minDistance)
                {
                    minDistance = distance; //мин расстояние
                    flowerMin = flower; //цветок с мин расстоянием (ближайший)
                }                   
            }
            string xStr = flowerMin?.GetCoords().X.ToString();
            string yStr = flowerMin?.GetCoords().Y.ToString();
            File.AppendAllText("file.txt", $"id пчелы: {bee.ID} id цветка: {flowerMin?.ID}, координаты цветка: {xStr} {yStr}\n");
            return flowerMin;
        }
    }
}

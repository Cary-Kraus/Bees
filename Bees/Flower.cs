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
            //g.FillEllipse(new SolidBrush(Color.FromArgb(90, Color.OliveDrab)), coords.X - radius, coords.Y - radius, radius * 2, radius * 2);
            g.DrawImage(image, new RectangleF(coords.X - 50, coords.Y - 50, 100, 100));            
        }

        bool IsEmpty()
        {
            return true;
        }
        public static Flower Find(Bee bee) //поиск ближайшего к пчеле цветка
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

﻿using System;
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
        bool isBusy;

        public Flower(Point p, string picName) :base(p)
        {
            radius = 100;
            image = Image.FromFile(picName);
            flowers.Add(this);
            ID = ++startID;
            isBusy = false;
        }
        public override void Live()
        {
            
        }
        public override void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.FromArgb(90, Color.OliveDrab)), coords.X - radius, coords.Y - radius, radius * 2, radius * 2);
            g.DrawImage(image, new Rectangle(coords.X - 50, coords.Y - 50, 100, 100));            
        }

        bool IsEmpty()
        {
            return true;
        }
        public static Flower Find(Bee bee) //поиск ближайшего к пчеле цветка
        {
            Flower flowerMin = null; //цветок на мин дистанции
            double minDistance = double.MaxValue;//минимальная дистанция
            Point p = bee.GetCoords(); //координаты пчелы
            double x = 0;
            double y = 0;
            foreach (var flower in flowers) //вычисление минимального расстояния от пчелы до цветка из списка цветов
            {
                x = p.X - flower.GetCoords().X; 
                y = p.Y - flower.GetCoords().Y;
                double distance = Math.Sqrt(x * x + y * y); //расстояние между цветком и пчелой
                if (distance < minDistance)
                {
                    minDistance = distance; //мин расстояние
                    flowerMin = flower; //цветок с мин расстоянием (ближайший)
                }   
            }
            if (Math.Abs(x) <= radius & Math.Abs(y) <= radius) //если пчела видит цветок
            {
                return flowerMin;
            }
            else return null;//цветок не найден

        }
    }
}

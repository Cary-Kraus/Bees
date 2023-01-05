using System;
using System.Collections.Generic;
using System.Drawing;

namespace Bees
{
    class Entity
    {
        protected static List<Entity> entities = new List<Entity>();
        protected PointF coords;
        public int ID;
        public static int startID = 0;
        protected Image image;
        public static int imRadius = 4;
        public string name;
        public Entity(Point p)
        {
            coords = p;
            image = null;
            entities.Add(this);
        }
        public virtual void Live()
        {

        }
        public virtual void Draw(Graphics g)
        {
            g.DrawImage(image, new PointF(coords.X - image.Width / 2, coords.Y - image.Height / 2));
        }
        /// <summary>
        /// Возвращает координаты сущности
        /// </summary>
        /// <returns></returns>
        public PointF GetCoords()
        {
            return coords;
        }
        /// <summary>
        /// Вычисляет расстояние между пчелой и объектом и возвращает значение "true"
        /// если пчела находится в пределах radius и "false" в обратном случае
        /// </summary>
        /// <param name="p">Координаты пчелы</param>
        /// <param name="radius">Минимальное расстояние между пчелой и объектом</param>
        /// <returns></returns>
        public bool InRadius(PointF p, int radius)
        {
            double x = p.X - coords.X;
            double y = p.Y - coords.Y;
            double distance = Math.Sqrt(x * x + y * y); //расстояние между цветком и пчелой
            return distance <= radius;
        }
        /// <summary>
        /// Возвращает список всех сущностей
        /// </summary>
        /// <returns></returns>
        public static List<Entity> GetAll()
        {
            return entities;
        }
        /// <summary>
        /// Удаляет всех сущностей
        /// </summary>
        public static void Reset()
        {
            entities.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bees
{
    class Entity
    {
        static List<Entity> entities = new List<Entity>();
        protected Point coords;
        public int ID;
        protected Image image;
        public static BeeHive hive;
        public int imRadius;

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
            g.DrawImage(image, new Point(coords.X - image.Width / 2, coords.Y - image.Height / 2));
        }
        public Point GetCoords()
        {
            return coords;
        }

        public bool InRadius(Point p, int radius)
        {
            int x = p.X - coords.X;
            int y = p.Y - coords.Y;
            int distance = x * x + y * y; //расстояние между цветком и пчелой
            return distance <= radius * radius;
        }
        public static List<Entity> GetAll()
        {
            return entities;
        }
    }
}

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
        protected PointF coords;
        public int ID;
        public static int startID = 0;
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
            g.DrawImage(image, new PointF(coords.X - image.Width / 2, coords.Y - image.Height / 2));
        }
        public PointF GetCoords()
        {
            return coords;
        }

        public bool InRadius(PointF p, int radius)
        {
            double x = p.X - coords.X;
            double y = p.Y - coords.Y;
            double distance = Math.Sqrt(x * x + y * y); //расстояние между цветком и пчелой
            return distance <= radius;
        }
        public static List<Entity> GetAll()
        {
            return entities;
        }
    }
}

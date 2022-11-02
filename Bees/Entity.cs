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
            //g.DrawImage(image, new Rectangle(coords.X, coords.Y, 64, 64));
            g.DrawImage(image, new Point(coords.X, coords.Y));
        }
        public Point GetCoords()
        {
            return coords;
        }
        public static List<Entity> GetAll()
        {
            return entities;
        }
    }
}

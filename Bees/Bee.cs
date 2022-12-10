using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bees
{
    class Bee : Entity
    {
        static Random rand = new Random();        
        //static Image im1 = Image.FromFile("bee1.gif");
        //static Image im2 = Image.FromFile("bee2.gif");
        //static Image im3 = Image.FromFile("beeH1.gif");
        //static Image im4 = Image.FromFile("beeH2.gif");        
        public static int deathTime; //жизненный цикл рабочей пчелы
        public static int birthTime; //время возрождения пчел/размножения матки
        public static int countBees; //кол-во рабочих пчел
        public static int growTime; //время превращения яйца в пчелу
        public static int countEggs; //кол-во яиц пчел
        public int tempDeathTime;
        public int tempBirthTime;
        public int tempGrowTime;
        public int time = 0;

        public Bee(Point p) : base(p)
        {
            ID = startID++;
        }
        public override void Draw(Graphics g)
        {
                        
        }                
        public override void Live()
        {
          
        }
        
        public virtual void Death()
        {
            if (time > rand.Next(50, 100))
                entities.Remove(this);
        }
    }

}

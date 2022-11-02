using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bees
{
    public partial class Form1 : Form
    {
        static Timer timer = new Timer();

        static Bitmap bitmap;
        static Graphics graphics;

        internal void TimerStart()
        {
            timer.Interval = 67;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += (args, e) =>
            {
                foreach (var item in Entity.GetAll()) //медот "жить" у всех сущностей
                    item.Live();
                Refresh();
            };
        }    

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();            
            for (int i = 0; i < 10; i++) //случайная генерация пчел
                new Bee(new Point(rand.Next(Width), rand.Next(Height)));
            for (int i = 0; i < 15; i++) //случайная генерация цветов
                new Flower(new Point(rand.Next(Width / 2), rand.Next(Height)), "4.png", 3);
            TimerStart();
        }        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var entity in Entity.GetAll()) //отрисовка всех сущностей
                entity.Draw(e.Graphics);           
        }
        public Form1()
        {
            InitializeComponent();
        }
    }
}

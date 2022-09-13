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
        Bee bee1 = new Bee();
        static PictureBox pictureBox1 = new PictureBox()
        {
            BackColor = Color.Transparent,
            Size = new Size(1200, 700)
        };
        static Bitmap bitmap;
        static Graphics graphics;
        
        internal void TimerStart()
        {
            int i = 0;
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += (args, e) =>
            {
                i++;
                bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                graphics = Graphics.FromImage(bitmap);
                graphics.FillRectangle(new SolidBrush(Color.Black), bee1.x + i, bee1.y + i, 8, 8);
                pictureBox1.Image = bitmap;
            };
        }    

        private void button1_Click(object sender, EventArgs e)
        {
            TimerStart();
        }
        public Form1()
        {
            InitializeComponent();
            Controls.Add(pictureBox1);
        }
    }
}

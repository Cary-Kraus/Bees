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
        static bool UpMove, LeftMove;

        static Bitmap bitmap;
        static Graphics graphics;

        static PictureBox picBee2 = new PictureBox()
        {
            Location = new Point(100,200),
            BackColor = Color.Transparent,
            Size = new Size(64, 64),           
            Image = Image.FromFile("bee1.gif"),
            SizeMode = PictureBoxSizeMode.Zoom
        }; //картинка пчелы 2

        internal void TimerStart()
        {
            int i = 0;
            timer.Interval = 10;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += (args, e) =>
            {
                if (LeftMove == true)
                    picBee2.Left += 1;
                else picBee2.Left -= 1;
                if (UpMove == true)
                    picBee2.Top += 1;
                else picBee2.Top -= 1;

                if (picBee2.Left <= ClientRectangle.Left)
                    LeftMove = true;
                if (picBee2.Right >= ClientRectangle.Right)
                    LeftMove = false;
                if (picBee2.Top <= ClientRectangle.Top)
                    UpMove = true;
                if (picBee2.Bottom >= ClientRectangle.Bottom)
                    UpMove = false;
                //picBee2.Left++;
                Refresh();
            };
        }    

        private void button1_Click(object sender, EventArgs e)
        {
            TimerStart();
        }        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //сюда отрисовка
            e.Graphics.DrawImage(picBee2.Image, new Rectangle(100, 100, 64, 64));
            //e.Graphics.DrawImage(Image.FromFile("bee1.gif"), new Rectangle(50,50,64,64)); //картинка пчелы 1 (не работает)
            Controls.Add(picBee2);
        }
        public Form1()
        {
            InitializeComponent();
        }

    }
}

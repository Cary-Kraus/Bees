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
            CreateParamsPanel();
        }        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var entity in Entity.GetAll()) //отрисовка всех сущностей
                entity.Draw(e.Graphics);           
        }

        private void CreateParamsPanel()//панель параметров игры задаваемых пользователем
        {
            Panel panelParams = new Panel()
            {
                Size = new Size(Width, Height),
                //Margin = new Padding(100)
            }; //панель
            Controls.Add(panelParams);

            Button buttonConfirm = new Button()
            {
                Size = new Size(Width/4, Height/10),
                Location = new Point(600, 600),
                Text = "Применить",
                Font = new Font(FontFamily.GenericSerif, 20)
            }; //кнопка подтверждения
            panelParams.Controls.Add(buttonConfirm);

            //TrackBar[] arrTrackBar = new TrackBar[5]; //ползунки
            //for (int i = 0; i < arrTrackBar.Length; i++)
            //{
            //    arrTrackBar[i] = new TrackBar()
            //    {
            //        Maximum = 50,
            //        TickFrequency = 5,
            //        LargeChange = 3,
            //        SmallChange = 2,
            //        Location = new Point(600, (i + 1) * 100),
            //        Size = new Size(Width / 3, Height / 10)
            //    };
            //    panelParams.Controls.Add(arrTrackBar[i]);
            //}

            Label[] arrLabel = new Label[5]; //значения ползунков
            for (int i = 0; i < arrLabel.Length; i++)
            {
                arrLabel[i] = new Label()
                {
                    Location = new Point(860, (i + 1) * 100 - 15),
                    Size = new Size(Width / 5, Height / 5),
                    Font = new Font(FontFamily.GenericSerif, 20),
                    //Text = arrTrackBar[i].Value.ToString()
                    Text = "0"
                };
                panelParams.Controls.Add(arrLabel[i]);
            }

            //arrTrackBar[0].Scroll += (sender, args) => { arrLabel[0].Text = "" + arrTrackBar[0].Value; };
            //arrTrackBar[1].Scroll += (sender, args) => { arrLabel[1].Text = "" + arrTrackBar[1].Value; };
            //arrTrackBar[2].Scroll += (sender, args) => { arrLabel[2].Text = "" + arrTrackBar[2].Value; };
            //arrTrackBar[3].Scroll += (sender, args) => { arrLabel[3].Text = "" + arrTrackBar[3].Value; };
            //arrTrackBar[4].Scroll += (sender, args) => { arrLabel[4].Text = "" + arrTrackBar[4].Value; };
        }
        public Form1()
        {
            InitializeComponent();
        }
    }
}

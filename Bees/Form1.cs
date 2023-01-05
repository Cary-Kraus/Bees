using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Bees
{
    public partial class Form1 : Form
    {
        static Timer timer = new Timer();
        /// <summary>
        /// Запускает таймер
        /// </summary>
        internal void TimerStart()
        {
            if (Bee.timerTick == false)
                timer.Stop();
            else
            {
                timer.Interval = 1;
                timer.Enabled = true;
                timer.Start();
                timer.Tick += (args, e) =>
                {
                    List<Entity> ents = Entity.GetAll();
                    for (int i = 0; i < ents.Count; i++) //медот "жить" у всех сущностей
                        ents[i].Live();
                    Refresh();
                };
            }
        }      
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var entity in Entity.GetAll())
                entity.Draw(e.Graphics);           
        }
        /// <summary>
        /// Создает и настраивает элементы формы и их поведение
        /// </summary>
        private void GreateFormObjects()
        {
            Panel panelParams = new Panel()
            {
                Size = new Size(Width, Height)
            }; 
            Controls.Add(panelParams);            

            TrackBar[] arrTrackBar = new TrackBar[5]; //ползунки
            for (int i = 0; i < arrTrackBar.Length; i++)
            {
                arrTrackBar[i] = new TrackBar()
                {
                    Maximum = 300,
                    Minimum = 100,
                    TickFrequency = 5,
                    LargeChange = 3,
                    SmallChange = 2,
                    Location = new Point(700, (i + 1) * 100),
                    Size = new Size(Width / 3, Height / 10)
                };
                panelParams.Controls.Add(arrTrackBar[i]);
            }
            arrTrackBar[0].Minimum = 1;
            arrTrackBar[0].Maximum = 15;
            arrTrackBar[1].Maximum = 1000;
            arrTrackBar[1].Minimum = 10;
            arrTrackBar[1].TickFrequency = 100;
            arrTrackBar[2].Minimum = 200;
            arrTrackBar[2].Maximum = 300;
            arrTrackBar[3].Minimum = 50;
            arrTrackBar[3].Maximum = 80;
            arrTrackBar[4].Minimum = 1;
            arrTrackBar[4].Maximum = 3;

            Label[] arrLabel = new Label[5]; //значения ползунков
            for (int i = 0; i < arrLabel.Length; i++)
            {
                arrLabel[i] = new Label()
                {
                    Location = new Point(860, (i + 1) * 100 - 45),
                    Size = new Size(Width / 3, Height / 10),
                    Font = new Font("Comic Sans MS", 20),
                    ForeColor = Color.FromArgb(1, 52, 8),
                    Text = arrTrackBar[i].Value.ToString()
                };
                panelParams.Controls.Add(arrLabel[i]);
            }
            Label[] arrLabelnames = new Label[5];
            {
                for (int i = 0; i < arrLabelnames.Length; i++)
                {
                    arrLabelnames[i] = new Label()
                    {
                        Location = new Point(80, (i + 1) * 100),
                        Size = new Size(Width / 3, Height / 10),
                        Font = new Font("Comic Sans MS", 20),
                        ForeColor = Color.FromArgb(1, 52, 8)
                    };
                    panelParams.Controls.Add(arrLabelnames[i]);
                }
            }
            arrLabelnames[0].Text = "Введите кол-во пчел";
            arrLabelnames[1].Text = "Введите время смерти пчел";
            arrLabelnames[2].Text = "Введите время воспроизведения новых пчел";
            arrLabelnames[3].Text = "Введите время превращения яиц в пчел";
            arrLabelnames[4].Text = "Введите кол-во яиц в кладке";

            arrTrackBar[0].Scroll += (sender, args) => { arrLabel[0].Text = "" + arrTrackBar[0].Value; };
            arrTrackBar[1].Scroll += (sender, args) => { arrLabel[1].Text = "" + arrTrackBar[1].Value; };
            arrTrackBar[2].Scroll += (sender, args) => { arrLabel[2].Text = "" + arrTrackBar[2].Value; };
            arrTrackBar[3].Scroll += (sender, args) => { arrLabel[3].Text = "" + arrTrackBar[3].Value; };
            arrTrackBar[4].Scroll += (sender, args) => { arrLabel[4].Text = "" + arrTrackBar[4].Value; };

            Button buttonConfirm = new Button()
            {
                Size = new Size(Width / 4, Height / 10),
                Location = new Point(300, 600),
                Text = "Применить",
                BackColor = Color.SeaGreen,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Comic Sans MS", 25),
                ForeColor = Color.FromArgb(1, 52, 8)
            };
            buttonConfirm.Click += (sender, args) =>
            {
                HoneyComb.Reset();
                Entity.Reset();

                Random rand = new Random();
                Bee.countBees = Convert.ToInt32(arrLabel[0].Text);
                Bee.deathTime = Convert.ToInt32(Convert.ToInt32(arrLabel[1].Text)* 10);
                Queen.birthTime = Convert.ToInt32(arrLabel[2].Text);
                Egg.growTime = Convert.ToInt32(arrLabel[3].Text);
                Queen.countEggs = Convert.ToInt32(arrLabel[4].Text);
                panelParams.Visible = false;
                Bee.MaxX = Width;
                Bee.MaxY = Height;
              
                GreateGameObjects();
            };
            panelParams.Controls.Add(buttonConfirm);

            Button buttonExit = new Button()
            {
                Size = new Size(Width / 4, Height / 10),
                Location = new Point(900, 600),
                Text = "Назад",
                BackColor = Color.SeaGreen,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Comic Sans MS", 25),
                ForeColor = Color.FromArgb(1, 52, 8)
            };
            buttonExit.Click += (sender, args) =>
            {
                panelParams.Visible = false;                
            };
            panelParams.Controls.Add(buttonExit);
            buttonExit.Visible = false;

            Button buttonParams = new Button()
            {
                Size = new Size(150, 50),
                Location = new Point(1350, 30),
                Text = "Параметры",
                BackColor = Color.SeaGreen,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Comic Sans MS", 15),
                ForeColor = Color.FromArgb(1, 52, 8)
            };
            buttonParams.Click += (sender, args) =>
            {
                panelParams.Visible = true;
                buttonExit.Visible = true;
            };
            Controls.Add(buttonParams);            
        }
        /// <summary>
        /// Создает сущностей на форме
        /// </summary>
        private void GreateGameObjects()
        {
            Random rand = new Random();
            for (int j = 0; j < 12; j++)
            {
                if (j % 2 != 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        new HoneyComb(new Point(Width - 173 - i * 44, Height / 5 + 67 + j * 33));
                    }
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                    {
                        new HoneyComb(new Point(Width - 150 - i * 44, Height / 5 + 67 + j * 33));
                    }
                }
            }
            for (int i = 0; i < 10; i++)
                new Flower(new Point(rand.Next(50, Width / 2), rand.Next(50, Height - 50)), $"{rand.Next(1, 5)}.png");
            for (int i = 0; i < Bee.countBees; i++)
                new Bee(new Point(rand.Next(50, Width - 200), rand.Next(50, Height - 50)));
            for (int i = 0; i < 3; i++)
            {
                new Drone(new Point((int)(HoneyComb.GetDronesPlaces()[i].X), (int)(HoneyComb.GetDronesPlaces()[i].Y)));
            }
            Queen queen = new Queen(new Point((int)(HoneyComb.GetQueenPlace().X), (int)(HoneyComb.GetQueenPlace().Y)));
            TimerStart();           
        }
        /// <summary>
        /// Начальный экран приложения
        /// </summary>
        private void StartMenu()
        {
            Panel menuPanel = new Panel()
            {
                Size = new Size(Width, Height),
                BackColor = Color.DarkSeaGreen,               
            };
            menuPanel.Click += (object sender, EventArgs e) => { menuPanel.Visible = false; };
            Controls.Add(menuPanel);
            PictureBox picMenu = new PictureBox()
            {
                Image = Image.FromFile("5.png"),
                Size = new Size(700, 700),
                Location = new Point(Width/2 - 350, Height/2 - 350),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            menuPanel.Controls.Add(picMenu);
            GreateFormObjects();
        }
        
        private void buttonPause_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            File.WriteAllText("file.txt", "");
            Application.Restart();
        }
        private void buttonResume_Click(object sender, EventArgs e)
        {
            timer.Start();
        }       
        private void buttonParams_Click(object sender, EventArgs e)
        {
            GreateFormObjects();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            StartMenu();           
        }
        private void buttonMenu_Click(object sender, EventArgs e)
        {
            StartMenu();
        }        
        public Form1()
        {
            InitializeComponent();
        }
    }
}

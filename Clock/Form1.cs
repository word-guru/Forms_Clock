using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        int _x;
        int _y;
        int secHand = 100, minHand = 80, hrHand = 55;

        Timer timer = new Timer();
        public Form1()
        {
           

            InitializeComponent();

            _x = pictureBox.Size.Width / 2;
            _y = pictureBox.Size.Height / 2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Interval = 1000;
            timer.Tick += new EventHandler(this.timer_Tick);
            timer.Start();
        }
        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = _x + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = _y - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = _x - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = _y - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;

            if (val >= 0 && val <= 180)
            {
                coord[0] = _x + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = _y - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = _x - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = _y - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            //Берём время.
            int s = DateTime.Now.Second;
            int m = DateTime.Now.Minute;
            int h = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            Graphics g = pictureBox.CreateGraphics();

            // Стираем предыдущее положения секундной стрелки
            handCoord = msCoord(s, secHand + 4);
            g.DrawLine(new Pen(Color.White, 45f), new Point(_x, _y), new Point(handCoord[0], handCoord[1]));

            // Стираем предыдущее положение минутной стрелки
            handCoord = msCoord(m, minHand + 4);
            g.DrawLine(new Pen(Color.White, 40f), new Point(_x, _y), new Point(handCoord[0], handCoord[1]));

            // Стираем предыдущее положение часовой стрелки
            handCoord = hrCoord(h % 12, m, hrHand + 4);
            g.DrawLine(new Pen(Color.White, 20f), new Point(_x, _y), new Point(handCoord[0], handCoord[1]));


            //Отрисовка стрелки часов.
            handCoord = hrCoord(h % 12, m, hrHand);
            g.DrawLine(new Pen(Color.Gray, 4f), new Point(_x, _y), new Point(handCoord[0], handCoord[1]));


            //Отрисовка минутной стрелки
            handCoord = msCoord(m, minHand);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(_x, _y), new Point(handCoord[0], handCoord[1]));

            // Отрисовка секундной стрелки.
            handCoord = msCoord(s, secHand);
            g.DrawLine(new Pen(Color.DarkOrange, 2f), new Point(_x, _y), new Point(handCoord[0], handCoord[1]));
        }
       
    }
}

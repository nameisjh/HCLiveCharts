using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCLiveCharts
{
    public partial class HCCircleChart : UserControl
    {
        
        private Dictionary<string, int> values = new Dictionary<string, int>();
        private Dictionary<string, double> angles = new Dictionary<string, double>();
        private Point center;
        private List<Color> colors = new List<Color>() {Color.FromArgb(93, 223, 255), Color.FromArgb(152, 246, 171), Color.FromArgb(255, 216, 126), Color.FromArgb(255, 172, 199) };
        private int radius = 120;
        private int maxRadius = 140;
        private int centerRadius = 60;
        private int diffRadius = 0;
        private Point mousePoint;
        private string key = "";
        private Color centerColor = Color.White;

        [Category("Values"), Description("Values"), Browsable(true)]
        public Dictionary<string, int> Values { get => values; set => values = value; }

        [Category("Colors"), Description("Colors"), Browsable(true)]
        public List<Color> Colors { get => colors; set => colors = value; }

        public int Radius { get => radius; set => radius = value; }

        public int MaxRadius { get => maxRadius; set => maxRadius = value; }

        public int CenterRadius { get => centerRadius; set => centerRadius = value; }

        public Color CenterColor { get => centerColor; set => centerColor = value; }

        public HCCircleChart ()
        {
            InitializeComponent();
            diffRadius = MaxRadius - Radius;
            Values.Add("动漫", 25);
            Values.Add("电影", 25);
            Values.Add("鬼畜", 25);
            Values.Add("音乐", 25);
        }

        private void CircleChart_Paint(object sender, PaintEventArgs e)
        {
            Draw(null);
        }

        private void Draw(string key = null)
        {
            center = new Point(this.Width / 2, this.Height / 2);
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Graphics gBmp = Graphics.FromImage(bmp);
            gBmp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (key == null)
            {
                DrawChart(gBmp);
            }
            else
            {
                DrawChart(gBmp, key);
            }

            g.DrawImage(bmp, 0, 0);
        }

        private void DrawChart(Graphics g)
        {
            g.Clear(this.BackColor);
            double sum = 0;
            foreach (var item in Values)
            {
                sum += item.Value;
            }

            double unit = 360d / 100d;
            float angle = 0;
            float startAngle = -90;
            int index = 0;
            angles.Clear();
            foreach (var item in Values)
            {
                double rate = item.Value * 100 / sum;
                angle = (float)(rate * unit);
                DrawPie(g, new SolidBrush(Colors[index++]), center.X - Radius, center.Y - Radius, Radius * 2, Radius * 2, startAngle, angle);
                startAngle += angle;
                angles.Add(item.Key, startAngle + 90);
            }
            g.FillEllipse(new SolidBrush(centerColor), center.X - CenterRadius, center.Y - CenterRadius, CenterRadius*2, CenterRadius*2);
        }

        private void DrawChart(Graphics g, string key)
        {
            g.Clear(this.BackColor);
            double sum = 0;
            foreach (var item in Values)
            {
                sum += item.Value;
            }

            double unit = 360d / 100d;
            float angle = 0;
            float startAngle = -90;
            int index = 0;
            angles.Clear();
            foreach (var item in Values)
            {
                double rate = item.Value * 100 / sum;
                angle = (float)(rate * unit);
                if (key == item.Key)
                {
                    g.FillPie(new SolidBrush(Colors[index]), center.X - Radius - diffRadius/2, center.Y - Radius - diffRadius / 2, Radius * 2 + diffRadius, Radius * 2 + diffRadius, startAngle, angle);

                    Point p2 = new Point(center.X - diffRadius / 2 + (int)((Radius + 50) * Math.Sin(Math.PI * 2 * (startAngle + 90 + angle / 2) / 360)), center.Y - diffRadius / 2 - (int)((Radius + 50) * Math.Cos(Math.PI * 2 * (startAngle + 90 + angle / 2) / 360)));
                    Point p1 = new Point(center.X - diffRadius / 2 + (int)((Radius + 30) * Math.Sin(Math.PI * 2 * (startAngle + 90 + angle / 2) / 360)), center.Y - diffRadius / 2 - (int)((Radius + 30) * Math.Cos(Math.PI * 2 * (startAngle + 90 + angle / 2) / 360)));
                    g.DrawLine(new Pen(new SolidBrush(Colors[index])), p1, p2);

                    g.FillEllipse(new SolidBrush(Colors[index]), p1.X - 2, p1.Y - 2, 4, 4);

                    if (p1.X >= center.X - diffRadius / 2)
                    {
                        g.DrawLine(new Pen(new SolidBrush(Colors[index])), p2, new Point(p2.X + 80, p2.Y));
                        g.DrawString(key + $" {Math.Round(rate, 2)}%", new Font("宋体", 9), new SolidBrush(Colors[index]), new Point(p2.X + 5, p2.Y - 15));
                        g.DrawString(item.Value.ToString(), new Font("宋体", 9), new SolidBrush(Colors[index]), new Point(p2.X + 7, p2.Y + 5));
                    }
                    else
                    {
                        g.DrawLine(new Pen(new SolidBrush(Colors[index])), p2, new Point(p2.X - 80, p2.Y));
                        g.DrawString(key + $" {Math.Round(rate, 2)}%", new Font("宋体", 9), new SolidBrush(Colors[index]), new Point(p2.X - 75, p2.Y - 15));
                        g.DrawString(item.Value.ToString(), new Font("宋体", 9), new SolidBrush(Colors[index]), new Point(p2.X - 73, p2.Y + 5));
                    }
                }
                //
                
                DrawPie(g, new SolidBrush(Colors[index++]), center.X - Radius, center.Y - Radius, Radius * 2, Radius * 2, startAngle, angle);
                startAngle += angle;
                angles.Add(item.Key, startAngle + 90);
            }
            g.FillEllipse(new SolidBrush(centerColor), center.X - CenterRadius, center.Y - CenterRadius, CenterRadius*2, CenterRadius*2);
        }

        private void DrawPie(Graphics g, Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            g.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
            g.FillEllipse(new SolidBrush(centerColor), center.X - CenterRadius, center.Y - CenterRadius, CenterRadius*2, CenterRadius*2);
        }

        
        private void CircleChart_MouseMove(object sender, MouseEventArgs e)
        {
            mousePoint = e.Location;
            int distance = GetDistance(center, mousePoint);
            if (IsInCircle(distance, Radius))
            {
                double angle = GetAngle(center, mousePoint);
                double startAngle = 0;
                int index = 0;
                foreach (var ag in angles)
                {
                    if (angle < ag.Value)
                    {
                        key = ag.Key;
                        Draw(key);
                        break;
                    }
                    startAngle = ag.Value;
                    index++;
                }
            }
            else
            {
                Draw(null);
            }
        }

        private int GetDistance(Point p1, Point p2)
        {
            int x = p1.X - p2.X;
            int y = p1.Y - p2.Y;
            return (int)Math.Sqrt(x * x + y * y);
        }

        private bool IsInCircle(int distance, int radius)
        {
            return distance <= radius;
        }

        private double GetAngle(Point p1, Point p2)
        {
            int x = p2.X - p1.X;
            int y = p2.Y - p1.Y;
            int distance = GetDistance(p1, p2);
            double angle = 0;
            if (x >= 0 && y <= 0)
            {
                angle = Math.Asin((double)x / distance) * 180 / Math.PI;
            }
            else if (x >= 0 && y > 0)
            {
                angle = Math.Asin((double)x / distance) * 180 / Math.PI;
                angle = 180 - angle;
            }
            else if (x < 0 && y > 0)
            {
                angle = Math.Asin((double)(0 - x) / distance) * 180 / Math.PI;
                angle = 180 + angle;
            }
            else
            {
                angle = Math.Asin((double)(0 - x) / distance) * 180 / Math.PI;
                angle = 360 - angle;
            }

            return angle;
        }
    }
}

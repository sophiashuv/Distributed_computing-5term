using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace task04_ParalelForms
{
    public partial class Form1 : Form
    {
        private List<Thread> threads;
        private List<bool> isRunning;

        public Form1()
        {
            InitializeComponent();
            threads = new List<Thread>();
            isRunning = new List<bool>
            {
                true,
                true,
                true,
                true
            };
            buttonStart1.Enabled = false;
            buttonStart2.Enabled = false;
            buttonStart3.Enabled = false;
            buttonStart4.Enabled = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            threads.Add(new Thread(Panel1Draw));
            threads.Add(new Thread(Panel2Draw));
            threads.Add(new Thread(Panel3Draw));
            threads.Add(new Thread(Panel4Draw));
            threads.ForEach(p => p.Start());
        }

        public void Panel1Draw()
        {
            Graphics g = panel1.CreateGraphics();
            Bitmap btm = new Bitmap(panel1.Width, panel1.Height);
            Graphics gImg = Graphics.FromImage(btm);
            var alpha = 0.0;
            var a = 70;
            var speed = 20;
            PointF[] rect;


            PointF img = new PointF(0, 0);
            var frameCount = 0;
            SolidBrush color = new SolidBrush(Color.Blue);
            while (true)
            {
                if (frameCount % speed == 0)
                {
                    color = new SolidBrush(Color.FromArgb(frameCount * 3 % 255, frameCount * 5 % 255, frameCount * 7 % 255));
                    rect = new[] {
                    new PointF((float)((-a) * Math.Cos(alpha) - (-a) * Math.Sin(alpha)) + panel4.Width / 2, (float)((-a) * Math.Cos(alpha) + (-a) * Math.Sin(alpha)) + panel4.Height / 2),
                    new PointF((float)((-a) * Math.Cos(alpha) - a * Math.Sin(alpha)) + panel4.Width / 2, (float)(a * Math.Cos(alpha) + (-a) * Math.Sin(alpha)) +  panel4.Height / 2),
                    new PointF((float)(a * Math.Cos(alpha) - a * Math.Sin(alpha)) + panel4.Width / 2, (float)(a * Math.Cos(alpha) + a * Math.Sin(alpha)) + panel4.Height / 2),
                    new PointF((float)(a * Math.Cos(alpha) - (-a) * Math.Sin(alpha)) + panel4.Width / 2, (float)((-a) * Math.Cos(alpha) + a * Math.Sin(alpha)) +  panel4.Height / 2)
                };
                    gImg.FillPolygon(color, rect);
                }

                g.DrawImage(btm, img);
                alpha += 0.01;
                frameCount++;
            }
        }

        private void buttonStart1_Click(object sender, EventArgs e)
        {
            if (isRunning[0] == false)
            {
                threads[0].Resume();
                isRunning[0] = true;
            }
            buttonStop1.Enabled = true;
            buttonStart1.Enabled = false;
        }

        private void buttonStop1_Click(object sender, EventArgs e)
        {
            if (isRunning[0])
            {
                threads[0].Suspend();
                isRunning[0] = false;
            }
            buttonStart1.Enabled = true;
            buttonStop1.Enabled = false;

        }

        public void Panel2Draw()
        {
            Bitmap btm = new Bitmap(panel2.Width, panel2.Height);
            Graphics gImg = Graphics.FromImage(btm);
            Graphics g = panel2.CreateGraphics();
            PointF img = new PointF(0, 0);
            Random rand = new Random();
            Pen pen = new Pen(Color.FromArgb(128, 128, 255), 5F);

            double X, Y, dX, dY;
            int radius = 25;
            X = rand.Next(radius, panel2.Width - radius);
            Y = rand.Next(radius, panel2.Height - radius);
            dX = (rand.Next(1, 5)) * 0.5;
            dY = (rand.Next(1, 5)) * 0.5;

            while (true)
            {
                gImg.Clear(panel2.BackColor);
                gImg.DrawEllipse(pen, (float)X - radius, (float)Y - radius, radius * 2, radius * 2);
                gImg.FillEllipse(new SolidBrush(Color.FromArgb(57, 172, 115)), (float)X - radius, (float)Y - radius, radius * 2, radius * 2);
                IsInRect(ref X, ref Y, ref dX, ref dY, radius, panel2.Width, panel2.Height);
                g.DrawImage(btm, img);
            }
           
        }

        public bool IsInRect(ref double X, ref double Y, ref double dX, ref double dY, int radius, int width, int height)
        {
            if (X <= radius && dX < 0)
            {
                dX = -dX;
                X += dX;
                Y += dY;
                return false;
            }
            else if (X >= width - radius && dX > 0)
            {
                dX = -dX;
                X += dX;
                Y += dY;
                return false;
            }
            else if (Y <= radius && dY < 0)
            {
                dY = -dY;
                X += dX;
                Y += dY;
                return false;
            }
            else if (Y >= height - radius && dY > 0)
            {
                dY = -dY;
                X += dX;
                Y += dY;
                return false;
            }
            else
            {
                X += dX;
                Y += dY;
                return true;
            }
        }

        private void buttonStart2_Click(object sender, EventArgs e)
        {
            if (isRunning[1] == false)
            {
                threads[1].Resume();
                isRunning[1] = true;
            }
            buttonStop2.Enabled = true;
            buttonStart2.Enabled = false;
        }

        private void buttonStop2_Click(object sender, EventArgs e)
        {
            if (isRunning[1] == true)
            {
                threads[1].Suspend();
                isRunning[1] = false;
            }
            buttonStart2.Enabled = true;
            buttonStop2.Enabled = false;
        }

        public void Panel3Draw()
        {
            Graphics g = panel3.CreateGraphics();
            Pen pen = new Pen(Color.Red, 6F);

            float x1 = 0, y1 = 0, x = 0;
            float y2;
            float yEx = 130;
            float eF = 20;

            g.Clear(panel3.BackColor);

            while (true)
            {
                y2 = (float)Math.Sin(x);
                g.DrawLine(pen, x1 * eF, y1 * eF + yEx, x * eF, y2 * eF + yEx);

                x1 = x;
                y1 = y2;
                x += 0.2f;

                if (x * eF >= 350)
                {
                    x = x1 = y1 = 0;
                    g.Clear(panel3.BackColor);
                }
                Thread.Sleep(10);
            }
        }

        private void buttonStart3_Click(object sender, EventArgs e)
        {
            if (isRunning[2] == false)
            {
                threads[2].Resume();
                isRunning[2] = true;
            }
            buttonStop3.Enabled = true;
            buttonStart3.Enabled = false;
        }

        private void buttonStop3_Click(object sender, EventArgs e)
        {
            if (isRunning[2])
            {
                threads[2].Suspend();
                isRunning[2] = false;
            }
            buttonStart3.Enabled = true;
            buttonStop3.Enabled = false;
        }


        public void Panel4Draw()
        {
            Graphics g = panel4.CreateGraphics();
            Bitmap btm = new Bitmap(panel4.Width, panel4.Height);
            Graphics gImg = Graphics.FromImage(btm);
            Rectangle area = new Rectangle(0, 10, 70, 35);
            Pen pen = new Pen(Color.FromArgb(255, 92, 51), 3F);
            PointF img = new PointF(0, 0);

            while (true)
            {
                if (area.X + 5 > panel4.Width)
                {
                    area.X = 0;
                    area.Y += 35;
                    if (area.Y + 35 > panel4.Height)
                    {
                        area.Y = 0;
                    }
                }

                gImg.Clear(panel4.BackColor);
                gImg.DrawRectangle(pen, area);
                gImg.FillRectangle(new SolidBrush(Color.FromArgb(51, 102, 255)), area);
                g.DrawImage(btm, img);

                area.X += 5;

                Thread.Sleep(5);
            }
        }

        private void buttonStart4_Click(object sender, EventArgs e)
        {
            if (isRunning[3] == false)
            {
                threads[3].Resume();
                isRunning[3] = true;
            }
            buttonStop4.Enabled = true;
            buttonStart4.Enabled = false;
        }

        private void buttonStop4_Click(object sender, EventArgs e)
        {
            if (isRunning[3])
            {
                threads[3].Suspend();
                isRunning[3] = false;
            }
            buttonStart4.Enabled = true;
            buttonStop4.Enabled = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            for(int i = 0; i < 4; i++)
            {
                if (!isRunning[i])
                {
                    threads[i].Resume();
                    isRunning[i] = true;
                }
            }
            threads.ForEach(p => p.Abort());
        }
    }
}

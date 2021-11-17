using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace task05_ParalelForms2
 {
    public partial class Form1 : Form
    {
        private int Num_petals => 16;
        private List<Thread> threads;
        private List<bool> status;

        public Bitmap Btm { set; get; }
        public Graphics GImg { set; get; }
        public Graphics g => panel1.CreateGraphics();

        private float alpha;
        float y0;
        float x0;

        public Form1()
        {
            InitializeComponent();
            threads = new List<Thread>();
            status = new List<bool> { true, false, false, false, false , false };

            Btm = new Bitmap(Width, Height);
            GImg = Graphics.FromImage(Btm);

            alpha =   (float)Math.PI  / Num_petals;
            y0 = panel1.Height / 2;
            x0 = CalculateValue(70, 1, 0, 2, 0).X;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            threads.Add(new Thread(() => PanelDraw(0, 1, ref alpha, ref x0, ref y0, Color.FromArgb(255, 179, 217))));
            threads.Add(new Thread(() => PanelDraw(1, 2, ref alpha, ref x0, ref y0, Color.FromArgb(255, 77, 166))));
            threads.Add(new Thread(() => PanelDraw(2, 3, ref alpha, ref x0, ref y0, Color.FromArgb(255, 0, 128))));
            threads.Add(new Thread(() => PanelDraw(3, 4, ref alpha, ref x0, ref y0, Color.FromArgb(255, 102, 204))));
            threads.Add(new Thread(() => PanelDraw(4, 5, ref alpha, ref x0, ref y0, Color.FromArgb(128, 0, 85))));
            threads.Add(new Thread(() => PanelDraw(5, 0, ref alpha, ref x0, ref y0, Color.FromArgb(179, 0, 134))));


            threads.ForEach(p => p.Start());
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
            threads.ForEach(p => p.Abort());
        }

        public void PanelDraw(int current, int next, ref float alpha, ref float x0, ref float y0, Color color)
        {

            while (true)
            {
                if (status[current])
                {
                    
                    if (alpha > 2 * (float)Math.PI)
                    {
                        g.Clear(panel1.BackColor);
                        alpha =  (float)Math.PI / Num_petals ;
                    }
                    
                    Pen pen = new Pen(Color.FromArgb(30, 132, 73));
                    pen.Width = 6.0F;


                    pen.Color = color;
                    pen.Width = 4.0F;
                    float end = alpha +  2 * (float)Math.PI / Num_petals;
                    Console.WriteLine($"end {alpha}");
                    for (float teta = alpha; teta < end; teta += (float)Math.PI / 180)
                    {
                        g.FillEllipse(new SolidBrush(Color.FromArgb(247, 220, 111)), Width / 2 - 10, Height / 2 - 10, 20, 20);
                        var p = CalculateValue(120, Num_petals/2, 0, 2, teta);
                        g.DrawLine(pen, x0, y0, p.X, p.Y);
                        x0 = p.X;
                        y0 = p.Y;
                        g.FillEllipse(new SolidBrush(Color.FromArgb(247, 220, 111)), Width / 2 - 10, Height / 2 - 10, 20, 20);
                        Thread.Sleep(20);
                    }

                    //Thread.Sleep(200);
                    
                    alpha = end;
                    status[current] = false;
                    status[next] = true;
                    
                }
            }

        }

        public PointF CalculateValue(float a, float b, float c, float d, float theta)
        {
            double r = a * Math.Cos(b * theta + c) + d;
            float x = (float)(r * Math.Cos(theta)) + Width / 2;
            float y = (float)(r * Math.Sin(theta)) + Height / 2;
            return new PointF(x, y);
        }

    }

}

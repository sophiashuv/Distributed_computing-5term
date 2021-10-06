using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ParalelForms
{
   
    class Panel3: BasePanel
    {
        public Panel3(Panel panel) : base(panel) { }

        public override void PanelDraw()
        {

            while (true)
            {
                Pen pen = new Pen(Color.FromArgb(30, 132, 73));
                pen.Width = 6.0F;
                float begin = Height / 2;
                for (float i = 0; i < Height - 1; i += 2)
                {
                    g.DrawLine(pen, Width / 2, begin, Width / 2, begin + i);
                    begin += i;
                    Thread.Sleep(5);
                }

                float y0 = Height / 2;
                float x0 = CalculateValue(10, 8, 0, 40, 0).X;
                DrawFlower(pen, x0, y0, 10, 8, 0, 40, Color.FromArgb(231, 76, 60), 5.0F);

                x0 = CalculateValue(35, 1, 0, 2, 0).X;
                DrawFlower(pen, x0, y0, 35, 4, 0, 2, Color.FromArgb(229, 152, 102), 4.0F);

                x0 = CalculateValue(1, 8, 0, 22, 0).X;
                DrawFlower(pen, x0, y0, 1, 8, 0, 22, Color.FromArgb(165, 105, 189), 3.0F);

                g.FillEllipse(new SolidBrush(Color.FromArgb(247, 220, 111)), Width / 2 - 6, Height / 2 - 6, 12, 12);
                Thread.Sleep(1000);
                g.Clear(Panel.BackColor);
            }

            
        }
        public PointF CalculateValue(float a, float b, float c, float d, float theta)
        {
            double r = a * Math.Cos(b * theta + c) + d;
            float x = (float)(r * Math.Cos(theta)) + Width / 2;
            float y = (float)(r * Math.Sin(theta)) + Height / 2;
            return new PointF(x, y);
        }

        public void DrawFlower(Pen pen, float x0, float y0, float a, float b, float c, float d, Color color, float width)
        {
            pen.Color = color;
            pen.Width = width;
            float end = 180 * 2 * (float)Math.PI / 180;
            for (float theta = 0; theta < end; theta += (float)Math.PI / 180)
            {
                var p = CalculateValue(a, b, c, d, theta);
                g.DrawLine(pen, x0, y0, p.X, p.Y);
                x0 = p.X;
                y0 = p.Y;
                Thread.Sleep(5);
            }
        }
    }
}

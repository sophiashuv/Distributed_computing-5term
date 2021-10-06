using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ParalelForms
{
    class Panel4: BasePanel
    {
        public Panel4(Panel panel) : base(panel) { }

        public override void PanelDraw()
        {
            Pen pen = new Pen(Color.Black);
            List<PointF> points = new List<PointF>();
            float radius = 20;
            float step = 10;

            float X = 0;
            float Y = Height / 2 - radius;

            float X0 = radius / 2;
            float Y0 = Height / 2 - radius;

            float delta_x;
            float delta_y;

            
            float alpha = (float) (step * 180 / Math.PI / radius);
            float delta = (float) Math.Sqrt(2 * Math.Pow(radius, 2) - 2 * Math.Pow(radius, 2) * Math.Cos(alpha * Math.PI / 180));

            int i = 0;
            while (true)
            {
                if (X >= Width - 2 * radius)
                {
                    X = 0;
                    X0 = radius / 2;
                    Y0 = Height / 2 - radius;
                    points = new List<PointF>();
                    i = 0;
                }

                delta_x = (float) (delta * Math.Sin((90 - i * alpha - alpha / 2) * Math.PI / 180));
                delta_y = (float) (delta * Math.Cos((90 - i * alpha - alpha / 2) * Math.PI / 180));

                GImg.FillEllipse(new SolidBrush(Color.FromArgb(22, 160, 133)), X, Y, radius * 2, radius * 2);

                X0 += delta_x + step;
                Y0 += delta_y;
                points.Add(new PointF(X0, Y0));

                if (points.Count > 2)
                {
                    for (int k = 0; k < points.Count - 1; k++)
                    {
                        GImg.DrawLine(pen, points[k], points[k + 1]);
                    }
                }

                GImg.FillEllipse(new SolidBrush(Color.Black), X0 - 3, Y0 - 3, 7, 7);
                g.DrawImage(Btm, img);
                GImg.Clear(Panel.BackColor);

                Thread.Sleep(100);

                X += step;
                i++;
            }
        }
    }
}

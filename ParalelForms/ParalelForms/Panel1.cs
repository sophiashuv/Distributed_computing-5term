using System;
using System.Drawing;
using System.Windows.Forms;

namespace ParalelForms
{
    class Panel1 : BasePanel
    {

        public Panel1(Panel panel): base(panel) { }
        public override void PanelDraw()
        {
            Random rand = new Random();

            double X, Y, dX, dY;
            int radius = 20;

            X = rand.Next(radius, Width - radius);
            Y = rand.Next(radius, Height - radius);

            dX = (rand.Next(1, 3)) * 0.5;
            dY = (rand.Next(1, 3)) * 0.5;


            while (true)
            {
                GImg.Clear(Panel.BackColor);
                SolidBrush fillColor = new SolidBrush(Color.FromArgb(244, 208, 63));
                GImg.FillEllipse(fillColor, (float)X - radius, (float)Y - radius, radius * 2, radius * 2);

                HitInWall(ref X, ref Y, ref dX, ref dY, radius, Width, Height);
                g.DrawImage(Btm, img);
            }
        }

        public void HitInWall(ref double X, ref double Y, ref double dX, ref double dY, int radius, int width, int height)
        {

            if (X <= radius && dX < 0)
            {
                dX = -dX;
                X += dX;
                Y += dY;
            }

            else if (X >= width - radius && dX > 0)
            {
                dX = -dX;
                X += dX;
                Y += dY;
            }

            else if (Y <= radius && dY < 0)
            {
                dY = -dY;
                X += dX;
                Y += dY;
            }

            else if (Y >= height - radius && dY > 0)
            {
                dY = -dY;
                X += dX;
                Y += dY;
            }
            else
            {
                X += dX;
                Y += dY;
            }
        }
    }
}

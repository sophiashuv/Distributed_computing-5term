using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ParalelForms
{
    class Panel2: BasePanel
    {
        public Panel2(Panel panel) : base(panel) { }
        public override void PanelDraw()
        {
            var sqrtWidth = 60.0;
            var sqrtHeight = 40.0;
            var alpha = 10;

            var sqrtCenterX = Width / 2.0;
            var sqrtCenterY = Height / 4.0;


            double delta_x;
            double delta_y;
            
            Rectangle area = new Rectangle((int)(sqrtCenterX - sqrtWidth / 2), (int)(sqrtCenterY - sqrtHeight / 2), (int)sqrtWidth, (int)sqrtHeight);

            var delta = Math.Sqrt(2 * Math.Pow(Height / 4, 2) - 2 * Math.Pow(Height / 4, 2) * Math.Cos(alpha * Math.PI / 180));

            int i = 0;
            while (true)
            {
                while (area.Width <= 60 && area.Width > 30)
                {
                    delta_x = delta * Math.Sin((90 - i * alpha - alpha / 2) * Math.PI / 180);
                    delta_y = delta * Math.Cos((90 - i * alpha - alpha / 2) * Math.PI / 180);

                    GImg.Clear(Panel.BackColor);
                    GImg.FillRectangle(new SolidBrush(Color.FromArgb(46, 134, 193)), area);
                    g.DrawImage(Btm, img);

                    sqrtCenterX += delta_x;
                    sqrtCenterY += delta_y;

                    area.Width -= 2;
                    area.Height -= 2;

                    area.X = (int)(sqrtCenterX - sqrtWidth / 2.0) + 1;
                    area.Y = (int)(sqrtCenterY - sqrtHeight / 2.0) + 1;

                    i++;
                    Thread.Sleep(20);
                }


                while (area.Width < 60 && area.Width >= 30)
                {
                    delta_x = delta * Math.Sin((90 - i * alpha - alpha / 2) * Math.PI / 180);
                    delta_y = delta * Math.Cos((90 - i * alpha - alpha / 2) * Math.PI / 180);

                    GImg.Clear(Panel.BackColor);

                    GImg.FillRectangle(new SolidBrush(Color.FromArgb(46, 134, 193)), area);
                    g.DrawImage(Btm, img);

                    sqrtCenterX += delta_x;
                    sqrtCenterY += delta_y;

                    area.Width += 2;
                    area.Height += 2;

                    area.X = (int)(sqrtCenterX - sqrtWidth / 2.0) - 1;
                    area.Y = (int)(sqrtCenterY - sqrtHeight / 2.0) - 1;
                    i++;
                    Thread.Sleep(20);
                }
            }
        }
    }
}

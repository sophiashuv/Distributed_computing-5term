using System.Drawing;
using System.Windows.Forms;

namespace ParalelForms
{
    abstract class BasePanel
    {
        public Panel Panel { set; get; }
        public Bitmap Btm { set; get; }
        public Graphics GImg { set; get; }
        public int Width => Panel.Width;
        public int Height => Panel.Height;
        public Graphics g => Panel.CreateGraphics();
        public PointF img => new PointF(0, 0);
        public BasePanel(Panel panel)
        {
            Panel = panel;
            Btm = new Bitmap(Width, Height);
            GImg = Graphics.FromImage(Btm);
        }

        public abstract void PanelDraw();
    }

}


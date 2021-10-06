using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace ParalelForms
{
    public partial class Form1 : Form
    {
        private List<Thread> threads;
        private List<bool> isRunning;
        private List<Button> buttonsStart;
        private List<Button> buttonsStop;
        private List<BasePanel> panels => new List<BasePanel> { new Panel1(panel1), new Panel2(panel2), new Panel3(panel3), new Panel4(panel4) };


        public Form1()
        {
            InitializeComponent();
            threads = new List<Thread>();
            isRunning = new List<bool>{true, true, true, true};
            buttonsStart = new List<Button> { buttonStart1, buttonStart2, buttonStart3, buttonStart4 };
            buttonsStop = new List<Button> { buttonStop1, buttonStop2, buttonStop3, buttonStop4 };
            foreach (var buttom in buttonsStart)
            {
                buttom.Enabled = false;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            threads.Add(new Thread(panels[0].PanelDraw));
            threads.Add(new Thread(panels[1].PanelDraw));
            threads.Add(new Thread(panels[2].PanelDraw));
            threads.Add(new Thread(panels[3].PanelDraw));
            threads.ForEach(p => p.Start());
        }

        private void buttonStart1_Click(object sender, EventArgs e) => buttonStart_Click(sender, e, 0);

        private void buttonStop1_Click(object sender, EventArgs e) => buttonStop_Click(sender, e, 0);

        private void buttonStart2_Click(object sender, EventArgs e) => buttonStart_Click(sender, e, 1);

        private void buttonStop2_Click(object sender, EventArgs e) => buttonStop_Click(sender, e, 1);

        private void buttonStart3_Click(object sender, EventArgs e) => buttonStart_Click(sender, e, 2);

        private void buttonStop3_Click(object sender, EventArgs e) => buttonStop_Click(sender, e, 2);

        private void buttonStart4_Click(object sender, EventArgs e) => buttonStart_Click(sender, e, 3);

        private void buttonStop4_Click(object sender, EventArgs e) => buttonStop_Click(sender, e, 3);

        private void buttonStart_Click(object sender, EventArgs e, int num)
        {
            if (!isRunning[num])
            {
                threads[num].Resume();
                isRunning[num] = true;
            }

            buttonsStop[num].Enabled = true;
            buttonsStart[num].Enabled = false;
        }

        private void buttonStop_Click(object sender, EventArgs e, int num)
        {
            if (isRunning[num])
            {
                threads[num].Suspend();
                isRunning[num] = false;
            }
            buttonsStop[num].Enabled = false;
            buttonsStart[num].Enabled = true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
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

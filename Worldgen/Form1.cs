using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Worldgen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(512, 512);
            richTextBox1.Text = "Starting Worldgen\n";

            World world = new World(512, 512) { width = 512, height = 512, topology = Topology.EUCLIDEAN };
            world.Generate(LogProgress);

            LogProgress("Generation done\nNow creating image\n");
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    bmp.SetPixel(x, y, world.GetColorAt(x, y));
                }
            }

            LogProgress("Image created\nNow printing output\n");
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
        }

        public void LogProgress(string input)
        {
            richTextBox1.Text += input;
            richTextBox1.Refresh();
        }
    }
}

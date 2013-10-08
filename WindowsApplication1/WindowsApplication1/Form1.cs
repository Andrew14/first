using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y);
        }
    }

    public class Cross
    {
        int x, y;

        public Cross(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
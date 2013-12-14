using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        List<Shape> Shapes = new List<Shape>();
        bool click = true;
        Point p1, p2;

        string file_name;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y);
                Shapes.Add(new Cross(e.Location));
            }
            else if (radioButton2.Checked)
            {
                if (click)
                {
                    this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y) + " первый";
                    p1 = e.Location;
                }
                else
                {
                    this.Text = Convert.ToString(e.X) + " " + Convert.ToString(e.Y) + " второй";
                    p2 = e.Location;
                    Shapes.Add(new Line(p1, p2));
                }
                click = !click;
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape count in this.Shapes)
            {
                count.ReDraw(e.Graphics);
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            click = true;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//load
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file_name = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(file_name);

                Shapes.Clear();

                string line;
                line = sr.ReadLine();

                while (line != null)
                {
                    if (line == "cross") Shapes.Add(new Cross(sr));
                    if (line == "line") Shapes.Add(new Line(sr));
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)//save
        {
            if (file_name == null)
            {
                button3_Click(sender, e);
            }
            else
            {
                try
                {
                    SaveFile(file_name);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)//save as
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    file_name = saveFileDialog1.FileName;
                    SaveFile(file_name);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
        }

        private void SaveFile(string file)
        {
            StreamWriter sw = new StreamWriter(file_name);
            foreach (Shape count in this.Shapes)
            {
                count.SaveTo(sw);
            }
            sw.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Shapes.Clear();
            Invalidate();
        }
    }
    public abstract class Shape
    {
        public abstract void ReDraw(Graphics g);
        public abstract void SaveTo(StreamWriter sw);
    }

    public class Cross : Shape
    {
        Point a;

        Pen p = new Pen(Color.Black);

        public Cross(Point a)
        {
            this.a = a;
        }

        public Cross(StreamReader sr)
        {
            string[] data = sr.ReadLine().Trim().Split(' ');
            a = new Point(int.Parse(data[0]), int.Parse(data[1]));
        }

        public override void ReDraw(Graphics g)
        {
            g.DrawLine(p, a.X - 2, a.Y - 2, a.X + 2, a.Y + 2);
            g.DrawLine(p, a.X - 2, a.Y + 2, a.X + 2, a.Y - 2);
        }

        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("cross");
            sw.WriteLine(Convert.ToString(a.X) + " " + Convert.ToString(a.Y));
        }


    }
    public class Line : Shape
    {
        Point s, f;

        Pen w = new Pen(Color.Black);

        public Line(Point s, Point f)
        {
            this.s = s;
            this.f = f;
        }

        public Line(StreamReader sr)
        {
            string[] data = sr.ReadLine().Trim().Split(' ');
            s = new Point(int.Parse(data[0]), int.Parse(data[1]));
            data = sr.ReadLine().Trim().Split(' ');
            f = new Point(int.Parse(data[0]), int.Parse(data[1]));
        }

        public override void ReDraw(Graphics g)
        {
            g.DrawLine(w, s, f);
        }

        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("line");
            sw.WriteLine(Convert.ToString(s.X) + " " + Convert.ToString(s.Y));
            sw.WriteLine(Convert.ToString(f.X) + " " + Convert.ToString(f.Y));
        }


    }
    

}

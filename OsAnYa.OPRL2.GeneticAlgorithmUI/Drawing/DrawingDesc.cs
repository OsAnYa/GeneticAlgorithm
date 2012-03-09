using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace OsAnYa.OPRL2.GeneticAlgorithmUI
{
    public delegate void DescClick(float x, float y);


    public class DrawingDesc
    {
        public event DescClick OnClick;

        bool md;
        int px;
        int py;

        public float Scale { get; set; }
        public int X0 { get; set; }
        public int Y0 { get; set; }
        public bool OneStroke { get; set; }
        public bool Asix { get; set; }

        System.Windows.Forms.PictureBox pictureBox;

        List<LineCord> Lines;
        List<TextCord> Texts;
        List<CircleCord> Circles;

        public void AddLine(float x1, float y1, float x2, float y2, System.Drawing.Pen pen)
        {
            Lines.Add(new LineCord(x1, y1, x2, y2, pen));
        }

        public void AddCircle(float x, float y, float rpx, Pen pen, bool fill = false)
        {
            Circles.Add(new CircleCord(x, y, rpx, pen, fill));
        }

        public void AddText(string st, float x, float y, System.Drawing.Font f, System.Drawing.Brush b)
        {
            Texts.Add(new TextCord(st, x, y, f, b));
        }

        public void AddText(string text, double x, double y)
        {
            AddText(text, (float)x, (float)y, SystemFonts.DefaultFont, Brushes.Black);
        }

        public void Update()
        {
            pictureBox.Invalidate();
        }

        public void Clear()
        {
            Lines.Clear();
            Texts.Clear();
            Circles.Clear();
            pictureBox.Invalidate();
        }



        public DrawingDesc(System.Windows.Forms.PictureBox picBox)
        {
            md = false;
            pictureBox = picBox;
            Scale = 1;
            X0 = pictureBox.Size.Width / 2;
            Y0 = pictureBox.Size.Height / 2;
            Lines = new List<LineCord>();
            Texts = new List<TextCord>();
            Circles = new List<CircleCord>();
            pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(pictureBox_Paint);
            pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(pictureBox_MouseDown);
            pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(pictureBox_MouseUp);
            pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(pictureBox_MouseMove);
            pictureBox.MouseLeave += new EventHandler(pictureBox_MouseLeave);
            pictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(pictureBox_MouseWheel);
            pictureBox.MouseEnter += new EventHandler(pictureBox_MouseEnter);

        }

        void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            pictureBox.Focus();
        }

        void pictureBox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (e.Delta > 0)
            {
                Scale *= 1.1f;
            }
            else
            {
                Scale /= 1.1f;
            }

            pictureBox.Invalidate();
        }

        void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            md = false;
        }

        void pictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (md)
            {
                X0 += e.X - px;
                Y0 += e.Y - py;
                px = e.X;
                py = e.Y;
                pictureBox.Invalidate();
            }
        }

        void pictureBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            md = false;

        }

        void pictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            md = true;
            px = e.X;
            py = e.Y;
            if (OnClick != null)
                OnClick((px - X0) / Scale, (Y0 - py) / Scale);
        }

        public float MaxX { get { return (pictureBox.Width - X0) / Scale; } }
        public float MinX { get { return (-X0) / Scale; } }
        public float MaxY { get { return (Y0) / Scale; } }
        public float MinY { get { return -(pictureBox.Height - Y0) / Scale; } }

        void pictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.Clear(System.Drawing.Color.White);


            if (Asix)
            {
                e.Graphics.DrawLine(System.Drawing.Pens.Black, 0, Y0, pictureBox.Width, Y0);
                e.Graphics.DrawLine(System.Drawing.Pens.Black, X0, 0, X0, pictureBox.Height);
            }

            if (OneStroke)
            {
                e.Graphics.DrawLine(Pens.Black, GetX(1), GetY(0.1f), GetX(1), GetY(-0.1f));
                e.Graphics.DrawString("1", SystemFonts.DefaultFont, Brushes.Black, GetX(1), GetY(0));
            }

            foreach (LineCord ln in Lines)
            {
                e.Graphics.DrawLine(ln.pen, GetX(ln.X1), GetY(ln.Y1), GetX(ln.X2), GetY(ln.Y2));

            }
            foreach (TextCord tc in Texts)
            {
                e.Graphics.DrawString(tc.str, tc.font, tc.brush, GetX(tc.X), GetY(tc.Y));
            }

            foreach (var item in Circles)
            {
                SolidBrush b = new SolidBrush(item.pen.Color);
                if (item.Fill)
                    e.Graphics.FillEllipse(b, GetX(item.X) - item.Rpx, GetY(item.Y) - item.Rpx, item.Rpx * 2, item.Rpx * 2);
                else
                    e.Graphics.DrawArc(item.pen, GetX(item.X) - item.Rpx, GetY(item.Y) - item.Rpx, item.Rpx * 2, item.Rpx * 2, 0, 360);
            }

        }


        float GetX(float x)
        {
            return X0 + x * Scale;
        }

        float GetY(float y)
        {
            return Y0 - y * Scale;
        }


    }
}

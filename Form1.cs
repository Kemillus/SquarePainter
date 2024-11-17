using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SquarePainter
{
    public partial class Form1 : Form
    {
        Pen pen = new Pen(Brushes.Black, 2);
        SolidBrush brush = new SolidBrush(Color.Black);

        public Form1()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(Painter);
        }

        private void Painter(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrowSquares(g, 1, 1, 100);
        }

        private void DrowSquares(Graphics g, int x, int y, int size)
        {
            int posY = y;
            bool lines = true;

            for (int i = 0; i < 4; i++)
            {
                DrowSquare(g, x, posY, size, lines);
                posY += size;
                lines = !lines;
            }
        }

        private void DrowSquare(Graphics g, int x, int y, int size, bool lines = true)
        {
            int posX = x;
            bool line = lines;

            for (int i = 0; i < 4; i++)
            {
                g.DrawRectangle(pen, posX, y, size, size);
                DrawRotateSquade(g, posX, y,size);
                if (line)
                {
                    DrowLines(g, posX, y, size);
                }
                line = !line;
                posX += size;
            }
        }

        private void DrowLines(Graphics g, int x, int y, int size)
        {
            int interval = 10;
            Point p1 = new Point(x + interval, y);
            Point p2 = new Point(x + interval, y + size);

            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(pen, p1, p2);
                p1.X += interval;
                p2.X += interval;
            }
        }

        private void DrawRotateSquade(Graphics g, int x, int y, int size)
        {
            Matrix matrix = new Matrix();
            matrix.Rotate(45);
            int drift = size / 2;
            g.Transform = matrix;
            g.DrawRectangle(pen, x + size + drift, y, drift, drift);
            matrix.Reset();
            g.Transform = matrix;
        }
    }
}

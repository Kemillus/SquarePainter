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
        private Pen pen = new Pen(Brushes.Black, 2);
        private SolidBrush brush = new SolidBrush(Color.Black);

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(410, 440);
            Paint += new PaintEventHandler(Painter);
        }

        private void Painter(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawSquares(g, 1, 1, 100);
        }

        private void DrawSquares(Graphics g, int x, int y, int size)
        {
            int posY = y;
            bool lines = true;

            for (int i = 0; i < 4; i++) 
            {
                DrawSquare(g, x, posY, size, lines);
                posY += size;
                lines = !lines;
            }

            DrawRotateSquaresAroundGrid(g, x, y, size);
        }

        private void DrawSquare(Graphics g, int x, int y, int size, bool lines = true)
        {
            int posX = x;
            bool line = lines;

            for (int i = 0; i < 4; i++)
            {
                g.DrawRectangle(pen, posX, y, size, size);

                if (line)
                {
                    DrawLines(g, posX, y, size);
                }

                line = !line;
                posX += size;
            }
        }

        private void DrawLines(Graphics g, int x, int y, int size)
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

        private void DrawRotateSquaresAroundGrid(Graphics g, int x, int y, int size)
        {
            int posY = y;
            int scale = size / 2;
            for (int i = 0; i < 5; i++)
            {
                int posX = x;
                for (int j = 0; j < 5; j++)
                {
                    DrawRotateSquare(g, posX-scale/2, posY-scale/2, scale);
                    posX += size;
                }
                posY += size;
            }
        }


        private void DrawRotateSquare(Graphics g, int x, int y, int size)
        {
            Matrix matrix = new Matrix();
            matrix.RotateAt(45, new PointF(x + size / 2, y + size / 2));

            g.Transform = matrix;
            g.FillRectangle(Brushes.White, x, y, size, size);
            g.FillRectangle(Brushes.Black, x + size / 4, y + size / 4, size / 2, size / 2);
            g.DrawRectangle(pen, x, y, size, size);

            matrix.Reset();
            g.Transform = matrix;
        }
    }




}

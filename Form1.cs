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

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(410, 440);
            Paint += new PaintEventHandler(Painter);
        }

        private void Painter(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int startX = 1;
            int startY = 1;
            int size = 100;

            DrawGridOfSquares(g, startX, startY, size, 4, 4);

            DrawGridOfRotatedSquares(g, startX - size / 4, startY - size / 4, size, 5, 5);
        }

        private void DrawGridOfSquares(Graphics g, int startX, int startY, int size, int rows, int cols)
        {
            int posY = startY;
            bool drawLines = true;

            for (int row = 0; row < rows; row++)
            {
                DrawRowOfSquares(g, startX, posY, size, cols, drawLines);
                posY += size;
                drawLines = !drawLines;
            }
        }

        private void DrawRowOfSquares(Graphics g, int startX, int startY, int size, int numSquares, bool drawLines)
        {
            int posX = startX;
            bool line = drawLines;

            for (int i = 0; i < numSquares; i++)
            {
                g.DrawRectangle(pen, posX, startY, size, size);

                if (line)
                {
                    DrawLinesInsideSquare(g, posX, startY, size);
                }

                DrawCenteredRotatedSquare(g, posX + size / 3, startY + size / 3, size / 3, line);

                line = !line;
                posX += size;
            }
        }

        private void DrawLinesInsideSquare(Graphics g, int x, int y, int size)
        {
            int interval = 10;
            for (int i = 0; i < size / interval; i++)
            {
                Point p1 = new Point(x + i * interval, y);
                Point p2 = new Point(x + i * interval, y + size);
                g.DrawLine(pen, p1, p2);
            }
        }

        private void DrawGridOfRotatedSquares(Graphics g, int startX, int startY, int size, int rows, int cols)
        {
            int posY = startY;

            for (int row = 0; row < rows; row++)
            {
                int posX = startX;
                for (int col = 0; col < cols; col++)
                {
                    DrawRotatedSquare(g, posX, posY, size / 2);
                    posX += size;
                }
                posY += size;
            }
        }

        private void DrawCenteredRotatedSquare(Graphics g, int x, int y, int size, bool isTwoSquares)
        {
            Matrix matrix = new Matrix();
            matrix.RotateAt(45, new PointF(x + size / 2, y + size / 2));

            g.Transform = matrix;
            g.FillRectangle(Brushes.White, x, y, size, size);
            g.DrawRectangle(pen, x, y, size, size);

            if (isTwoSquares)
            {
                g.FillRectangle(Brushes.Black, x + size / 4, y + size / 4, size / 2, size / 2);
            }

            matrix.Reset();
            g.Transform = matrix;
        }

        private void DrawRotatedSquare(Graphics g, int x, int y, int size)
        {
            Matrix matrix = new Matrix();
            matrix.RotateAt(45, new PointF(x + size / 2, y + size / 2));

            g.Transform = matrix;
            g.FillRectangle(Brushes.White, x, y, size, size);
            g.DrawRectangle(pen, x, y, size, size);

            g.FillRectangle(Brushes.Black, x + size / 4, y + size / 4, size / 2, size / 2);

            matrix.Reset();
            g.Transform = matrix;
        }
    }

}

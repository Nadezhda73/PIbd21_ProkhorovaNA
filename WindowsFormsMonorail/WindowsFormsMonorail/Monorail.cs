using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsMonorail
{
    class Monorail
    {
        private float _startPosX;
        private float _startPosY;
        private int _pictureWidth;
        private int _pictureHeight;
        private const int monorailWidth = 100;
        private const int monorailHeight = 60;
        public int MaxSpeed { private set; get; }
        public float Weight { private set; get; }
        public Color MainColor { private set; get; }
        public Color DopColor { private set; get; }
        public bool Window { private set; get; }
        public bool SideStrip { private set; get; }
        public Monorail(int maxSpeed, float weight, Color mainColor, Color dopColor,
       bool window, bool sideStripe)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;
            Window = window;
            SideStrip = sideStripe;
        }
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }
        public void MoveTransport(Direction direction)
        {
            float step = MaxSpeed * 200 / Weight;
            switch (direction)
            {
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - monorailWidth)
                    {
                        _startPosX += step;
                    }
                    break;
                case Direction.Left:
                    if (_startPosX - step > 0)
                    {
                        _startPosX -= step;
                    }
                    break;
                case Direction.Up:
                    if (_startPosY - step > 0)
                    {
                        _startPosY -= step;
                    }
                    break;
                case Direction.Down:
                    if (_startPosY + step < _pictureHeight - monorailHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }
        public void DrawMonorail(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            g.DrawEllipse(pen, _startPosX, _startPosY, 20, 20);
            g.DrawEllipse(pen, _startPosX, _startPosY + 30, 20, 20);
            g.DrawRectangle(pen, _startPosX, _startPosY + 10, 10, 30);
            g.DrawRectangle(pen, _startPosX + 10, _startPosY - 1, 80, 52);
            Brush monorail = new SolidBrush(DopColor);
            g.FillEllipse(monorail, _startPosX, _startPosY, 20, 20);
            g.FillEllipse(monorail, _startPosX, _startPosY + 30, 20, 20);
            g.FillRectangle(monorail, _startPosX, _startPosY + 10, 10, 30);
            g.FillRectangle(monorail, _startPosX + 10, _startPosY - 1, 80, 52);
            if (Window)
            {
                g.DrawRectangle(pen, _startPosX, _startPosY + 10, 90, 20);
                Brush window = new SolidBrush(MainColor);
                g.FillRectangle(window, _startPosX, _startPosY + 10, 90, 20);
            }
            if (SideStrip)
            {
                g.DrawRectangle(pen, _startPosX, _startPosY + 35, 90, 10);
                Brush strip = new SolidBrush(MainColor);
                g.FillRectangle(strip, _startPosX, _startPosY + 35, 90, 10);
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsMonorail
{
    public class Locomotive : Vehicle
    {
        protected const int monorailWidth = 100;
        protected const int monorailHeight = 60;

        public Locomotive(int maxSpeed, float weight, Color mainColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
        }

        public override void MoveTransport(Direction direction)
        {
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                // вправо
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - monorailWidth)
                    {
                        _startPosX += step;
                    }
                    break;
                //влево
                case Direction.Left:
                    if (_startPosX - step > 0)
                    {
                        _startPosX -= step;
                    }
                    break;
                //вверх
                case Direction.Up:
                    if (_startPosY - step > 0)
                    {
                        _startPosY -= step;
                    }
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + step < _pictureHeight - monorailHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }

        public override void DrawMonorail(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            g.DrawEllipse(pen, _startPosX, _startPosY, 20, 20);
            g.DrawEllipse(pen, _startPosX, _startPosY + 30, 20, 20);
            g.DrawRectangle(pen, _startPosX, _startPosY + 10, 10, 30);
            g.DrawRectangle(pen, _startPosX + 10, _startPosY - 1, 80, 52);

            Brush monorail = new SolidBrush(Color.Gray);
            g.FillEllipse(monorail, _startPosX, _startPosY, 20, 20);
            g.FillEllipse(monorail, _startPosX, _startPosY + 30, 20, 20);
            g.FillRectangle(monorail, _startPosX, _startPosY + 10, 10, 30);
            g.FillRectangle(monorail, _startPosX + 10, _startPosY - 1, 80, 52);

            Brush window = new SolidBrush(Color.Blue);
            g.FillRectangle(window, _startPosX, _startPosY + 10, 20, 20);
        }
    }
}
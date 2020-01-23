using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsMonorail
{
    public class Locomotive : Vehicle, IComparable<Locomotive>, IEquatable<Locomotive>
    {
        protected const int monorailWidth = 100;
        protected const int monorailHeight = 60;

        public Locomotive(int maxSpeed, float weight, Color mainColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
        }

        public Locomotive(string info)
        {
            string[] strs = info.Split(';');
            if (strs.Length == 3)
            {
                MaxSpeed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                MainColor = Color.FromName(strs[2]);
            }
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

            Brush monorail = new SolidBrush(MainColor);
            g.FillEllipse(monorail, _startPosX, _startPosY, 20, 20);
            g.FillEllipse(monorail, _startPosX, _startPosY + 30, 20, 20);
            g.FillRectangle(monorail, _startPosX, _startPosY + 10, 10, 30);
            g.FillRectangle(monorail, _startPosX + 10, _startPosY - 1, 80, 52); ;

            Brush window = new SolidBrush(Color.Black);
            g.FillRectangle(window, _startPosX, _startPosY + 10, 20, 20);
        }

        public override string ToString()
        {
            return MaxSpeed + ";" + Weight + ";" + MainColor.Name;
        }

        public int CompareTo(Locomotive other)
        {
            if (other == null)
            {
                return 1;
            }
            if (MaxSpeed != other.MaxSpeed)
            {
                return MaxSpeed.CompareTo(other.MaxSpeed);
            }
            if (Weight != other.Weight)
            {
                return Weight.CompareTo(other.Weight);
            }
            if (MainColor != other.MainColor)
            {
                MainColor.Name.CompareTo(other.MainColor.Name);
            }
            return 0;
        }

        public bool Equals(Locomotive other)
        {
            if (other == null)
            {
                return false;
            }
            if (GetType().Name != other.GetType().Name)
            {
                return false;
            }
            if (MaxSpeed != other.MaxSpeed)
            {
                return false;
            }
            if (Weight != other.Weight)
            {
                return false;
            }
            if (MainColor != other.MainColor)
            {
                return false;
            }
            return true;
        }


        public override bool Equals(Object obj)
        {
            if (obj == null) { return false; }

            if (!(obj is Locomotive boatObj))
            {
                return false;
            }
            else
            {
                return Equals(boatObj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
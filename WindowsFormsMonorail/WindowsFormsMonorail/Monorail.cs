using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsMonorail
{
    public class Monorail : Locomotive, IComparable<Monorail>, IEquatable<Monorail>
    {

        public Color DopColor { private set; get; }

        public bool Window { private set; get; }

        public bool SideStrip { private set; get; }

        public Monorail(int maxSpeed, float weight, Color mainColor, Color dopColor, bool window, bool sideStripe) :
            base(maxSpeed, weight, mainColor)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;
            Window = window;
            SideStrip = sideStripe;
        }

        public Monorail(string info) : base(info)
        {
            string[] strs = info.Split(';');
            if (strs.Length == 6)
            {
                MaxSpeed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                MainColor = Color.FromName(strs[2]);
                DopColor = Color.FromName(strs[3]);
                Window = Convert.ToBoolean(strs[4]);
                SideStrip = Convert.ToBoolean(strs[5]);
            }
        }

        public override void DrawMonorail(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            base.DrawMonorail(g);

            if (Window)
            {
                Brush window = new SolidBrush(DopColor);
                g.FillRectangle(window, _startPosX, _startPosY + 10, 20, 20);
                g.FillRectangle(window, _startPosX + 40, _startPosY + 10, 20, 20);
                g.DrawRectangle(pen, _startPosX, _startPosY + 10, 90, 20);

            }

            if (SideStrip)
            {
                Brush strip = new SolidBrush(DopColor);
                g.FillRectangle(strip, _startPosX, _startPosY + 35, 90, 10);
                g.DrawRectangle(pen, _startPosX, _startPosY + 35, 90, 10);
            }
        }
        public void SetDopColor(Color color)
        {
            DopColor = color;
        }

        public override string ToString()
        {
            return base.ToString() + ";" + DopColor.Name + ";" + Window + ";" + SideStrip;
        }

        public int CompareTo(Monorail other)
        {
            var res = (this is Locomotive).CompareTo(other is Locomotive);
            if (res != 0)
            {
                return res;
            }
            if (DopColor != other.DopColor)
            {
                DopColor.Name.CompareTo(other.DopColor.Name);
            }
            if (Window != other.Window)
            {
                return Window.CompareTo(other.Window);
            }
            if (SideStrip != other.SideStrip)
            {
                return SideStrip.CompareTo(other.SideStrip);
            }

            return 0;
        }

        public bool Equals(Monorail other)
        {
            var res = (this as Locomotive).Equals(other as Locomotive);
            if (!res)
            {
                return res;
            }
            if (GetType().Name != other.GetType().Name)
            {
                return false;
            }
            if (DopColor != other.DopColor)
            {
                return false;
            }
            if (Window != other.Window)
            {
                return false;
            }
            if (SideStrip != other.SideStrip)
            {
                return false;
            }
            return true;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) { return false; }

            if (!(obj is Monorail boatObj))
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsMonorail
{
    public class Monorail : Locomotive
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

        public override void DrawMonorail(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Brush dopBrush = new SolidBrush(Color.Black);
            base.DrawMonorail(g);

            if (Window)
            {
                Brush window = new SolidBrush(MainColor);
                g.FillRectangle(window, _startPosX, _startPosY + 10, 20, 20);
                g.FillRectangle(window, _startPosX + 40, _startPosY + 10, 20, 20);
                g.DrawRectangle(pen, _startPosX, _startPosY + 10, 90, 20);

            }

            if (SideStrip)
            {
                Brush strip = new SolidBrush(MainColor);
                g.FillRectangle(strip, _startPosX, _startPosY + 35, 90, 10);
                g.DrawRectangle(pen, _startPosX, _startPosY + 35, 90, 10);
            }
        }
    }
}
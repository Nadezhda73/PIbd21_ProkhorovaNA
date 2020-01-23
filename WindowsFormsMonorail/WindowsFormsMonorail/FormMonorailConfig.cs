using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsMonorail
{
    public partial class FormMonorailConfig : Form
    {
        private event trainDelegate eventAddTrain;

        ITransport train = null;

        public FormMonorailConfig()
        {
            InitializeComponent();
            panelBlack.MouseDown += panelColor_MouseDown;
            panelOrange.MouseDown += panelColor_MouseDown;
            panelGray.MouseDown += panelColor_MouseDown;
            panelGreen.MouseDown += panelColor_MouseDown;
            panelRed.MouseDown += panelColor_MouseDown;
            panelWhite.MouseDown += panelColor_MouseDown;
            panelYellow.MouseDown += panelColor_MouseDown;
            panelBlue.MouseDown += panelColor_MouseDown;

            buttonCancel.Click += (object sender, EventArgs e) => { Close(); };
        }

        private void panelColor_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Control).DoDragDrop((sender as Control).BackColor, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void DrawTrain()
        {
            if (train != null)
            {
                Bitmap bmp = new Bitmap(pictureBoxMonorail.Width, pictureBoxMonorail.Height);
                Graphics gr = Graphics.FromImage(bmp);
                train.SetPosition(5, 5, pictureBoxMonorail.Width, pictureBoxMonorail.Height);
                train.DrawMonorail(gr);
                pictureBoxMonorail.Image = bmp;
            }
        }

        public void AddEvent(trainDelegate ev)
        {
            if (eventAddTrain == null)
            {
                eventAddTrain = new trainDelegate(ev);
            }
            else
            {
                eventAddTrain += ev;
            }
        }
        private void labelMonorail_MouseDown(object sender, MouseEventArgs e)
        {
            labelMonorail.DoDragDrop(labelMonorail.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void labelLocomotive_Click(object sender, EventArgs e)
        {
            labelLocomotive.DoDragDrop(labelLocomotive.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void panelMonorail_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }
        private void panelMonorail_DragDrop(object sender, DragEventArgs e)
        {
            switch (e.Data.GetData(DataFormats.Text).ToString())
            {
                case "Локомотив":
                    train = new Locomotive(100, 500, Color.White);
                    break;
                case "Монорельс":
                    train = new Monorail(100, 500, Color.White, Color.Black, true, true);
                    break;
            }
            DrawTrain();
        }

        private void labelMainColor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Color)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void labelMainColor_DragDrop(object sender, DragEventArgs e)
        {
            if (train != null)
            {
                train.SetMainColor((Color)e.Data.GetData(typeof(Color)));
                DrawTrain();
            }
        }

        private void labelDopColor_DragDrop(object sender, DragEventArgs e)
        {
            if (train != null)
            {
                if (train is Monorail)
                {
                    (train as Monorail).SetDopColor((Color)e.Data.GetData(typeof(Color)));
                    DrawTrain();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            eventAddTrain?.Invoke(train);
            Close();
        }

        private void labelLocomotive_MouseDown(object sender, MouseEventArgs e)
        {
            labelLocomotive.DoDragDrop(labelLocomotive.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }

        
    }
}

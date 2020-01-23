﻿using System;
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
    public partial class FormMonorail : Form
    {
        private Monorail monorail;
        public FormMonorail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод отрисовки машины
        /// </summary>
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxMonorail.Width, pictureBoxMonorail.Height);
            Graphics gr = Graphics.FromImage(bmp);
            monorail.DrawMonorail(gr);
            pictureBoxMonorail.Image = bmp;
        }
        /// <summary>
        /// Обработка нажатия кнопки "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            monorail = new Monorail(rnd.Next(100, 300), rnd.Next(1000, 2000), Color.Blue,
           Color.Gray, true, true);
            monorail.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100), pictureBoxMonorail.Width,
           pictureBoxMonorail.Height);
            Draw();
        }
        /// <summary>
        /// Обработка нажатия кнопок управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    monorail.MoveTransport(Direction.Up);
                    break;
                case "buttonDown":
                    monorail.MoveTransport(Direction.Down);
                    break;
                case "buttonLeft":
                    monorail.MoveTransport(Direction.Left);
                    break;
                case "buttonRight":
                    monorail.MoveTransport(Direction.Right);
                    break;
            }
            Draw();
        }
    }
}

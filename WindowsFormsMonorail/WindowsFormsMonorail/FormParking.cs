using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace WindowsFormsMonorail
{
    public partial class FormParking : Form
    {
        MultiLevelParking parking;

        FormMonorailConfig form;

        private const int countLevel = 5;

        private Logger logger;

        public FormParking()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
            parking = new MultiLevelParking(countLevel, pictureBoxParking.Width, pictureBoxParking.Height);

            for (int i = 0; i < countLevel; i++)
            {
                listBoxLevels.Items.Add("Уровень " + (i + 1));
            }
            listBoxLevels.SelectedIndex = 0;
        }

        private void Draw()
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                Bitmap bmp = new Bitmap(pictureBoxParking.Width,
                    pictureBoxParking.Height);
                Graphics gr = Graphics.FromImage(bmp);
                parking[listBoxLevels.SelectedIndex].Draw(gr);
                pictureBoxParking.Image = bmp;
            }
        }


        private void ParkingLocomotive_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var train = new Locomotive(100, 1000, dialog.Color);
                    int place = parking[listBoxLevels.SelectedIndex] + train;
                    if (place == -1)
                    {
                        MessageBox.Show("Нет свободных мест", "Ошибка",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Draw();
                }
            }
        }

        private void ParkingMonorail_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        var train = new Monorail(100, 1000, dialog.Color,
                       dialogDop.Color, true, true);
                        int place = parking[listBoxLevels.SelectedIndex] + train;
                        if (place == -1)
                        {
                            MessageBox.Show("Нет свободных мест", "Ошибка",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Draw();
                    }
                }
            }
        }

        private void buttonTake_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                if (maskedTextBoxParking.Text != "")
                {
                    try
                    {
                        var train = parking[listBoxLevels.SelectedIndex] - Convert.ToInt32(maskedTextBoxParking.Text);

                        Bitmap bmp = new Bitmap(pictureBoxTake.Width, pictureBoxTake.Height);
                        Graphics gr = Graphics.FromImage(bmp);
                        train.SetPosition(5, 5, pictureBoxTake.Width, pictureBoxTake.Height);
                        train.DrawMonorail(gr);
                        logger.Info($"Изъята поезд {train.ToString()} с места {maskedTextBoxParking.Text}");
                        pictureBoxTake.Image = bmp;
                        Draw();
                    }

                    catch (ParkingNotFoundException ex)
                    {
                        logger.Warn(ex.Message);
                        MessageBox.Show(ex.Message, "Не найдено", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Bitmap bmp = new Bitmap(pictureBoxTake.Width, pictureBoxTake.Height);
                        pictureBoxTake.Image = bmp;
                    }

                    catch (Exception ex)
                    {
                        logger.Warn(ex.Message);
                        MessageBox.Show(ex.Message, "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void listBoxLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void buttonSelectTrain_Click(object sender, EventArgs e)
        {
            form = new FormMonorailConfig();
            form.AddEvent(AddTrain);
            form.Show();
        }

        private void AddTrain(ITransport train)
        {
            if (train != null && listBoxLevels.SelectedIndex > -1)
            {
                try
                {
                    int place = parking[listBoxLevels.SelectedIndex] + train;
                    Draw();
                    logger.Info($"Поезд {train.ToString()} помещен на место {place}");
                }
                catch (ParkingOverflowException ex)
                {
                    logger.Warn(ex.Message);
                    MessageBox.Show(ex.Message, "Переполнение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (ParkingAlreadyHaveException ex)
                {
                    logger.Warn(ex.Message);
                    MessageBox.Show(ex.Message, "Дублирование", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    logger.Warn(ex.Message);
                    MessageBox.Show(ex.Message, "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    parking.SaveData(saveFileDialog.FileName);
                    MessageBox.Show("Сохранение прошло успешно", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Сохранено в файл " + saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    logger.Warn(ex.Message);
                    MessageBox.Show(ex.Message, "Неизвестная ошибка при сохранении", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    parking.LoadData(openFileDialog.FileName);
                    MessageBox.Show("Загрузили", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Загружено из файла " + openFileDialog.FileName);
                }

                catch (ParkingOccupiedPlaceException ex)
                {
                    logger.Warn(ex.Message);
                    MessageBox.Show(ex.Message, "Занятое место", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    logger.Warn(ex.Message);
                    MessageBox.Show(ex.Message, "Неизвестная ошибка при загрузке", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Draw();
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            parking.Sort();
            Draw();
            logger.Info("Сортировка уровней");
        }
    }
}

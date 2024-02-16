﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ToursProject.Context.Models;

namespace ToursProject
{
    public partial class OrderView : UserControl
    {
        public Tour Tour;
        public int Count;
        public Action ChangeCount;

        public OrderView(Tour tour, int count)
        {
            InitializeComponent();
            Tour = tour;
            Count = count;
            labelTitle.Text = Tour.Title;
            labelPrice.Text = $"Цена {Tour.Price}руб.";
            labelCountry.Text = Tour.Country.ToString();
            labelDescription.Text = string.IsNullOrWhiteSpace(Tour.Description) ? 
                "Описание отсутствует" : Tour.Description;
            labelTicketCount.Text = $"Билетов {Tour.TicketCount}";
            numericUpDownCount.Value = count;
            listBox1.Items.AddRange(Tour.Types.ToArray());

            if (Tour.ImagePreview != null)
            {
                pictureBox1.Image = Image.FromStream(new MemoryStream(Tour.ImagePreview));
            }
        }

        private void numericUpDownCount_ValueChanged(object sender, System.EventArgs e)
        {
            Count = (int)numericUpDownCount.Value;
            ChangeCount?.Invoke();
        }
    }
}

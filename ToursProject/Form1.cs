﻿using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using ToursProject.Context;
using ToursProject.Context.Models;
using System.Collections.Generic;

namespace ToursProject
{
    public partial class Form1 : Form
    {
        private int allToursSum = 0;
        private List<Tour> Tours = new List<Tour>();
        public Form1()
        {
            InitializeComponent();
            comboBoxType.DisplayMember = nameof(TypeTour.Name);
            comboBoxType.ValueMember = nameof(TypeTour.Id);
            buttonAdd.Enabled = !WorkToUser.CompareRole(Context.Enum.Role.Quest) 
                && !WorkToUser.CompareRole(Context.Enum.Role.Client);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            using(var db = new ToursContext())
            {
                var types = db.TypeTours.ToArray();
                var defaultType = new TypeTour
                {
                    Id = -1,
                    Name = "Все типы"
                };

                comboBoxType.Items.AddRange(types);
                comboBoxType.Items.Insert(0, defaultType);
                comboBoxType.SelectedIndex = 0;

                var tours = db.Tours.Include(x => x.Types).Include(x => x.Country).ToList();
                allToursSum = 0;

                foreach (var item in tours)
                {
                    var tourView = new TourView(item);
                    tourView.AddToOrder += VisibleList;
                    tourView.Parent = flowLayoutPanel1;

                    allToursSum += (int)(item.Price * item.TicketCount);
                }

                label3.Text = $"Общая сумма: {allToursSum}руб.";
            }
        }

        private void VisibleList(Tour tour)
        {
            Tours.Add(tour);
            if(button1.Visible == false)
            {
                button1.Visible = true;
            }
        }

        private void checkBoxActual_CheckedChanged(object sender, System.EventArgs e)
        {
            Filter();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Filter();
        }

        private void textBoxSearch_TextChanged(object sender, System.EventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            if (comboBoxType.SelectedItem == null)
            {
                return;
            }

            var selectedTypeId = ((TypeTour)comboBoxType.SelectedItem).Id;
            allToursSum = 0;

            foreach (var item in flowLayoutPanel1.Controls)
            {
                var visible = true;

                if(item is TourView tourView)
                {
                    if(selectedTypeId != -1 
                        && !tourView.tour.Types.Any(x => x.Id == selectedTypeId))
                    {
                        visible = false;
                    }

                    if(checkBoxActual.Checked && !tourView.tour.IsActual)
                    {
                        visible = false;
                    }

                    if(!string.IsNullOrWhiteSpace(textBoxSearch.Text) 
                        && !tourView.tour.Title.Contains(textBoxSearch.Text))
                    {
                        visible = false;
                    }

                    if(visible)
                    {
                        allToursSum += (int)(tourView.tour.Price * tourView.tour.TicketCount);
                    }
                    tourView.Visible = visible;
                }
            }

            label3.Text = $"Общая сумма: {allToursSum}руб.";
        }

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            var form = new EditTour();

            if(form.ShowDialog() == DialogResult.OK)
            {
                using(var db = new ToursContext())
                {
                    var ids = form.GetCheckedTypes();
                    form.Tour.Types = db.TypeTours.Where(x => ids.Contains(x.Id)).ToList();
                    db.Tours.Add(form.Tour);
                    db.SaveChanges();
                }
            }

            var tourView = new TourView(form.Tour);
            tourView.Parent = flowLayoutPanel1;
            allToursSum += (int)(tourView.tour.Price * tourView.tour.TicketCount);
            label3.Text = $"Общая сумма: {allToursSum}руб.";
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
        }
    }
}

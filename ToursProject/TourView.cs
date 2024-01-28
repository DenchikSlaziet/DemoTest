using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ToursProject.Context;
using ToursProject.Context.Models;

namespace ToursProject
{
    public partial class TourView : UserControl
    {
        public Tour tour { get; set; }

        public TourView(Tour tour)
        {
            InitializeComponent();
            this.tour = tour;
            InitialComponent();
        }

        private void InitialComponent()
        {
            labelTitle.Text = tour.Title;
            labelActual.Text = tour.IsActual ? "Актуален" : "Не актуален";
            labelActual.ForeColor = tour.IsActual ? Color.Green : Color.Red;
            labelPrice.Text = $"{tour.Price} руб.";
            labelCount.Text = $"Билетов {tour.TicketCount}";
            if(tour.ImagePreview != null)
            {
                var image = Image.FromStream(new MemoryStream(tour.ImagePreview));
                pictureBoxImage.Image = image;
            }
        }

        private void buttonReview_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var image = File.ReadAllBytes(openFileDialog1.FileName);
                using (var db = new ToursContext())
                {
                    tour.ImagePreview = image;
                    db.Entry(tour).State = EntityState.Modified;
                    db.SaveChanges();
                }
                pictureBoxImage.Image = Image.FromStream(new MemoryStream(tour.ImagePreview));
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var form = new EditTour(tour);
            form.ShowDialog();
        }
    }
}

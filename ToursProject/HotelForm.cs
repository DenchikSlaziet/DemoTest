using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using ToursProject.AddForm;
using ToursProject.Context;
using ToursProject.Context.Models;

namespace ToursProject
{
    public partial class HotelForm : Form
    {
        private int pageActual = 1;
        private int pageSize = 3;

        public HotelForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            using (var db = new ToursContext())
            {
                Print();
                buttonBack.Enabled = false;
                buttonNext.Enabled = db.Hotels.Count() > pageSize;
            }

            buttonAdd.Enabled = buttonEdit.Enabled = buttonDeleted.Enabled = WorkToUser.CompareRole(Context.Enum.Role.Admin);

        }

        private void buttonNext_Click(object sender, System.EventArgs e)
        {
            pageActual++;
            Print();
        }

        private void Print()
        {
            using (var db = new ToursContext())
            {
                var count = db.Hotels.Count();
                var countPage = count / pageSize;
                labelCountPage.Text = count % pageSize == 0 ? countPage.ToString() : (++countPage).ToString();
                toolStripStatusLabelAllCount.Text = $"Кол-во записей {count}";
                if (pageActual > countPage)
                {
                    --pageActual;
                }
                dataGridView1.DataSource = db.Hotels.Include(x => x.Country).ToList().Skip((pageActual - 1) * pageSize).Take(pageSize).ToList();
                buttonNext.Enabled = buttonEndCount.Enabled = count > pageSize * pageActual;
                buttonBack.Enabled = buttonStartCount.Enabled = pageActual > 1;
                labelActual.Text = pageActual.ToString();
            }
        }

        private void buttonBack_Click(object sender, System.EventArgs e)
        {
            pageActual--;
            Print();
        }

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            var Hotel = new Hotel
            {
                CountOfStars = 1,
                CountryCode = "RU",
                Title = "Test"
            };

            using (var db = new ToursContext())
            {
                db.Hotels.Add(Hotel);
                db.SaveChanges();
            }
            Print();
        }

        private void buttonStartCount_Click(object sender, System.EventArgs e)
        {
            pageActual = 1;
            Print();
        }

        private void buttonEndCount_Click(object sender, System.EventArgs e)
        {
            using (var db = new ToursContext())
            {
                var count = db.Hotels.Count();
                var countPage = count / pageSize;
                pageActual = count % pageSize == 0 ? countPage : ++countPage;
            }
            Print();
        }

        private void buttonDeleted_Click(object sender, System.EventArgs e)
        {
            var hotel = (Hotel)dataGridView1.SelectedRows[0].DataBoundItem;

            if (hotel == null)
            {
                return;
            }

            using (var db = new ToursContext())
            {
                var hotelDB = db.Hotels.Include(x => x.Tours).FirstOrDefault(x => x.Id == hotel.Id);

                if (hotelDB.Tours.Count(x => x.IsActual) != 0)
                {
                    MessageBox.Show("Этот отель подходит к актуальным турам!!");
                }
                else if (MessageBox.Show($"Удалить ли отель {hotel.Title}?", "Подтвердите!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    db.Hotels.Remove(hotelDB);
                    db.SaveChanges();
                    Print();
                }
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var form = new EditHotel();

            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var db = new ToursContext())
                {
                    db.Hotels.Add(form.Hotel);
                    db.SaveChanges();
                    Print();
                }
            }
        }

        private void buttonEdit_Click(object sender, System.EventArgs e)
        {
            var hotelId = (Hotel)dataGridView1.SelectedRows[0].DataBoundItem;

            if (hotelId == null)
            {
                return;
            }
            using (var db = new ToursContext())
            {
                var hotel1 = db.Hotels.FirstOrDefault(x => x.Id == hotelId.Id);
                var form = new EditHotel(hotel1);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.SaveChanges();
                    Print();
                }
            }
        }
    }
}

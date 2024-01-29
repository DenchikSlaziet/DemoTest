using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
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
                dataGridView1.DataSource = db.Hotels.Include(x => x.Country).ToList().Skip((pageActual - 1) * pageSize).Take(pageSize).ToList();
                buttonNext.Enabled = count > pageSize*pageActual;
                buttonBack.Enabled = pageActual > 1;
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
        }
    }

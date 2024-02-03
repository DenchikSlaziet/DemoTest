using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ToursProject.Context;
using ToursProject.Context.Models;

namespace ToursProject
{
    public partial class OrderForm : Form
    {
        private Dictionary<Tour, int> Tours;
        private decimal sum = 0;
        private decimal sale = 0;

        public OrderForm(Dictionary<Tour, int> tours)
        {
            InitializeComponent();
            labelUserName.Text = $"{WorkToUser.User.LastName} {WorkToUser.User.FirstName} {WorkToUser.User.Patronymic}";
            Tours = tours;

            foreach (var item in Tours.Keys)
            {
                var orderView = new OrderView(item, Tours[item]);
                orderView.ChangeCount += UpdateSum;
                orderView.Margin = new Padding(0, 0, 0, 50);
                orderView.Parent = flowLayoutPanel1;
                orderView.Visible = true;
                sum += item.Price * Tours[item];
            }

            labelSum.Text = $"{sum}руб.";

            using(var db = new ToursContext())
            {
                comboBox1.Items.AddRange(db.ReceivingPoints.ToArray());
                comboBox1.SelectedIndex = 0;
            }
        }

        private void UpdateSum()
        {
            sum = 0;

            foreach (var item in flowLayoutPanel1.Controls)
            {          
                if(item is OrderView order)
                {
                    sum += order.Tour.Price * order.Count;
                }
            }

            labelSum.Text = $"{sum}руб.";
        }
    }
}

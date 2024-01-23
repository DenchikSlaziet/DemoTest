using System.Windows.Forms;
using ToursProject.Context.Models;

namespace ToursProject.UserControls
{
    public class UserView
    {
        private Tour tour { get; set; }
        public Label Title { get; set; } = new Label();
        public Label Price { get; set; } = new Label();
        public Label Actual { get; set; } = new Label();
        public Label CountTicket { get; set; } = new Label();

        public UserView(Tour tour, int x, int y, int number)
        {
            this.tour = tour;

            Title.AutoSize = true;
            Title.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Title.Location = new System.Drawing.Point(x+50, y);
            Title.Name = $"Title_{number}";
            Title.TabIndex = number;
            Title.Text = tour.Title;

            Price.AutoSize = true;
            Price.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Price.Location = new System.Drawing.Point(x+50, y+30);
            Price.Name = $"Price_{number}";
            Price.TabIndex = number;
            Price.Text = $"{tour.Price} руб.";

            Actual.AutoSize = true;
            Actual.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Actual.Location = new System.Drawing.Point(x, y+60);
            Actual.Name = $"Actual_{number}";
            Actual.TabIndex = number;
            if(tour.IsActual)
            {
                Actual.Text = "Актуален";
            }
            else
            {
                Actual.Text = "Неактуален";
            }

            CountTicket.AutoSize = true;
            CountTicket.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            CountTicket.Location = new System.Drawing.Point(x+100, y+60);
            CountTicket.Name = $"CountTicket_{number}";
            CountTicket.TabIndex = number;
            CountTicket.Text = $"Билетов: {tour.TicketCount}";
        }
    }
}

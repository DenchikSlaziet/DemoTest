using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ToursProject.Context.Models;
using ToursProject.UserControls;

namespace ToursProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var boxes = new List<UserView>();
            var tours = new List<Tour>();

            for (int i = 0; i < 12; i++)
            {
                var tour = new Tour
                {
                    Id = i,
                    Title = string.Join("",Guid.NewGuid().ToString().Take(7)),
                    IsActual = true,
                    Price = new Random().Next(100,100000),
                    TicketCount = new Random().Next(1, 100)
                };
                tours.Add(tour);
            }

            var y = 10;
            var n = 0;
            for (int i = 0; i < tours.Count; i++, n++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    n = 0;
                    y += 100;
                }

                var box = new UserView(tours[i], 250 * n, y, i);
                boxes.Add(box);
                Controls.Add(box.Title);
                Controls.Add(box.Price);
                Controls.Add(box.CountTicket);
                Controls.Add(box.Actual);
            }
        }
    }
}

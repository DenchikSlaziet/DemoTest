﻿using System;
using System.Windows.Forms;

namespace ToursProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            label1.Text = $"{WorkToUser.User.LastName} {WorkToUser.User.FirstName} {WorkToUser.User.Patronymic}";
            отелиToolStripMenuItem.Enabled = !WorkToUser.CompareRole(Context.Enum.Role.Meneger);
        }

        private void турыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Form1();

            form.ShowDialog();
        }

        private void отелиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new HotelForm();

            form.ShowDialog();
        }
    }
}

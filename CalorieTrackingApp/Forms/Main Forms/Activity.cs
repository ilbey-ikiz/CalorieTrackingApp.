using BLL;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndPL.Forms
{
    public partial class Activity : Form
    {
        public Activity(int id)
        {
            InitializeComponent();
            UserID = id;
            bl = new();
        }
        int UserID;
        BusinessLogic bl;
        int day;
        int activityID;

        /// <summary>
        /// Handles the Load event of the Activity form.
        /// Fills the data source for the DataGridView with the daily exercises.
        /// Sets up the ComboBox for selecting activities with the list of all activities.
        /// </summary>
        private void Activity_Load(object sender, EventArgs e)
        {
            FillDataSource();
            cmbExercise.DisplayMember = "ActivityName";
            cmbExercise.ValueMember = "ID";
            cmbExercise.DataSource = bl.Activities.GetAll();
            cmbExercise.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the btnAdd button.
        /// Validates the entered time value.
        /// Creates a new UserActivity object with the selected activity, user ID, and time values.
        /// Calculates the total burned calorie based on the activity's calories burned per minute and the entered time.
        /// Adds the UserActivity to the data source.
        /// Displays a success message box.
        /// Refreshes the data source and clears the form for adding a new activity.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (nudTime.Value != 0)
            {
                UserActivity userActivity = new();
                userActivity.UserID = UserID;
                userActivity.ActivityID = (int)cmbExercise.SelectedValue;
                userActivity.Minute = (int)nudTime.Value;
                var perMinuteCalorie = bl.Activities.GetById((int)cmbExercise.SelectedValue).CaloriesBurnedPerMinute;
                userActivity.TotalBurnedCalorie = perMinuteCalorie * nudTime.Value;
                bl.UserActivities.Add(userActivity);
                MessageBox.Show("Succesfully Added ");
                bl = new BusinessLogic();
                FillDataSource();

            }
            else MessageBox.Show("Time information cannot be left blank !");
        }

        /// <summary>
        /// Handles the Click event of the btnDelete button.
        /// Displays a warning message box to confirm the deletion of the selected activity.
        /// If the user confirms the deletion, the selected activity is removed from the data source.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (activityID != 0 && result == DialogResult.Yes)
            {
                bl.UserActivities.Remove(activityID);
                FillDataSource();
            }

        }

        /// <summary>
        /// Handles the CellClick event of the dgvDailyExervies DataGridView.
        /// Retrieves the ID of the selected activity from the clicked cell.
        /// </summary>
        private void dgvDailyExervies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            activityID = (int)dgvDailyExervies.SelectedRows[0].Cells[0].Value;
        }

        /// <summary>
        /// Fills the data source for the daily exercises DataGridView.
        /// Retrieves the activity list for the current user for today and binds it to the DataGridView.
        /// Updates the column headers and hides the first column.
        /// </summary>
        public void FillDataSource()
        {
            dgvDailyExervies.DataSource = null;
            dgvDailyExervies.DataSource = bl.UserActivities.GetActivityListToday(UserID);
            if (dgvDailyExervies.Rows.Count != 0)
            {
                dgvDailyExervies.Columns[0].Visible = false;
                dgvDailyExervies.Columns[1].HeaderText = "Activity Name";
                dgvDailyExervies.Columns[2].HeaderText = "Total Burned Calorie";
                dgvDailyExervies.Columns[3].HeaderText = "Minute";
            }
        }
    }
}

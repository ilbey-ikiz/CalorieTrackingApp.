using BLL;
using Entities;
using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndPL.Forms.Main_Forms.ReportForms
{
    public partial class ActivityReports : Form
    {
        public ActivityReports(int id)
        {
            InitializeComponent();
            userId = id;
        }
        BusinessLogic bl = new BusinessLogic();
        int userId;

        /// <summary>
        /// Initializes the ActivityReports form.
        /// Clears the existing data points in the lineDataSet.
        /// Retrieves the user's weight data from the database.
        /// Calculates the number of days passed since the user's creation.
        /// Iterates through each weight data and creates a new LPoint object.
        /// Sets the label of the LPoint to the corresponding day number.
        /// Sets the Y value of the LPoint to the weight value.
        /// Adds the LPoint to the lineDataSet's DataPoints collection.
        /// Calls the FillChart method to populate the activity chart.
        /// </summary>
        private void ActivityReports_Load(object sender, EventArgs e)
        {
            lineDataSet.DataPoints.Clear();
            User user = bl.Users.GetById(userId);

            foreach (var weight in user.Weights)
            {
                TimeSpan timePassed = weight.CreationTime - user.CreationTime;
                int day = (int)timePassed.TotalDays + 1;
                LPoint lpDay = new LPoint();
                lpDay.Label = day.ToString();
                lpDay.Y = (double)weight.Weight;
                lineDataSet.DataPoints.Add(lpDay);
            }

            FillChart();
        }

        /// <summary>
        /// Fills the activityChart with exercise data based on the selected time unit.
        /// Clears the existing data points in the activityBarChart.
        /// Retrieves the activities and corresponding times (calories or minutes) for the specified user ID.
        /// Configures the title and color of the activityChart based on the selected time unit.
        /// Iterates through each activity and creates a new LPoint object.
        /// Sets the label of the LPoint to the activity name.
        /// Sets the Y value of the LPoint to the corresponding calories or minutes.
        /// Adds the LPoint to the activityBarChart's DataPoints collection.
        /// </summary>
        public void FillChart()
        {
            var list = bl.UserActivities.GetActivitiesAndTimes(userId);
            activityBarChart.DataPoints.Clear();

            if (tsMinuteCalorie.Checked)
            {
                activityChart.Title.Text = "Exercises (Calorie)";
                activityChart.Title.ForeColor = Color.MediumPurple;
                foreach (var item in list)
                {
                    LPoint lPointCalorie = new LPoint();
                    lPointCalorie.Label = item.Item1;
                    lPointCalorie.Y = (double)item.Item3;
                    activityBarChart.DataPoints.Add(lPointCalorie);
                }
            }
            else
            {
                activityChart.Title.Text = "Exercises (Minute)";
                activityChart.Title.ForeColor = Color.Teal;
                foreach (var item in list)
                {
                    LPoint lPointMinute = new LPoint();
                    lPointMinute.Label = item.Item1;
                    lPointMinute.Y = item.Item2;
                    activityBarChart.DataPoints.Add(lPointMinute);
                }
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the tsMinuteCalorie toggle switch.
        /// Calls the FillChart method to update the activity chart based on the selected option (Minute or Calorie).
        /// </summary>
        private void tsMinuteCalorie_CheckedChanged(object sender, EventArgs e)
        {
            FillChart();
        }
    }
}

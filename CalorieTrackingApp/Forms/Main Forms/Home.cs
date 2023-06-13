using BLL;
using Castle.Core.Internal;
using Entities;
using Entities.Models;
using Guna.Charts.WinForms;
using Microsoft.VisualBasic.ApplicationServices;
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

namespace WndPL.Forms
{
    public partial class Home : Form
    {
        public Home(int id)
        {
            InitializeComponent();
            userId = id;
            user = bl.Users.GetById(userId);
        }

        BusinessLogic bl = new BusinessLogic();
        int userId;
        Entities.User user;
        decimal currentWeight = 0;
        decimal goalWeight = 0;

        private void Home_Load(object sender, EventArgs e)
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Load calorie-related data
            LoadCalorieData();

            // Fill the circle progress bar for daily calories
            FillDailyCalorieProgressBar();

            // Fill the doughnut chart for nutrient distribution
            FillNutrientDoughnutChart();

            // Fill the bar chart for weekly calories
            FillWeeklyCaloriesBarChart();
        }

        /// <summary>
        /// Loads the calorie data and updates the corresponding labels and text boxes.
        /// </summary>
        private void LoadCalorieData()
        {
            lblCalorie.Text = ((int)bl.Users.GetDailyCalorieById(userId)).ToString();
            lblGoalCalorie.Text = ((int)user.DailyGoalCalorie).ToString();
            int leftCalorie = (int)(Convert.ToDouble(lblGoalCalorie.Text) - Convert.ToDouble(lblCalorie.Text));
            if (leftCalorie >= 0)
                lblLeftCalorie.Text = "-" + leftCalorie.ToString();
            else
            {
                leftCalorie = leftCalorie * -1;
                lblLeftCalorie.Text = "+" + leftCalorie.ToString();
            }

            txtDayGoal.Text = user.DayGoal.ToString();
            txtCurrentWeight.Text = user.Weights.Last().Weight.ToString();
            txtGoalWeight.Text = user.GoalWeight.ToString();
            int remainingDay = CalculateRemainingDay();
            if (remainingDay > 0)
                lblRemainingDay.Text = remainingDay.ToString();
            else
            {
                lblRemainingDay.Text = "0";
                user.DayGoal = 0;
                bl.Users.Update(user);
            }

            // fill DailyRemaining circle progress bar
            FillDaysRemainingProgressBar();
        }

        /// <summary>
        /// Fills the circle progress bar for remaining days based on the user's day goal.
        /// </summary>
        private void FillDaysRemainingProgressBar()
        {
            if (txtDayGoal.Text != "0")
            {
                int remainingDay = CalculateRemainingDay();
                txtDayGoal.Enabled = false;
                txtGoalWeight.Enabled = false;
                cpbDaysRemaining.Maximum = user.DayGoal;
                cpbDaysRemaining.Value = user.DayGoal - remainingDay;
                lblRemainingDay.Text = remainingDay.ToString();
            }
        }

        /// <summary>
        /// Fills the circle progress bar for daily calories based on the user's daily goal calorie.
        /// </summary>
        private void FillDailyCalorieProgressBar()
        {
            cpbDailyCalorie.Maximum = user.DailyGoalCalorie;
            cpbDailyCalorie.Value = (int)bl.Users.GetDailyCalorieById(userId);
        }

        /// <summary>
        /// Fills the doughnut chart for nutrient distribution based on the user's daily nutrient percentages.
        /// </summary>
        private void FillNutrientDoughnutChart()
        {
            bl.Users.GetDailyNutrientsPercentageById(userId, out double dailyProteinGram, out double dailyFatGram, out double dailyCarbohydrateGram);
            doughnutData.DataPoints.Clear();
            LPoint lpProtein = new LPoint();
            LPoint lpFat = new LPoint();
            LPoint lpCarbohydrate = new LPoint();
            lpProtein.Label = "Protein";
            lpProtein.Y = Math.Round(dailyProteinGram, 2);
            lpFat.Label = "Fat";
            lpFat.Y = Math.Round(dailyFatGram, 2);
            lpCarbohydrate.Label = "Carbohydrate";
            lpCarbohydrate.Y = Math.Round(dailyCarbohydrateGram, 2);
            doughnutData.DataPoints.AddRange(new LPoint[] { lpProtein, lpFat, lpCarbohydrate });
            double totalGram = dailyProteinGram + dailyFatGram + dailyCarbohydrateGram;
            double ProteinPercentage = Math.Round((dailyProteinGram / totalGram) * 100, 2);
            double FatPercentage = Math.Round((dailyFatGram / totalGram) * 100, 2);
            double CarbohydratePercentage = Math.Round((dailyCarbohydrateGram / totalGram) * 100, 2);
            lblProteinPercentage.Text = $"%{ProteinPercentage}";
            lblFatPercentage.Text = $"%{FatPercentage}";
            lblCarbohydratePercentage.Text = $"%{CarbohydratePercentage}";
        }

        /// <summary>
        /// Fills the bar chart for weekly calories based on the user's calorie data for the past week.
        /// </summary>
        private void FillWeeklyCaloriesBarChart()
        {
            barData.DataPoints.Clear();
            for (int i = 0, k = 5; i < 6; i++)
            {
                LPoint lpDay = new LPoint();
                DateTime day = DateTime.Today.AddDays(-(i + 1));
                lpDay.Label = day.DayOfWeek.ToString();
                lpDay.Y = bl.Users.GetSpesificDayCaloriesById(userId, (i + 1));
                k--;
                barData.DataPoints.Add(lpDay);
            }
            LPoint lpDay7 = new LPoint();
            lpDay7.Label = DateTime.Now.DayOfWeek.ToString();
            lpDay7.Y = Convert.ToDouble(lblCalorie.Text);
            barData.DataPoints.Add(lpDay7);
        }

        /// <summary>
        /// Calculates the remaining days based on the user's day goal.
        /// </summary>
        /// <returns>The number of remaining days.</returns>
        public int CalculateRemainingDay()
        {
            TimeSpan timePassed = DateTime.Now - user.DayGoalCreationTime;
            int day = (int)timePassed.TotalDays + 1;
            int remainingDay = user.DayGoal - day;
            return remainingDay;
        }

        /// <summary>
        /// Handles the KeyPress event for the txtWeightTexts control, allowing only digits, comma, and backspace characters to be entered.
        /// </summary>
        private void txtWeightTexts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        #region txtDayGoal
        /// <summary>
        /// Handles the KeyPress event for the txtDayGoal control, allowing only digits and backspace characters to be entered.
        /// </summary>
        private void txtDayGoal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the TextChanged event for the txtDayGoal control, updates the user's day goal if the entered value is a valid integer, and saves the updated user.
        /// </summary>
        private void txtDayGoal_TextChanged(object sender, EventArgs e)
        {
            if (txtDayGoal.Text.All(char.IsDigit) && !txtDayGoal.Text.IsNullOrEmpty())
            {
                user.DayGoal = Convert.ToInt32(txtDayGoal.Text);
                bl.Users.Update(user);
            }
        }

        /// <summary>
        /// Handles the Leave event for the txtDayGoal control, displays a confirmation message to set a day goal, and updates the days remaining progress bar if confirmed.
        /// </summary>
        private void txtDayGoal_Leave(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to set a day goal?\n\n Note: If you confirm this action, you will not make any changes in the \"Target Weight\" and \"Target Day\" sections on the home screen until the targeted day ends. You can use the user information screen to make changes. ", "Deletion Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                FillDaysRemainingProgressBar();
            }
            else
                txtDayGoal.Text = "0";
        }
        #endregion
        private void txtGoalWeight_Enter(object sender, EventArgs e)
        {
            goalWeight = Convert.ToDecimal(txtGoalWeight.Text);
        }
        private void txtGoalWeight_Leave(object sender, EventArgs e)
        {
            if (char.IsDigit(txtGoalWeight.Text[0]) && char.IsDigit(txtGoalWeight.Text[^1]))
            {
                user.GoalWeight = Convert.ToDecimal(txtGoalWeight.Text);
                txtGoalWeight.Text = user.GoalWeight.ToString();
                bl.Users.Update(user);
            }
            else
                txtGoalWeight.Text = goalWeight.ToString();
        }


        #region txtCurrentWeight
        /// <summary>
        /// Handles the Enter event for the txtCurrentWeight control, stores the current weight value for reference.
        /// </summary>
        private void txtCurrentWeight_Enter(object sender, EventArgs e)
        {
            currentWeight = Convert.ToDecimal(txtCurrentWeight.Text);
        }

        /// <summary>
        /// Handles the Leave event for the txtCurrentWeight control, validates the entered weight value and updates the user's weight history if valid.
        /// If the entered value is invalid, restores the previous weight value.
        /// </summary>
        private void txtCurrentWeight_Leave(object sender, EventArgs e)
        {
            if (char.IsDigit(txtCurrentWeight.Text[0]) && char.IsDigit(txtCurrentWeight.Text[^1]))
            {
                WeightHistory newWeight = new WeightHistory();
                newWeight.Weight = Convert.ToDecimal(txtCurrentWeight.Text);
                newWeight.UserId = user.ID;
                bl.WeightHistories.Add(newWeight);
                txtCurrentWeight.Text = newWeight.Weight.ToString();
            }
            else
                txtCurrentWeight.Text = currentWeight.ToString();
        }
        #endregion



    }
}
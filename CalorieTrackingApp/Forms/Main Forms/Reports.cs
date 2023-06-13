using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WndPL.Forms;
using WndPL.Forms.Main_Forms.ReportForms;
using WndPL.Forms.ReportForms;

namespace WndPL.Forms
{
    public partial class Reports : Form
    {
        public Reports(int id)
        {
            InitializeComponent();
            userId = id;

        }
        Helper helper = new Helper();
        int userId;

        /// <summary>
        /// Handles the Load event for the Reports form.
        /// Initializes and displays the FoodReports panel.
        /// </summary>
        private void Reports_Load(object sender, EventArgs e)
        {

            FoodReports foodReports = new FoodReports(userId);
            helper.ShowPanel(foodReports, pnlMain);
        }

        /// <summary>
        /// Handles the Click event for the btnMeals button.
        /// Initializes and displays the MealsReports panel.
        /// </summary>
        private void btnMeals_Click(object sender, EventArgs e)
        {
            MealsReports mealsReports = new MealsReports(userId);
            helper.ShowPanel(mealsReports, pnlMain);
        }

        /// <summary>
        /// Handles the Click event for the btnFoods button.
        /// Initializes and displays the FoodReports panel.
        /// </summary>
        private void btnFoods_Click(object sender, EventArgs e)
        {
            FoodReports foodReports = new FoodReports(userId);
            helper.ShowPanel(foodReports, pnlMain);
        }

        /// <summary>
        /// Handles the Click event for the btnActivity button.
        /// Initializes and displays the ActivityReports panel.
        /// </summary>
        private void btnActivity_Click(object sender, EventArgs e)
        {
            ActivityReports activityReports = new ActivityReports(userId);
            helper.ShowPanel(activityReports, pnlMain);
        }
    }
}

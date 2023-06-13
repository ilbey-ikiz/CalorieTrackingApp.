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

namespace WndPL.Forms
{
    public partial class Main : Form
    {
        public Main(int id)
        {
            InitializeComponent();
            userId = id;
        }
        BusinessLogic bl = new BusinessLogic();
        Helper helper = new Helper();
        int userId;
        /// <summary>
        /// Handles the load event of the Main form. Initializes the home form, displays user information, and disables certain buttons.
        /// </summary>
        private void Main_Load(object sender, EventArgs e)
        {
            DisplayUserInfo();
            InitializeHomeForm();
        }

        /// <summary>
        /// Initialize the home form and displays it in the main panel.
        /// </summary>
        private void InitializeHomeForm()
        {
            Home homeForm = new Home(userId);
            helper.ShowPanel(homeForm, pnlMain);
        }

        /// <summary>
        /// Displays the user information in the respective labels and profile picture.
        /// </summary>
        private void DisplayUserInfo()
        {
            Entities.User user = bl.Users.GetById(userId);
            lblName.Text = user.FullName;
            lblMail.Text = user.Mail;
            btnDiets.Enabled = false;

            if (user.Image != null)
            {
                byte[] imageData = user.Image;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    picboxProfile.Image = Image.FromStream(ms);
                }
            }
        }

        /// <summary>
        /// Handles the click event of the Home button. Displays the home form.
        /// </summary>
        private void btnHome_Click(object sender, EventArgs e)
        {
            Home homeForm = new Home(userId);
            helper.ShowPanel(homeForm, pnlMain);
        }
        /// <summary>
        /// Handles the click event of the Meals button. Displays the meals form.
        /// </summary>
        private void btnMeals_Click(object sender, EventArgs e)
        {
            Meals mealForm = new Meals(userId);
            helper.ShowPanel(mealForm, pnlMain);
        }
        /// <summary>
        /// Handles the click event of the Reports button. Displays the reports form.
        /// </summary>
        private void btnReports_Click(object sender, EventArgs e)
        {
            Reports reportsForm = new Reports(userId);
            helper.ShowPanel(reportsForm, pnlMain);
        }

        /// <summary>
        /// Handles the click event of the User Settings button. Displays the user settings form.
        /// </summary>
        private void btnUserSettings_Click(object sender, EventArgs e)
        {
            UserSettings userSettingsForm = new UserSettings(userId);
            helper.ShowPanel(userSettingsForm, pnlMain);
        }
        /// <summary>
        /// Handles the click event of the Exit button. Closes the application.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActivity_Click(object sender, EventArgs e)
        {
            Activity activityForm = new Activity(userId);
            helper.ShowPanel(activityForm, pnlMain);
        }
    }
}

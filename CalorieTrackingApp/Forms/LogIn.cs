using BLL;
using Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndPL.Forms
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }
        BusinessLogic bl = new BusinessLogic();
        Helper helper = new Helper();
        /// <summary>
        /// Handles the load event of the LogIn form. Clears the email and password fields.
        /// </summary>
        private void LogIn_Load(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtPassword.Clear();
        }

        /// <summary>
        /// Toggles the visibility of the password in the text box based on the checked state of the checkbox.
        /// </summary>
        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        /// <summary>
        /// Handles the click event of the Login button. Validates the user login information and opens the main form if successful.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool emptyControl = helper.AreTextBoxesEmpty(this);
            if (emptyControl)
            {
                MessageBox.Show("You entered incorrect information. \nPlease fill in all fields.");
                return;
            }

            var users = bl.Users.GetAll();
            User user = users.SingleOrDefault(a => a.Mail == txtEmail.Text);

            if (user == null)
            {

                MessageBox.Show("User not found. \nPlease click the 'Sign Up' button to register.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            }
            else if (user != null && (user.Mail != txtEmail.Text || user.Password != txtPassword.Text))
            {
                MessageBox.Show("Username or password incorrect", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            }
            else
            {
                Main main = new(user.ID);
                helper.HideAndShow(this, main);
            }
        }
        /// <summary>
        /// Handles the click event of the Sign In button. Opens the Sign Up form for user registration.
        /// </summary>
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            helper.HideAndShow(this, signUp);
        }
        /// <summary>
        /// Handles the click event of the Exit button. Displays a farewell message and exits the application.
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks for visiting us. \nWe wish you a healthy day.");
            Application.Exit();
        }


    }
}

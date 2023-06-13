using BLL;
using Entities;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }
        BusinessLogic bl = new BusinessLogic();
        Helper helper = new Helper();

        /// <summary>
        /// Handles the click event of the Back button. Closes the current form.
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the click event of the Confirm button. Performs validation on the input fields and creates a new user if the input is valid.
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Entities.User existingUser = bl.Users.GetByEmail(txtMail.Text);
                if (existingUser != null)
                {
                    MessageBox.Show("This email address already exists in the system. Please check your information.");
                }
                else
                {
                    CreateUserAndOpenUserInformationForm();
                }
            }
        }

        /// <summary>
        /// Creates a new user with the provided information, adds it to the database, and opens the UserInformation form.
        /// </summary>
        private void CreateUserAndOpenUserInformationForm()
        {
            Entities.User newUser = new Entities.User()
            {
                Mail = txtMail.Text,
                Password = txtPassword.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text
            };
            //bl.Users.Add(newUser);

            UserInformation userInfo = new UserInformation(newUser);
            helper.HideAndShow(this, userInfo);
        }

        /// <summary>
        /// Validates the input fields for user registration.
        /// </summary>
        /// <returns>True if the input is valid, otherwise false.</returns>
        private bool ValidateInput()
        {
            if (helper.AreTextBoxesEmpty(this))
            {
                MessageBox.Show("Fields cannot be empty. Please enter your missing information.");
                return false;
            }

            if (ContainsNumeric(txtFirstName.Text) || ContainsNumeric(txtLastName.Text))
            {
                MessageBox.Show("Please enter a valid name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtFirstName.Text.Length < 2 || txtLastName.Text.Length < 2)
            {
                MessageBox.Show("Name and surname must contain at least 2 letters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtMail.Text != txtMailRepeat.Text)
            {
                MessageBox.Show("The email addresses you entered do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!IsPasswordValid(txtPassword.Text))
            {
                MessageBox.Show("Passwords do not meet the criteria:\n\n• Must be at least 8 characters long.\n• Must contain at least 2 uppercase letters.\n• Must contain at least 3 lowercase letters.\n• Must contain at least 2 of the characters: ! (exclamation), : (colon), + (plus), * (asterisk).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtPassword.Text != txtPasswordRepeat.Text)
            {
                MessageBox.Show("Passwords do not match. Please check passwords.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the password meets the required criteria.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        private bool IsPasswordValid(string password)
        {
            int minLength = 8;
            int minUpperCase = 2;
            int minLowerCase = 3;
            int minSpecialChars = 2;

            if (password.Length < minLength)
                return false;

            if (password.Count(char.IsUpper) < minUpperCase)
                return false;

            if (password.Count(char.IsLower) < minLowerCase)
                return false;

            if (password.Count(c => "!:+*".Contains(c)) < minSpecialChars)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the text contains at least one numeric character.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True if the text contains at least one numeric character, otherwise false.</returns>
        private bool ContainsNumeric(string text)
        {
            return text.Any(char.IsDigit);
        }
    }
}

using BLL;
using Castle.Core.Internal;
using DAL;
using Entities;
using Entities.Enums;
using Entities.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WndPL.Forms
{
    public partial class UserSettings : Form
    {
        public UserSettings(int id)
        {
            InitializeComponent();
            userId = id;
            user = bl.Users.GetById(userId);
        }
        BusinessLogic bl = new BusinessLogic();
        Helper helper = new Helper();
        int userId;
        Entities.User user;

        /// <summary>
        /// Loads the UserSettings form and initializes the user data.
        /// </summary>
        private void UserSettings_Load(object sender, EventArgs e)
        {
            LoadGenderComboBox();
            LoadUserData();
        }

        /// <summary>
        /// Loads the gender options into the gender combo box and selects the user's gender.
        /// </summary>
        private void LoadGenderComboBox()
        {
            cmbGender.Items.AddRange(Enum.GetNames(typeof(Gender)));
            cmbGender.SelectedIndex = (int)user.Gender - 1;
        }

        /// <summary>
        /// Loads the user data into the form controls.
        /// </summary>
        private void LoadUserData()
        {
            txtEmail.Text = user.Mail;
            txtEmail.ReadOnly = true;
            txtFirstName.Text = user.FirstName.ToString();
            txtLastName.Text = user.LastName.ToString();
            txtAge.Text = user.Age.ToString();
            txtHeight.Text = user.Height.ToString();
            txtWeight.Text = user.Weights.Last().Weight.ToString();
            txtDailyCaloriesGoal.Text = user.DailyGoalCalorie.ToString();
            txtWeightGoal.Text = user.GoalWeight.ToString();
            txtDayGoal.Text = user.DayGoal.ToString();
            if (user.PhoneNumber != null)
                mtbTelephone.Text = user.PhoneNumber.ToString();
            if (user.Image != null)
                pbChangePicture.Image = Image.FromStream(new MemoryStream(user.Image));
        }

        /// <summary>
        /// Handles the save button click event. Validates the input and updates the user data.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                UpdateUserData();
                bool result = bl.Users.Update(user);
                if (result)
                    MessageBox.Show("Update successful.");
                else
                    MessageBox.Show("Update failed.");
            }
        }

        /// <summary>
        /// Updates the user data based on the input values.
        /// </summary>
        private void UpdateUserData()
        {
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Age = Convert.ToInt32(txtAge.Text);
            user.Gender = (Gender)cmbGender.SelectedIndex + 1;
            user.PhoneNumber = new string(mtbTelephone.Text.Where(char.IsDigit).ToArray());
            user.Height = Convert.ToDecimal(txtHeight.Text);

            if(!string.IsNullOrEmpty(txtNewPassword.Text))
                user.Password = txtNewPassword.Text;

            var newWeight = new WeightHistory
            {
                Weight = Convert.ToDecimal(txtWeight.Text),
                UserId = user.ID
            };
            user.Weights.Add(newWeight);

            user.GoalWeight = Convert.ToDecimal(txtWeightGoal.Text);
            user.DailyGoalCalorie = Convert.ToInt32(txtDailyCaloriesGoal.Text);
            user.DayGoal = Convert.ToInt32(txtDayGoal.Text);

            if (pbChangePicture.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    pbChangePicture.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    user.Image = ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Validates the input values entered by the user.
        /// </summary>
        /// <returns>True if the input is valid, False otherwise.</returns>
        private bool ValidateInput()
        {
            bool control = true;
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
            //|| (!string.IsNullOrEmpty(txtNewPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text))
            if (!IsPasswordValid(txtNewPassword.Text))
            {
                if (string.IsNullOrEmpty(txtNewPassword.Text) && string.IsNullOrEmpty(txtConfirmPassword.Text))
                    control = false;
                if (control) 
                { 
                MessageBox.Show("Passwords do not meet the criteria.\nPlease check the password requirements.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
                }
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match. Please check passwords.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (user.Password != txtPassword.Text)
            {
                MessageBox.Show("You entered your previous password incorrectly. Please check passwords.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!helper.StartAndEndWithDigit(pnlText))
            {
                MessageBox.Show("Make sure you enter your information correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the given text contains any numeric characters.
        /// </summary>
        /// <param name="text">The text to check for numeric characters.</param>
        /// <returns>True if the text contains numeric characters, false otherwise.</returns>
        private bool ContainsNumeric(string text)
        {
            return text.Any(char.IsDigit);
        }

        /// <summary>
        /// Checks if the given password meets the specified criteria.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the password meets the criteria, false otherwise.</returns>
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
        /// Handles the key press event for text boxes to allow only numeric input.
        /// </summary>
        private void Texts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the click event for changing the user picture.
        /// </summary>
        private void btnChangeUserPicture_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.Title = "Select Photo";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                Image originalImage = Image.FromFile(imagePath);
                int maxWidth = 100;
                int maxHeight = 100;

                int newWidth, newHeight;
                if (originalImage.Width > originalImage.Height)
                {
                    newWidth = maxWidth;
                    newHeight = (int)(originalImage.Height * (float)newWidth / originalImage.Width);
                }
                else
                {
                    newHeight = maxHeight;
                    newWidth = (int)(originalImage.Width * (float)newHeight / originalImage.Height);
                }

                Image resizedImage = new Bitmap(originalImage, newWidth, newHeight);
                MemoryStream ms = new MemoryStream();
                resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                pbChangePicture.Image = resizedImage;
                pbChangePicture.SizeMode = PictureBoxSizeMode.CenterImage;
                byte[] imageData = File.ReadAllBytes(imagePath);
                user.Image = imageData;
            }
        }
    }
}

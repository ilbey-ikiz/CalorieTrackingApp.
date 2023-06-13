using BLL;
using Castle.Core.Internal;
using DAL;
using Entities;
using Entities.Enums;
using Entities.Models;
using FluentFTP.Helpers;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WndPL.Forms
{
    public partial class UserInformation : Form
    {
        public UserInformation(Entities.User user)
        {
            InitializeComponent();
            this.user = user;
        }

        BusinessLogic bl = new BusinessLogic();
        Helper helper = new Helper();
        Entities.User user;
        bool result;

        /// <summary>
        /// Handles the Load event of the UserInformation form. Initializes the form by setting up the gender dropdown list and setting the initial value of the body mass index textbox.
        /// </summary>
        private void UserInformation_Load(object sender, EventArgs e)
        {

            cmbGender.Items.AddRange(Enum.GetNames(typeof(Gender)));
            txtBodyMassIndex.ReadOnly = true;
            txtBodyMassIndex.Text = "0";

            foreach (var item in Enum.GetNames(typeof(ActivityFrequency)))
                cmbActivity.Items.Add(item);
            cmbActivity.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the click event of the Calculate button. Calculates the body mass index based on the entered height and weight and displays the result in the body mass index textbox.
        /// </summary>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHeight.Text) || string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                MessageBox.Show("This operation cannot be performed without entering height and weight information.");
            }
            else
            {
                double height = Convert.ToDouble(txtHeight.Text) / 100.0;
                double weight = Convert.ToDouble(txtHeight.Text);
                double bodyMassIndex = weight / (height * height);
                txtBodyMassIndex.Text = bodyMassIndex.ToString("0.00");
            }
        }

        /// <summary>
        /// Handles the click event of the Confirm button. Performs validation on the input fields and saves the user data if the input is valid.
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {

            bool emptyControl = helper.AreTextBoxesEmptyExceptOne(this.pnlRight, txtDayTarget);
            if (emptyControl || cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Fields cannot be empty. Please enter your missing information.");
            }
            else
            {
                if (helper.StartAndEndWithDigitExceptOne(this.pnlRight, txtDayTarget))
                {

                    SetUserData();                  
                    if (result)
                    {
                        MessageBox.Show("Registration successful.");
                        this.Close();
                    }
                    else
                        MessageBox.Show("Registration failed.");
                }
                else
                {
                    MessageBox.Show("Make sure you enter your information correctly.");
                }
            }
        }
        /// <summary>
        /// Sets the user data based on the form fields.
        /// </summary>
        private void SetUserData()
        {
            user.Height = Convert.ToDecimal(txtHeight.Text);
            user.GoalWeight = Convert.ToDecimal(txtTargetWeight.Text);
            user.Activity = (ActivityFrequency)cmbActivity.SelectedIndex + 1;
            user.Gender = (Gender)cmbGender.SelectedIndex + 1;
            user.Age = Convert.ToInt32(txtAge.Text);
            user.DailyGoalCalorie = (int)bl.Users.CalculateBasalMetabolism(txtWeight.Text,user);
            string phoneNumber = new string(mtbTelephone.Text.Where(char.IsDigit).ToArray());
            user.PhoneNumber = phoneNumber;
            if (!string.IsNullOrEmpty(txtDayTarget.Text))
                user.DayGoal = Convert.ToInt32(txtDayTarget.Text);
            result = bl.Users.Add(user);
            WeightHistory weight = new WeightHistory();
            weight.Weight = Convert.ToDecimal(txtWeight.Text);
            weight.UserId = user.ID;
            bl.WeightHistories.Add(weight);
        }

        /// <summary>
        /// Handles the click event of the Add Photo button. Allows the user to select and add a photo to their profile.
        /// </summary>
        private void btnAddPhoto_Click(object sender, EventArgs e)
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
                pbPhoto.Image = resizedImage;
                pbPhoto.SizeMode = PictureBoxSizeMode.CenterImage;
                byte[] imageData = File.ReadAllBytes(imagePath);
                user.Image = imageData;
            }
        }

        /// <summary>
        /// Handles the click event of the Back button. Closes the current form.
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the KeyPress event of the Textbox controls. Allows only digits, comma, and backspace characters to be entered.
        /// </summary>
        private void Texts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }


    }

}


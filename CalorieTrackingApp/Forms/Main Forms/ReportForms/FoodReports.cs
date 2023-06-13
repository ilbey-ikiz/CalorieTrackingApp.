using BLL;
using Entities;
using Entities.Enums;
using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndPL.Forms.ReportForms
{
    public partial class FoodReports : Form
    {
        public FoodReports(int id)
        {
            InitializeComponent();
            userId = id;
        }
        BusinessLogic bl = new BusinessLogic();
        Helper helper = new Helper();
        byte[] imageData1;
        int foodId;
        int userId;

        /// <summary>
        /// Handles the Load event of the FoodReports form.
        /// Populates the category dropdown list with the available food categories.
        /// </summary>
        private void FoodReports_Load(object sender, EventArgs e)
        {
            foreach (var item in Enum.GetNames(typeof(FoodCategory)))
            {
                cbCategory.Items.Add(item);
            }
        }

        #region Food Search

        /// <summary>
        /// Handles the TextChanged event of the txtFoodSearch TextBox.
        /// Searches for foods based on the entered text and populates the search results in the ListView.
        /// </summary>
        private void txtFoodSearch_TextChanged(object sender, EventArgs e)
        {

            lvSearchedFoods.Items.Clear();
            List<Food> searchedFoods = bl.Foods.GetFoodsByContainTextAndOwnerId(txtFoodSearch.Text.Trim(), userId);
            foreach (var food in searchedFoods)
            {
                ListViewItem lv1 = new ListViewItem();
                lv1.Tag = food.ID;
                lv1.Text = food.Name;
                lv1.SubItems.Add(food.Category.ToString());
                lvSearchedFoods.Items.Add(lv1);
            }


            lvSearchedFoods.Show();
        }

        /// <summary>
        /// Handles the Leave event of the txtFoodSearch TextBox.
        /// Shows the lblFoodSearch label.
        /// </summary>
        private void txtFoodSearch_Leave(object sender, EventArgs e)
        {
            lblFoodSearch.Show();

        }
        /// <summary>
        /// Handles the Enter event of the txtFoodSearch TextBox.
        /// Hides the lblFoodSearch label.
        /// </summary>
        private void txtFoodSearch_Enter(object sender, EventArgs e)
        {
            lblFoodSearch.Hide();
        }
        /// <summary>
        /// Handles the Click event of the lblFoodSearch label.
        /// Hides the lblFoodSearch label and sets focus to the txtFoodSearch TextBox.
        /// </summary>
        private void lblFoodSearch_Click(object sender, EventArgs e)
        {
            lblFoodSearch.Hide();
            txtFoodSearch.Focus();
        }
        /// <summary>
        /// Handles the Click event of the FoodReports form.
        /// Hides the lvSearchedFoods ListView, shows the lblFoodSearch label, and sets focus to the lblFat label.
        /// </summary>
        private void FoodReports_Click(object sender, EventArgs e)
        {
            lvSearchedFoods.Hide();
            lblFoodSearch.Show();
            lblFat.Focus();
        }
        #endregion

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lvSearchedFoods ListView.
        /// Updates the form with the details of the selected food item.
        /// </summary>
        private void lvSearchedFoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvSearchedFoods.Hide();
            if (lvSearchedFoods.SelectedItems.Count > 0)
            {
                txtFoodName.Text = lvSearchedFoods.SelectedItems[0].Text;
                foodId = (int)lvSearchedFoods.SelectedItems[0].Tag;
                Food selectedFood = bl.Foods.GetById(foodId);

                // fill food deatils
                txtCalorie.Text = selectedFood.CalorieFor100Gram.ToString();
                cbCategory.SelectedIndex = (int)selectedFood.Category - 1;
                txtPortion.Text = selectedFood.PortionGram.ToString();
                txtProtein.Text = selectedFood.ProteinRateFor100Gram.ToString();
                txtFat.Text = selectedFood.FatRateFor100Gram.ToString();
                txtCarbohydrate.Text = selectedFood.CarbonhydrateAmountFor100Gram.ToString();
                if (selectedFood.Image != null)
                {
                    byte[] imageData = selectedFood.Image;
                    MemoryStream ms = new MemoryStream(imageData);
                    var image = Image.FromStream(ms);
                    picboxFoodPicture.Image = image;
                }
                else
                {
                    string imagePath = "~\\..\\..\\..\\..\\Resources\\Icons\\FoodReports Icons\\2427852_burger_cheeseburger_fast food_food_junk food_icon.png";
                    picboxFoodPicture.Image = Image.FromFile(imagePath);
                }
                if (selectedFood.OwnerId == 0)
                {
                    txtFoodName.ReadOnly = true;
                    txtCalorie.ReadOnly = true;
                    txtPortion.ReadOnly = true;
                    txtProtein.ReadOnly = true;
                    txtFat.ReadOnly = true;
                    txtCarbohydrate.ReadOnly = true;
                    cbCategory.Enabled = false;
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                }
                else
                {
                    txtFoodName.ReadOnly = false;
                    txtCalorie.ReadOnly = false;
                    txtPortion.ReadOnly = false;
                    txtProtein.ReadOnly = false;
                    txtFat.ReadOnly = false;
                    txtCarbohydrate.ReadOnly = false;
                    cbCategory.Enabled = true;
                    btnSave.Enabled = true;
                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;
                }

                //fill place and total and Quantity and Count
                FillPlaceAndTotal(MealType.Breakfast, MealType.Lunch, MealType.Dinner, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);

                //fill FoodAvgDayConsume Pie chart 
                int dataPointCount = pieDataFoodAvgDayConsume.DataPointCount;
                pieDataFoodAvgDayConsume.DataPoints.Clear();
                List<ConsumedFood> consumedFoods = bl.ConsumedFoods.GetAll();
                int totalDay = bl.ConsumedFoods.GetTotalDaysInConsumedFoods();

                for (int i = 0; i < dataPointCount - 1; i++)
                {
                    LPoint lPoint = new();
                    lPoint.Label = ((MealType)(i + 1)).ToString();
                    int totalQuantity1 = bl.ConsumedFoods.GetFoodConsumedTotalQuantityByFoodId(foodId, (MealType)(i + 1));
                    int totalPortionCount1 = bl.ConsumedFoods.GetFoodConsumedTotalPortionCountByFoodId(foodId, (MealType)(i + 1));
                    double dailyConsumeAmount = totalQuantity1 + totalPortionCount1;
                    lPoint.Y = Math.Round(dailyConsumeAmount / totalDay, 2);
                    pieDataFoodAvgDayConsume.DataPoints.Add(lPoint);
                }
                LPoint lPointLast = new();
                lPointLast.Label = "Others";
                int totalQuantity = bl.ConsumedFoods.GetFoodConsumedTotalQuantityByFoodId(foodId, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
                int totalPortionCount = bl.ConsumedFoods.GetFoodConsumedTotalPortionCountByFoodId(foodId, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
                double dailyConsumeAmount1 = totalQuantity + totalPortionCount;
                lPointLast.Y = Math.Round(dailyConsumeAmount1 / totalDay, 2);
                pieDataFoodAvgDayConsume.DataPoints.Add(lPointLast);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAll button.
        /// Fills the place and total information for all meal types.
        /// </summary>
        private void btnAll_Click(object sender, EventArgs e)
        {
            FillPlaceAndTotal(MealType.Breakfast, MealType.Lunch, MealType.Dinner, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
        }

        /// <summary>
        /// Handles the Click event of the btnBreakfast button.
        /// Fills the place and total information for the Breakfast meal type.
        /// </summary>
        private void btnBreakfast_Click(object sender, EventArgs e)
        {
            FillPlaceAndTotal(MealType.Breakfast);
        }

        /// <summary>
        /// Handles the Click event of the btnLunch button.
        /// Fills the place and total information for the Lunch meal type.
        /// </summary>
        private void btnLunch_Click(object sender, EventArgs e)
        {
            FillPlaceAndTotal(MealType.Lunch);
        }

        /// <summary>
        /// Handles the Click event of the btnDinner button.
        /// Fills the place and total information for the Dinner meal type.
        /// </summary>
        private void btnDinner_Click(object sender, EventArgs e)
        {
            FillPlaceAndTotal(MealType.Dinner);
        }

        /// <summary>
        /// Handles the Click event of the btnOthers button.
        /// Fills the place and total information for the Snack meal types.
        /// </summary>
        private void btnOthers_Click(object sender, EventArgs e)
        {
            FillPlaceAndTotal(MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
        }

        /// <summary>
        /// Handles the Click event of the btnDelete button.
        /// Deletes the selected food from the database.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to delete this food?  ", "Delete", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                bl.Foods.Remove(foodId);
                txtFoodName.Text = "";
                helper.ClearTextBoxes(pnlInfo.Controls);
                foodId = 0;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnNew button.
        /// Resets the form to create a new food entry.
        /// </summary>
        private void btnNew_Click(object sender, EventArgs e)
        {
            helper.ClearTextBoxes(pnlInfo.Controls);
            txtFoodName.Text = "";
            foodId = 0;
            txtFoodName.ReadOnly = false;
            txtCalorie.ReadOnly = false;
            txtPortion.ReadOnly = false;
            txtProtein.ReadOnly = false;
            txtFat.ReadOnly = false;
            txtCarbohydrate.ReadOnly = false;
            cbCategory.Text = "";
            cbCategory.SelectedIndex = -1;
            cbCategory.Enabled = true;
            btnSave.Enabled = true;
            btnEdit.Enabled = true;
            txtFoodName.Focus();
        }

        /// <summary>
        /// Handles the KeyPress event of the Texts (Guna2TextBox) controls.
        /// Allows only digits, comma, and backspace key to be entered as input.
        /// </summary>
        private void Texts_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the Leave event of the Texts (Guna2TextBox) controls.
        /// Checks if the textbox is empty or does not start and end with a digit.
        /// If so, sets the text value to "0".
        /// </summary>
        private void Texts_Leave(object sender, EventArgs e)
        {
            Guna2TextBox txtBox = (Guna2TextBox)sender;
            if (string.IsNullOrEmpty(txtBox.Text))
                return;

            if ((!char.IsDigit(txtBox.Text[0]) && !char.IsDigit(txtBox.Text[^1])))
            {
                txtBox.Text = "0";
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave button.
        /// Saves or updates the food information in the database based on the provided data.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Food food = bl.Foods.GetById(foodId);
            bool emptyControl = helper.AreTextBoxesEmpty(pnlInfo);
            if (string.IsNullOrWhiteSpace(txtFoodName.Text))
            {
                MessageBox.Show("Enter the name of the food.");
                return;
            }
            if (emptyControl)
            {
                MessageBox.Show("Fields cannot be left blank. Please enter your missing information.");
                return;
            }
            if (food != null)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to update this food?  ", "Update", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    food.Name = txtFoodName.Text;
                    food.CalorieFor100Gram = Convert.ToDecimal(txtCalorie.Text);
                    food.Category = (FoodCategory)(cbCategory.SelectedIndex + 1);
                    food.PortionGram = Convert.ToDecimal(txtPortion.Text);
                    food.ProteinRateFor100Gram = Convert.ToDecimal(txtProtein.Text);
                    food.FatRateFor100Gram = Convert.ToDecimal(txtFat.Text);
                    food.CarbonhydrateAmountFor100Gram = Convert.ToDecimal(txtCarbohydrate.Text);
                    food.OwnerId = userId;

                    if (imageData1 != null)
                        food.Image = imageData1;
                    bl.Foods.Update(food);
                }
            }
            else if (!bl.Foods.IsFoodExist(txtFoodName.Text))
            {
                DialogResult result = MessageBox.Show("Are you sure you want to add this food?  ", "Add", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    Food newFood = new Food();
                    newFood.Name = txtFoodName.Text;
                    newFood.CalorieFor100Gram = Convert.ToDecimal(txtCalorie.Text);
                    newFood.Category = (FoodCategory)(cbCategory.SelectedIndex + 1);
                    newFood.PortionGram = Convert.ToDecimal(txtPortion.Text);
                    newFood.ProteinRateFor100Gram = Convert.ToDecimal(txtProtein.Text);
                    newFood.FatRateFor100Gram = Convert.ToDecimal(txtFat.Text);
                    newFood.CarbonhydrateAmountFor100Gram = Convert.ToDecimal(txtCarbohydrate.Text);
                    newFood.OwnerId = userId;
                    if (imageData1 != null)
                        newFood.Image = imageData1;
                    bl.Foods.Add(newFood);
                    foodId = newFood.ID;
                }
            }
            else
                MessageBox.Show("There is a food with the same name! Please change the name.");

        }

        /// <summary>
        /// Handles the Click event of the btnEdit button.
        /// Allows the user to select and edit the food image by choosing a new image file.
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.Title = "Fotoğraf Seç";

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
                picboxFoodPicture.Image = resizedImage;
                picboxFoodPicture.SizeMode = PictureBoxSizeMode.CenterImage;
                imageData1 = File.ReadAllBytes(imagePath);
            }
        }


        #region Helper Methods
        /// <summary>
        /// Fills the place and total information for the specified meal types.
        /// Updates the corresponding labels and textboxes with the calculated values.
        /// </summary>
        /// <param name="mealTypes">The meal types for which to calculate the information.</param>
        public void FillPlaceAndTotal(params MealType[] mealTypes)
        {
            var allFoods = bl.Foods.GetAll();
            lblTotalFoodCount.Text = allFoods.Count.ToString();
            int totalQuantity = bl.ConsumedFoods.GetFoodConsumedTotalQuantityByFoodId(foodId, mealTypes);
            int totalPortionCount = bl.ConsumedFoods.GetFoodConsumedTotalPortionCountByFoodId(foodId, mealTypes);
            int totalCount = totalQuantity + totalPortionCount;
            bl.ConsumedFoods.GetFoodConsumedPlaceByFoodId(foodId, out int place, mealTypes);
            if (mealTypes.Count() == 1)
            {
                txtPlace.Text = place.ToString();
                txtQuantity.Text = totalQuantity.ToString();
                txtPortionCount.Text = totalPortionCount.ToString();
                txtTotal.Text = totalCount.ToString();
                lblMealType.Text = mealTypes[0].ToString();
            }
            else if (mealTypes.Count() < 6)
            {
                txtPlace.Text = place.ToString();
                txtQuantity.Text = totalQuantity.ToString();
                txtPortionCount.Text = totalPortionCount.ToString();
                txtTotal.Text = totalCount.ToString();
                lblMealType.Text = "Others";
            }
            else
            {
                txtPlace.Text = place.ToString();
                txtQuantity.Text = totalQuantity.ToString();
                txtPortionCount.Text = totalPortionCount.ToString();
                txtTotal.Text = totalCount.ToString();
                lblMealType.Text = "All";
            }
        }
        #endregion

    }
}

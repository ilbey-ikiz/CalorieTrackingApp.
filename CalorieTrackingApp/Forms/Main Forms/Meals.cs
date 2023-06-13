using BLL;
using Entities;
using Entities.Enums;
using Grup5_KaloriTakipProgrami;
using Guna.UI2.WinForms;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WndPL.Forms
{
    public partial class Meals : Form
    {
        public Meals(int id)
        {
            InitializeComponent();
            userID = id;
        }
        BusinessLogic bl;
        int foodID;
        int userID;
        MealType mealType;
        int count = 1;
        List<int> btnNumbers;
        int day;

        /// <summary>
        /// Handles the Load event of the Meals form.
        /// Initializes the business logic layer (bl) and retrieves all food items.
        /// Sets the default selected index for the cbxPortion ComboBox.
        /// Initializes the btnNumbers list.
        /// Sets the tbxFoodCalorie and tbxFoodName TextBox controls as read-only.
        /// Retrieves the user information and calculates the number of days since account creation.
        /// Initializes and sets the maximum and initial values for the calorie progress bars (cpbBreakfeastCalorie, cpbLunchCalorie, cpbDinnerCalorie, cpgTotalMealCalorie).
        /// Checks if there are any consumed foods for the selected snack meals and enables the corresponding snack button.
        /// </summary>
        private void Meals_Load(object sender, EventArgs e)
        {
            bl = new BusinessLogic();
            List<Food> foods = bl.Foods.GetAll();
            cbxPortion.SelectedIndex = 0;
            btnNumbers = new();
            tbxFoodCalorie.ReadOnly = true;
            tbxFoodName.ReadOnly = true;
            Entities.User user = bl.Users.GetById(userID);
            TimeSpan timePassed = DateTime.Now - user.CreationTime;
            day = (int)timePassed.TotalDays + 1;
            btnAddToMeal.Enabled = false;

            double totalCalorie1 = bl.Users.GetCaloriesForDailyMeal(userID, MealType.Breakfast); ;
            cpbBreakfeastCalorie.Maximum = 3000;
            cpbBreakfeastCalorie.Value = (int)totalCalorie1;

            double totalCalorie2 = bl.Users.GetCaloriesForDailyMeal(userID, MealType.Lunch); ;
            cpbLunchCalorie.Maximum = 3000;
            cpbLunchCalorie.Value = (int)totalCalorie2;

            double totalCalorie3 = bl.Users.GetCaloriesForDailyMeal(userID, MealType.Dinner); ;
            cpbDinnerCalorie.Maximum = 3000;
            cpbDinnerCalorie.Value = (int)totalCalorie3;

            cpgTotalMealCalorie.Maximum = 3000;

            MealType[] selectedMeals = new MealType[] { MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5 };

            foreach (var meal in selectedMeals)
            {
                if (bl.ConsumedFoods.GetConsumedFoodsByUserID(userID, day, meal))
                {
                    btnSnack.Enabled = true;
                    btnSnack.Visible = true;
                }
            }
        }

        /// <summary>
        /// Changes the activity of the button based on the incoming value.
        /// Enables or disables the btnAddToMeal button based on the control value.
        /// </summary>
        private void ButtonActivity(bool control)
        {
            if (control)
            {

                btnAddToMeal.Enabled = true;
            }
            else
            {

                btnAddToMeal.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the click event of the btnAddToMeal button.
        /// Adds the selected food to the meal.
        /// Retrieves the food information based on the foodID.
        /// Creates a new ListViewItem and populates its subitems with the food details and portion information.
        /// Adds the ListViewItem to the lviDailyConsumedFood ListView.
        /// Disables the btnAddToMeal button after adding the food to the meal.
        /// </summary>
        private void btnAddToMeal_Click(object sender, EventArgs e) //Adds selected food to Meal
        {
            if (tbxFoodName != null || tbxFoodCalorie != null)
            {
                Food food = bl.Foods.GetById(foodID);
                ListViewItem lvi = new();
                lvi.Text = food.Name;
                lvi.SubItems.Add(food.Category.ToString());

                decimal portionGramForType = 0;

                if (cbxPortion.SelectedIndex >= 0 && cbxPortion.SelectedIndex <= 2)
                {
                    if (cbxPortion.SelectedIndex == 0)  //Full
                    {
                        portionGramForType = food.PortionGram / 1;
                    }
                    else if (cbxPortion.SelectedIndex == 1)  //Half 
                    {
                        portionGramForType = food.PortionGram / 2;
                    }
                    else if (cbxPortion.SelectedIndex == 2)  //Quartar
                    {
                        portionGramForType = food.PortionGram / 4;
                    }

                    lvi.SubItems.Add(cbxPortion.SelectedItem.ToString());
                    lvi.SubItems.Add(nudAmount.Value.ToString());
                    lvi.SubItems.Add(portionGramForType.ToString());
                    lvi.SubItems.Add(((food.CalorieFor100Gram * portionGramForType) / 100).ToString());
                    lvi.SubItems.Add(((food.ProteinRateFor100Gram * portionGramForType) / 100).ToString());
                    lvi.SubItems.Add(((food.FatRateFor100Gram * portionGramForType) / 100).ToString());
                    lvi.SubItems.Add(((food.CarbonhydrateAmountFor100Gram * portionGramForType) / 100).ToString());
                }
                else if (cbxPortion.SelectedIndex == 3)  //100Gram
                {
                    portionGramForType = 100 * nudAmount.Value;

                    lvi.SubItems.Add(cbxPortion.SelectedItem.ToString());
                    lvi.SubItems.Add(nudAmount.Value.ToString());
                    lvi.SubItems.Add(portionGramForType.ToString());
                    lvi.SubItems.Add((food.CalorieFor100Gram * nudAmount.Value).ToString());
                    lvi.SubItems.Add((food.ProteinRateFor100Gram * nudAmount.Value).ToString());
                    lvi.SubItems.Add((food.FatRateFor100Gram * nudAmount.Value).ToString());
                    lvi.SubItems.Add((food.CarbonhydrateAmountFor100Gram * nudAmount.Value).ToString());
                }

                lviDailyConsumedFood.Items.Add(lvi);
                ButtonActivity(false);

            }
            else
            {
                ButtonActivity(false);
            }
        }

        /// <summary>
        /// Fills the lviDailyConsumedFood ListView with the consumed food items for the specified meal.
        /// Clears the existing items in the ListView.
        /// Retrieves the consumed food items for the given user ID and meal type from the database.
        /// Calculates the total calorie value for the consumed food items.
        /// Iterates through each consumed food item and creates a new ListViewItem.
        /// Populates the subitems of the ListViewItem with the food details and portion information.
        /// Adds the ListViewItem to the lviDailyConsumedFood ListView.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="meal">The type of meal (Breakfast, Lunch, Dinner, Snack1, Snack2, Snack3, Snack4, Snack5).</param>
        private void FillListViewConsumedFood(int id, MealType meal)//Fills the consumedfood list view for incoming meal
        {

            lviDailyConsumedFood.Items.Clear();
            List<ConsumedFood> consumedFoods = bl.ConsumedFoods.GetConsumedFoodsByDayAndMealType(id, meal);
            double totalCalorie = 0;
            foreach (ConsumedFood consumed in consumedFoods)
            {
                ListViewItem lvi = new ListViewItem();
                int foodId = consumed.FoodId;
                Food food = bl.Foods.GetById(foodId);
                lvi.Text = food.Name;//food name
                lvi.SubItems.Add(food.Category.ToString());//category
                if (consumed.Quantity > 0)
                {
                    lvi.SubItems.Add("100Gram");
                    lvi.SubItems.Add(consumed.Quantity.ToString());
                    lvi.SubItems.Add((100 * consumed.Quantity).ToString());
                    lvi.SubItems.Add((food.CalorieFor100Gram * consumed.Quantity).ToString());
                    lvi.SubItems.Add((food.ProteinRateFor100Gram * consumed.Quantity).ToString());
                    lvi.SubItems.Add((food.FatRateFor100Gram * consumed.Quantity).ToString());
                    lvi.SubItems.Add((food.CarbonhydrateAmountFor100Gram * consumed.Quantity).ToString());
                }
                else
                {
                    lvi.SubItems.Add(consumed.PortionType.ToString());
                    lvi.SubItems.Add(consumed.PortionCount.ToString());
                    double portionGramForType = 0;

                    if (consumed.PortionType == PortionType.Full)
                    {
                        totalCalorie = consumed.PortionCount * (double)(food.PortionGram / 100) * (double)food.CalorieFor100Gram / (int)PortionType.Full;
                        portionGramForType = (double)food.PortionGram / (int)PortionType.Full;


                    }
                    else if (consumed.PortionType == PortionType.Half)
                    {
                        totalCalorie = consumed.PortionCount * (double)(food.PortionGram / 100) * (double)food.CalorieFor100Gram / (int)PortionType.Half;
                        portionGramForType = (double)food.PortionGram / (int)PortionType.Half;

                    }
                    else if (consumed.PortionType == PortionType.Quarter)
                    {
                        totalCalorie = consumed.PortionCount * (double)(food.PortionGram / 100) * (double)food.CalorieFor100Gram / (int)PortionType.Quarter;
                        portionGramForType = (double)food.PortionGram / (int)PortionType.Quarter;

                    }

                    lvi.SubItems.Add(Math.Round(portionGramForType, 2).ToString());
                    lvi.SubItems.Add(Math.Round(totalCalorie, 2).ToString());
                    lvi.SubItems.Add(Math.Round((((double)food.ProteinRateFor100Gram * portionGramForType) / 100), 2).ToString());
                    lvi.SubItems.Add(Math.Round((((double)food.FatRateFor100Gram * portionGramForType) / 100), 2).ToString());
                    lvi.SubItems.Add(Math.Round((((double)food.CarbonhydrateAmountFor100Gram * portionGramForType) / 100), 2).ToString());

                }
                lvi.Tag = consumed.ID;
                lviDailyConsumedFood.Items.Add(lvi);
            }
        }

        //Fills the Meal listview for breakfeast and calculates the breakfeast calorie
        private void btnBreakFeast_Click(object sender, EventArgs e)
        {
            mealType = MealType.Breakfast;
            FillListViewConsumedFood(userID, mealType);
            ChangeStyleSelectedButton(btnBreakFeast);
        }

        private void btnLunch_Click(object sender, EventArgs e)
        {
            mealType = MealType.Lunch;
            FillListViewConsumedFood(userID, mealType);
            ChangeStyleSelectedButton(btnLunch);

        }

        private void btnDinner_Click(object sender, EventArgs e)
        {
            mealType = MealType.Dinner;
            FillListViewConsumedFood(userID, mealType);
            ChangeStyleSelectedButton(btnDinner);

        }

        /// <summary>
        /// Saves the selected meal by iterating through the list of consumed foods.
        /// </summary>
        private void btnSaveSelectedMeal_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lviDailyConsumedFood.Items.Count; i++)
            {
                if (lviDailyConsumedFood.Items[i].Tag == null)
                {
                    ConsumedFood consumed = new ConsumedFood()
                    {
                        MealType = mealType,
                        Day = day,
                        UserId = userID
                    };

                    int foodId = bl.Foods.GetFoodIdByFoodName(lviDailyConsumedFood.Items[i].SubItems[0].Text);
                    consumed.FoodId = foodId;

                    if (lviDailyConsumedFood.Items[i].SubItems[2].Text == "100Gram")
                    {
                        consumed.Quantity = Convert.ToInt32(lviDailyConsumedFood.Items[i].SubItems[3].Text);
                    }
                    else
                    {
                        consumed.PortionType = (PortionType)Enum.Parse(typeof(PortionType), lviDailyConsumedFood.Items[i].SubItems[2].Text);
                        consumed.PortionCount = Convert.ToInt32(lviDailyConsumedFood.Items[i].SubItems[3].Text);
                    }

                    bool isAdded = bl.ConsumedFoods.Add(consumed);

                    if (isAdded)
                    {
                        MessageBox.Show("The meal is saved");

                        double totalCalorie = CalculateTotalCalorie();
                        UpdateCalorieProgressBar(mealType, totalCalorie);
                    }
                    else
                    {
                        MessageBox.Show("The meal is not saved. Something went wrong.");
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the selected meal, removes the consumed foods associated with it, and updates the calorie progress bar.
        /// </summary>
        private void btnDeleteSelectedMeal_Click(object sender, EventArgs e)
        {
            if (mealType == 0)
            {
                MessageBox.Show("Please select a meal before deleting");
            }
            else
            {
                if (mealType.ToString() != "Breakfast" || mealType.ToString() != "Lunch" || mealType.ToString() != "Dinner")
                {
                    // Hide and disable the snack button associated with the meal type
                    var snackBtnList = new List<Guna2Button>() { btnSnack, btnSnack2, btnSnack3, btnSnack4, btnSnack5 };
                    foreach (var item in snackBtnList)
                    {
                        if (mealType.ToString() == item.Text)
                        {
                            item.Visible = false;
                            item.Enabled = false;
                            count--;
                            btnNumbers.Remove(snackBtnList.IndexOf(item) + 1);
                        }
                    }
                }

                // Remove the consumed foods for the selected meal type
                for (int i = 0; i < lviDailyConsumedFood.Items.Count; i++)
                {
                    if (lviDailyConsumedFood.Items[i].Tag != null)
                    {
                        int id = (int)lviDailyConsumedFood.Items[i].Tag;
                        ConsumedFood consumed = bl.ConsumedFoods.GetById(id);
                        Food food = bl.Foods.GetById(consumed.FoodId);
                        bl.ConsumedFoods.Remove(id);
                    }
                }

                MessageBox.Show($"{mealType} deleted from your meal");
                UpdateCalorieProgressBar(mealType, 0);
                lviDailyConsumedFood.Items.Clear();
            }
        }

        /// <summary>
        /// Adds a snack button to the user interface if there are available slots.
        /// </summary>
        private void btnAddSnack_Click(object sender, EventArgs e)
        {

            var snackBtnList = new List<Guna2Button>() { btnSnack, btnSnack2, btnSnack3, btnSnack4, btnSnack5 };
            if (count <= 5)
            {
                foreach (var item in snackBtnList)
                {
                    if (!btnNumbers.Contains(snackBtnList.IndexOf(item) + 1))
                    {
                        item.Enabled = true;
                        item.Visible = true;
                        count++;
                        btnNumbers.Add(snackBtnList.IndexOf(item) + 1);
                        if (item.Enabled == true)
                        {
                            break;
                        }
                    }
                }
            }
            else MessageBox.Show("You can not add more than 5 Snack");
        }

        /// <summary>
        /// Handles the ValueChanged event of the nudAmount control. 
        /// Updates the activity of a button based on the selected amount, food details, and meal type.
        /// </summary>
        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            if (nudAmount.Value == 0 || tbxFoodCalorie.Text == string.Empty || tbxFoodName.Text == string.Empty || mealType == 0)
            {
                ButtonActivity(false);
            }
            else
            {
                ButtonActivity(true);
            }
        }

        /// <summary>
        /// Handles the Click event of the lblFoodSearch label. 
        /// Hides the label and sets focus to the txtFoodSearch textbox.
        /// </summary>
        private void lblFoodSearch_Click(object sender, EventArgs e)
        {
            lblFoodSearch.Hide();
            txtFoodSearch.Focus();
        }

        /// <summary>
        /// Handles the TextChanged event of the txtFoodSearch textbox. 
        /// Clears the list view, searches for foods containing the entered text for the current user, and displays the search results in the list view.
        /// </summary>
        private void txtFoodSearch_TextChanged(object sender, EventArgs e)
        {
            lvSearchedFoods.Items.Clear();
            List<Food> searchedFoods = bl.Foods.GetFoodsByContainTextAndOwnerId(txtFoodSearch.Text.Trim(), userID);
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
        /// Handles the MouseEnter event of the txtFoodSearch textbox. 
        /// Hides the lblFoodSearch label when the mouse enters the textbox.
        /// </summary>
        private void txtFoodSearch_MouseEnter(object sender, EventArgs e)
        {
            lblFoodSearch.Hide();
        }

        /// <summary>
        /// Handles the MouseLeave event of the txtFoodSearch textbox. 
        /// Shows the lblFoodSearch label when the mouse leaves the textbox.
        /// </summary>
        private void txtFoodSearch_MouseLeave(object sender, EventArgs e)
        {
            lblFoodSearch.Show();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lvSearchedFoods ListView.
        /// Hides the lvSearchedFoods ListView and updates the UI with the selected food's details.
        /// If the food is selected, it retrieves the food information from the business logic layer (bl.Foods).
        /// Sets the text boxes (tbxFoodName and tbxFoodCalorie) with the selected food's name and calorie information.
        /// Updates the foodID variable with the selected food's ID.
        /// If the amount value is greater than zero and a meal type is selected, enables the button activity.
        /// </summary>
        private void lvSearchedFoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvSearchedFoods.Hide();
            if (lvSearchedFoods.SelectedItems.Count > 0)
            {
                int foodId = (int)lvSearchedFoods.SelectedItems[0].Tag;
                Food food = bl.Foods.GetById(foodId);
                tbxFoodName.Text = food.Name;
                tbxFoodCalorie.Text = food.CalorieFor100Gram.ToString();
                foodID = (int)lvSearchedFoods.SelectedItems[0].Tag;
                if (nudAmount.Value > 0 && mealType != 0)
                {
                    ButtonActivity(true);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the deleteToolStripMenuItem.
        /// Deletes the selected item from the lviDailyConsumedFood ListView.
        /// If the selected item has a tag (non-null), it removes the corresponding consumed food entry from the business logic layer (bl.ConsumedFoods).
        /// After deletion, it updates the lviDailyConsumedFood ListView, recalculates the total calorie, and updates the calorie progress bar.
        /// If the selected item doesn't have a tag (null), it removes the item directly from the lviDailyConsumedFood ListView.
        /// </summary>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lviDailyConsumedFood.SelectedItems.Count > 0)
            {
                if (lviDailyConsumedFood.SelectedItems[0].Tag != null)
                {
                    int consumedId = (int)lviDailyConsumedFood.SelectedItems[0].Tag;
                    bl.ConsumedFoods.Remove(consumedId);
                    FillListViewConsumedFood(userID, mealType);
                    double totalCalorie = CalculateTotalCalorie();
                    UpdateCalorieProgressBar(mealType, totalCalorie);
                }
                else
                {
                    lviDailyConsumedFood.SelectedItems[0].Remove();
                }
            }
        }

        private void btnSnack_Click(object sender, EventArgs e)
        {
            mealType = MealType.Snack1;
            FillListViewConsumedFood(userID, mealType);
            ChangeStyleSelectedButton(btnSnack);
        }

        private void btnSnack2_Click(object sender, EventArgs e)
        {
            mealType = MealType.Snack2;
            FillListViewConsumedFood(userID, mealType);
            ChangeStyleSelectedButton(btnSnack2);
        }

        private void btnSnack3_Click(object sender, EventArgs e)
        {
            mealType = MealType.Snack3;
            FillListViewConsumedFood(userID, mealType);
            ChangeStyleSelectedButton(btnSnack3);
        }

        private void btnSnack4_Click(object sender, EventArgs e)
        {
            mealType = MealType.Snack4;
            FillListViewConsumedFood(userID, mealType);
            ChangeStyleSelectedButton(btnSnack4);
        }

        private void btnSnack5_Click(object sender, EventArgs e)
        {
            mealType = MealType.Snack5;
            FillListViewConsumedFood(userID, mealType);
            ChangeStyleSelectedButton(btnSnack5);
        }

        /// <summary>
        /// Changes the style of the selected button to indicate the active meal.
        /// Updates the border thickness, color, and radius of the selected button.
        /// Updates the meal label to display the selected meal.
        /// Updates the total meal calorie progress bar.
        /// </summary>
        /// <param name="btn">The selected button to change the style.</param>
        public void ChangeStyleSelectedButton(Guna2Button btn)
        {
            List<Guna2Button> btnList = new List<Guna2Button>() { btnBreakFeast, btnLunch, btnDinner, btnSnack, btnSnack2, btnSnack3, btnSnack4, btnSnack5 };

            foreach (var button in btnList)
            {
                if (button == btn)
                {
                    btn.BorderThickness = 3;
                    btn.BorderColor = Color.Red;
                    btn.BorderRadius = 15;
                    lblMeal.Text = $"Selected Meal - {btn.Text}";
                    cpgTotalMealCalorie.Value = (int)CalculateTotalCalorie();
                }
                else
                {
                    button.BorderThickness = 0;
                }
            }
        }

        /// <summary>
        /// Calculates the total calorie by summing up the calorie values of all items in the lviDailyConsumedFood ListView.
        /// It iterates through each ListViewItem in the ListView and retrieves the calorie value from the corresponding subitem.
        /// The calculated total calorie is returned as a double value.
        /// </summary>
        /// <returns>The total calorie value as a double.</returns>
        double CalculateTotalCalorie()
        {
            double totalCalorie = 0;
            foreach (ListViewItem item in lviDailyConsumedFood.Items)
            {
                totalCalorie += Convert.ToDouble(item.SubItems[5].Text);
            }
            return totalCalorie;
        }

        /// <summary>
        /// Updates the calorie progress bar for the specified meal type with the provided total calorie value.
        /// The method takes the mealType and totalCalorie as parameters.
        /// Based on the meal type, it updates the corresponding calorie progress bar value.
        /// </summary>
        /// <param name="mealType">The type of meal (Breakfast, Lunch, or Dinner).</param>
        /// <param name="totalCalorie">The total calorie value to update the progress bar.</param>
        void UpdateCalorieProgressBar(MealType mealType, double totalCalorie)
        {
            switch (mealType)
            {
                case MealType.Breakfast:
                    cpbBreakfeastCalorie.Value = (int)totalCalorie;
                    break;
                case MealType.Lunch:
                    cpbLunchCalorie.Value = (int)totalCalorie;
                    break;
                case MealType.Dinner:
                    cpbDinnerCalorie.Value = (int)totalCalorie;
                    break;
            }
        }




    }
}

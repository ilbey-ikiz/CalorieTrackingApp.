using BLL;
using Castle.DynamicProxy.Generators;
using Entities;
using Entities.Enums;
using Guna.Charts.WinForms;
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

namespace WndPL.Forms.ReportForms
{
    public partial class MealsReports : Form
    {
        public MealsReports(int id)
        {
            InitializeComponent();
            userId = id;
        }
        BusinessLogic bl = new BusinessLogic();
        int userId;

        /// <summary>
        /// Loads the MealsReports form and fills the initial data.
        /// </summary>
        private void MealsReports_Load(object sender, EventArgs e)
        {
            FillFoodCategoryDatas(7, MealType.Breakfast, MealType.Lunch, MealType.Dinner, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
            FillCalorieData(7);
            lblMealType.Text = "All";
        }
        /// <summary>
        /// Handles the checked changed event of the tsWeekMonth toggle switch. Updates the data based on the selected time period.
        /// </summary>
        private void tsWeekMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (!tsWeekMonth.Checked)
            {
                FillCalorieData(7);
                if (lblMealType.Text == MealType.Breakfast.ToString())
                    FillFoodCategoryDatas(7, MealType.Breakfast);
                else if (lblMealType.Text == MealType.Lunch.ToString())
                    FillFoodCategoryDatas(7, MealType.Lunch);
                else if (lblMealType.Text == MealType.Dinner.ToString())
                    FillFoodCategoryDatas(7, MealType.Dinner);
                else
                    FillFoodCategoryDatas(7, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
                ChangeColors(Color.Teal);
            }
            else
            {
                FillCalorieData(30);
                if (lblMealType.Text == MealType.Breakfast.ToString())
                    FillFoodCategoryDatas(30, MealType.Breakfast);
                else if (lblMealType.Text == MealType.Lunch.ToString())
                    FillFoodCategoryDatas(30, MealType.Lunch);
                else if (lblMealType.Text == MealType.Dinner.ToString())
                    FillFoodCategoryDatas(30, MealType.Dinner);
                else
                    FillFoodCategoryDatas(30, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);

                ChangeColors(Color.MediumPurple);
            }
        }

        /// <summary>
        /// Handles the click event of the "All" button. Sets the meal type to "All" and fills the food category and calorie data based on the selected time period.
        /// </summary>
        private void btnAll_Click(object sender, EventArgs e)
        {
            lblMealType.Text = "All";
            if (!tsWeekMonth.Checked)
            {
                FillFoodCategoryDatas(7, MealType.Breakfast, MealType.Lunch, MealType.Dinner, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
                FillCalorieData(7);
            }
            else
            {
                FillFoodCategoryDatas(30, MealType.Breakfast,MealType.Lunch,MealType.Dinner, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
                FillCalorieData(30);
            }
        }

        /// <summary>
        /// Handles the click event of the "Breakfast" button. Sets the meal type to "Breakfast" and fills the food category and calorie data based on the selected time period.
        /// </summary>
        private void btnBreakfast_Click(object sender, EventArgs e)
        {
            lblMealType.Text = MealType.Breakfast.ToString();
            if (!tsWeekMonth.Checked)
            {
                FillFoodCategoryDatas(7, MealType.Breakfast);
                FillCalorieData(7, MealType.Breakfast);
            }
            else
            {
                FillFoodCategoryDatas(30, MealType.Breakfast);
                FillCalorieData(30, MealType.Breakfast);
            }
        }

        /// <summary>
        /// Handles the click event of the "Lunch" button. Sets the meal type to "Lunch" and fills the food category and calorie data based on the selected time period.
        /// </summary>
        private void btnLunch_Click(object sender, EventArgs e)
        {
            lblMealType.Text = MealType.Lunch.ToString();
            if (!tsWeekMonth.Checked)
            {
                FillFoodCategoryDatas(7, MealType.Lunch);
                FillCalorieData(7, MealType.Lunch);
            }
            else
            {
                FillFoodCategoryDatas(30, MealType.Lunch);
                FillCalorieData(30, MealType.Lunch);
            }
        }

        /// <summary>
        /// Handles the click event of the "Dinner" button. Sets the meal type to "Dinner" and fills the food category and calorie data based on the selected time period.
        /// </summary>
        private void btnDinner_Click(object sender, EventArgs e)
        {
            lblMealType.Text = MealType.Dinner.ToString();
            if (!tsWeekMonth.Checked)
            {
                FillFoodCategoryDatas(7, MealType.Dinner);
                FillCalorieData(7, MealType.Dinner);
            }
            else
            {
                FillFoodCategoryDatas(30, MealType.Dinner);
                FillCalorieData(30, MealType.Dinner);
            }
        }

        /// <summary>
        /// Handles the click event of the "Others" button. Sets the meal type to "Others" and fills the food category and calorie data based on the selected time period.
        /// </summary>
        private void btnOthers_Click(object sender, EventArgs e)
        {
            lblMealType.Text = "Others";
            if (!tsWeekMonth.Checked)
            {
                FillFoodCategoryDatas(7, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
                FillCalorieData(7, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
            }

            else
            {
                FillFoodCategoryDatas(30, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
                FillCalorieData(30, MealType.Snack1, MealType.Snack2, MealType.Snack3, MealType.Snack4, MealType.Snack5);
            }
        }


        #region Helper Methods

        /// <summary>
        /// Fills the data for food category bar chart.
        /// </summary>
        /// <param name="day">Number of days to consider for data retrieval.</param>
        /// <param name="mealTypes">Meal types to consider for data retrieval.</param>
        public void FillFoodCategoryDatas(int day, params MealType[] mealTypes)
        {
            //Data1
            barDataFoodCategoryMy1.DataPoints.Clear();
            for (int i = 0; i < Enum.GetValues(typeof(FoodCategory)).Length / 2; i++)
            {
                LPoint lPoint = new LPoint();
                lPoint.Label = ((FoodCategory)(i + 1)).ToString();
                lPoint.Y = bl.Users.GetConsumedFoodsAmountForDaysById(userId, day, (FoodCategory)(i + 1), mealTypes);
                barDataFoodCategoryMy1.DataPoints.Add(lPoint);
            }

            barDataFoodCategoryUsers1.DataPoints.Clear();
            for (int i = 0; i < Enum.GetValues(typeof(FoodCategory)).Length / 2; i++)
            {
                LPoint lPoint = new LPoint();
                lPoint.Label = ((FoodCategory)(i + 1)).ToString();
                var users = bl.Users.GetAll();
                int usersTotal = 0;
                foreach (var user in users)
                {
                    usersTotal += bl.Users.GetConsumedFoodsAmountForDaysById(user.ID, day, (FoodCategory)(i + 1), mealTypes);
                }
                lPoint.Y = usersTotal / users.Count;
                barDataFoodCategoryUsers1.DataPoints.Add(lPoint);
            }

            //Data2
            barDataFoodCategoryMy2.DataPoints.Clear();
            for (int i = 0; i < Enum.GetValues(typeof(FoodCategory)).Length / 2; i++)
            {
                LPoint lPoint = new LPoint();
                lPoint.Label = ((FoodCategory)((Enum.GetValues(typeof(FoodCategory)).Length / 2) + i + 1)).ToString();
                lPoint.Y = bl.Users.GetConsumedFoodsAmountForDaysById(userId, day, (FoodCategory)((Enum.GetValues(typeof(FoodCategory)).Length / 2) + i + 1), mealTypes);
                barDataFoodCategoryMy2.DataPoints.Add(lPoint);
            }

            barDataFoodCategoryUsers2.DataPoints.Clear();
            for (int i = 0; i < Enum.GetValues(typeof(FoodCategory)).Length / 2; i++)
            {
                LPoint lPoint = new LPoint();
                lPoint.Label = ((FoodCategory)((Enum.GetValues(typeof(FoodCategory)).Length / 2) + i + 1)).ToString();
                var users = bl.Users.GetAll();
                int usersTotal = 0;
                foreach (var user in users)
                {
                    usersTotal += bl.Users.GetConsumedFoodsAmountForDaysById(user.ID, day, (FoodCategory)((Enum.GetValues(typeof(FoodCategory)).Length / 2) + i + 1), mealTypes);

                }
                lPoint.Y = usersTotal / users.Count;
                barDataFoodCategoryUsers2.DataPoints.Add(lPoint);
            }
        }

        /// <summary>
        /// Fills the data for calorie bar chart.
        /// </summary>
        /// <param name="day">Number of days to consider for data retrieval.</param>
        /// <param name="mealTypes">Meal types to consider for data retrieval.</param>
        public void FillCalorieData(int day, params MealType[] mealTypes)
        {
            LPoint lpMyAvgCalorie = new LPoint();
            LPoint lpUsersAvgCalorie = new LPoint();
            //Data1
            barDataCalories1.DataPoints.Clear();
            double totalMyCalorieForDays = bl.Users.GetCaloriesForDaysById(userId, day, out int emptyDays1, mealTypes);
            lpMyAvgCalorie.Y = Math.Round(totalMyCalorieForDays / (day - emptyDays1), 2);
            barDataCalories1.DataPoints.Add(lpMyAvgCalorie);

            //Data2
            barDataCalories2.DataPoints.Clear();
            double totalUsersCalorieForDays = 0;
            var users = bl.Users.GetAll();
            int userCount = users.Count;
            int emptyusersDays = 0;
            foreach (var user in users)
            {
                totalUsersCalorieForDays += bl.Users.GetCaloriesForDaysById(user.ID, day, out int emptyDays2, mealTypes);
                emptyusersDays += emptyDays2;
            }
            lpUsersAvgCalorie.Y = Math.Round(totalUsersCalorieForDays / (day - emptyusersDays/userCount) / userCount, 2);
            barDataCalories2.DataPoints.Add(lpUsersAvgCalorie);
        }

        /// <summary>
        /// Changes the colors of UI elements based on the given color.
        /// </summary>
        /// <param name="color">Color to apply to UI elements.</param>
        private void ChangeColors(Color color)
        {
            lblMealType.ForeColor = color;
            btnAll.FillColor = color;
            btnBreakfast.FillColor = color;
            btnDinner.FillColor = color;
            btnLunch.FillColor = color;
            btnOthers.FillColor = color;
            pnlStick.FillColor = color;
            pnlStick.FillColor2 = color;
            chartCalorie.Title.ForeColor = color;
        }
        #endregion


    }
}

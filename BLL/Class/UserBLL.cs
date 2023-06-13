using Castle.DynamicProxy.Generators;
using DAL;
using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using ActivityFrequency = Entities.Enums.ActivityFrequency;

namespace BLL
{
    public class UserBLL : BaseClass<User>
    {
        /// <summary>
        /// Calculates the daily calorie intake for a user based on consumed foods.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>The total daily calorie intake for the user</returns>
        public double GetDailyCalorieById(int id)
        {
            User? user = db.Users.Find(id);
            TimeSpan timePassed = DateTime.Now - user.CreationTime;
            int day = (int)timePassed.TotalDays + 1;
            var cfList = user.ConsumedFoods.Where(cf => cf.Day == day).ToList();
            double dailyCalorie = 0;

            foreach (var cf in cfList)
            {
                if (cf.Quantity > 0)
                    dailyCalorie += cf.Quantity * (double)cf.Food.CalorieFor100Gram;
                else if (cf.PortionCount > 0)
                {
                    if (cf.PortionType == PortionType.Full)
                        dailyCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Full;
                    else if (cf.PortionType == PortionType.Half)
                        dailyCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Half;
                    else if (cf.PortionType == PortionType.Quarter)
                        dailyCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Quarter;
                }
            }

            return dailyCalorie;
        }

        /// <summary>
        /// Calculates the total calorie intake for a user over a specified number of days, considering the specified meal types.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="howManyDaysBeforeToday">Number of days to consider</param>
        /// <param name="emptyDays">Number of days where no meals were consumed</param>
        /// <param name="mealTypes">Meal types to consider</param>
        /// <returns>Total calorie intake for the user</returns>
        public double GetCaloriesForDaysById(int id, int howManyDaysBeforeToday, out int emptyDays, MealType[] mealTypes)
        {
            User? user = db.Users.Find(id);
            TimeSpan timePassed = DateTime.Now - user.CreationTime;
            double totalCalorie = 0;
            emptyDays = 0;
            if (mealTypes.Count() == 1)
            {
                // Calculation for a single meal type
                for (int i = 0; i < howManyDaysBeforeToday; i++)
                {
                    int day = (int)timePassed.TotalDays - i + 1;
                    var cfList = user.ConsumedFoods.Where(cf => cf.Day == day && cf.MealType == mealTypes[0]).ToList();
                    if (cfList.Count == 0 && (int)timePassed.TotalDays >= howManyDaysBeforeToday)
                        emptyDays++;
                    foreach (var cf in cfList)
                    {
                        // Calorie calculation based on quantity or portion count
                        if (cf.Quantity > 0)
                            totalCalorie += cf.Quantity * (double)cf.Food.CalorieFor100Gram;
                        else if (cf.PortionCount > 0)
                        {
                            if (cf.PortionType == PortionType.Full)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Full;
                            else if (cf.PortionType == PortionType.Half)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Half;
                            else if (cf.PortionType == PortionType.Quarter)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Quarter;
                        }
                    }
                }
            }
            else if (mealTypes.Count() < 6 && mealTypes.Count() != 0)
            {
                // Calculation for multiple meal types excluding Breakfast, Lunch, and Dinner
                for (int i = 0; i < howManyDaysBeforeToday; i++)
                {
                    int day = (int)timePassed.TotalDays - i + 1;
                    var cfList = user.ConsumedFoods.Where(cf => cf.Day == day && (cf.MealType != MealType.Breakfast && cf.MealType != MealType.Lunch && cf.MealType != MealType.Dinner)).ToList();
                    if (cfList.Count == 0 && (int)timePassed.TotalDays >= howManyDaysBeforeToday)
                        emptyDays++;
                    foreach (var cf in cfList)
                    {
                        if (cf.Quantity > 0)
                            totalCalorie += cf.Quantity * (double)cf.Food.CalorieFor100Gram;
                        else if (cf.PortionCount > 0)
                        {
                            if (cf.PortionType == PortionType.Full)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Full;
                            else if (cf.PortionType == PortionType.Half)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Half;
                            else if (cf.PortionType == PortionType.Quarter)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Quarter;
                        }
                    }
                }
            }
            else
            {
                // Calculation for all meal types
                for (int i = 0; i < howManyDaysBeforeToday; i++)
                {
                    int day = (int)timePassed.TotalDays - i + 1;
                    var cfList = user.ConsumedFoods.Where(cf => cf.Day == day).ToList();
                    if (cfList.Count == 0 && (int)timePassed.TotalDays >= howManyDaysBeforeToday)
                        emptyDays++;
                    foreach (var cf in cfList)
                    {
                        if (cf.Quantity > 0)
                            totalCalorie += cf.Quantity * (double)cf.Food.CalorieFor100Gram;
                        else if (cf.PortionCount > 0)
                        {
                            if (cf.PortionType == PortionType.Full)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Full;
                            else if (cf.PortionType == PortionType.Half)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Half;
                            else if (cf.PortionType == PortionType.Quarter)
                                totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Quarter;
                        }
                    }
                }

            }

            return totalCalorie;
        }

        /// <summary>
        /// Calculates the daily nutrient percentages (protein, fat, and carbohydrates) for a user based on their consumed foods.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="dailyProteinGram">Output parameter for the daily protein intake in grams</param>
        /// <param name="dailyFatGram">Output parameter for the daily fat intake in grams</param>
        /// <param name="dailyCarbohydrateGram">Output parameter for the daily carbohydrate intake in grams</param>
        public void GetDailyNutrientsPercentageById(int id, out double dailyProteinGram, out double dailyFatGram, out double dailyCarbohydrateGram)
        {
            dailyProteinGram = 0;
            dailyFatGram = 0;
            dailyCarbohydrateGram = 0;
            User? user = db.Users.Find(id);
            TimeSpan timePassed = DateTime.Now - user.CreationTime;
            int day = (int)timePassed.TotalDays + 1;
            var cfList = user.ConsumedFoods.Where(cf => cf.Day == day).ToList();

            foreach (var cf in cfList)
            {
                if (cf.Quantity > 0)
                {
                    // Calculate nutrient grams based on quantity
                    dailyProteinGram += cf.Quantity * (double)cf.Food.ProteinRateFor100Gram;
                    dailyFatGram += cf.Quantity * (double)cf.Food.FatRateFor100Gram;
                    dailyCarbohydrateGram += cf.Quantity * (double)cf.Food.CarbonhydrateAmountFor100Gram;
                }
                else if (cf.PortionCount > 0)
                {
                    // Calculate nutrient grams based on portion count and type
                    if (cf.PortionType == PortionType.Full)
                    {
                        dailyProteinGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.ProteinRateFor100Gram / (int)PortionType.Full;
                        dailyFatGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.FatRateFor100Gram / (int)PortionType.Full;
                        dailyCarbohydrateGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CarbonhydrateAmountFor100Gram / (int)PortionType.Full;

                    }
                    else if (cf.PortionType == PortionType.Half)
                    {
                        dailyProteinGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.ProteinRateFor100Gram / (int)PortionType.Half;
                        dailyFatGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.FatRateFor100Gram / (int)PortionType.Half;
                        dailyCarbohydrateGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CarbonhydrateAmountFor100Gram / (int)PortionType.Half;
                    }
                    else if (cf.PortionType == PortionType.Quarter)
                    {
                        dailyProteinGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.ProteinRateFor100Gram / (int)PortionType.Quarter;
                        dailyFatGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.FatRateFor100Gram / (int)PortionType.Quarter;
                        dailyCarbohydrateGram += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CarbonhydrateAmountFor100Gram / (int)PortionType.Quarter;
                    }

                }
            }
        }

        /// <summary>
        /// Calculates the total calorie intake for a specific day based on the user's consumed foods.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="howManyDaysBeforeToday">Number of days before today to calculate the calorie intake</param>
        /// <returns>Total calorie intake for the specific day</returns>
        public double GetSpesificDayCaloriesById(int id, int howManyDaysBeforeToday)
        {
            User? user = db.Users.Find(id);
            TimeSpan timePassed = DateTime.Now - user.CreationTime;
            int day = (int)timePassed.TotalDays - howManyDaysBeforeToday;
            var cfList = user.ConsumedFoods.Where(cf => cf.Day == day).ToList();
            double dayCalorie = 0;
            foreach (var cf in cfList)
            {
                if (cf.Quantity > 0)
                    // Calculate calorie intake based on quantity
                    dayCalorie += cf.Quantity * (double)cf.Food.CalorieFor100Gram;
                else if (cf.PortionCount > 0)
                {
                    // Calculate calorie intake based on portion count and type
                    if (cf.PortionType == PortionType.Full)
                        dayCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Full;
                    else if (cf.PortionType == PortionType.Half)
                        dayCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Half;
                    else if (cf.PortionType == PortionType.Quarter)
                        dayCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Quarter;
                }
            }

            return dayCalorie;
        }

        /// <summary>
        /// Retrieves the total amount of consumed foods for a specific user, food category, and meal types within a given number of days before today.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="howManyDaysBeforeToday">Number of days before today to consider</param>
        /// <param name="foodCategory">Food category to filter consumed foods</param>
        /// <param name="mealTypes">Meal types to filter consumed foods</param>
        /// <returns>Total amount of consumed foods for the specified criteria</returns>
        public int GetConsumedFoodsAmountForDaysById(int id, int howManyDaysBeforeToday, FoodCategory foodCategory, params MealType[] mealTypes)
        {
            User? user = db.Users.Find(id);
            TimeSpan timePassed = DateTime.Now - user.CreationTime;
            int totalAmount = 0;
            foreach (var mealType in mealTypes)
            {
                for (int i = 0; i < howManyDaysBeforeToday; i++)
                {
                    int day = (int)timePassed.TotalDays - i + 1;
                    var cfList = user.ConsumedFoods.Where(cf => cf.Day == day && cf.MealType == mealType && cf.Food.Category == foodCategory).ToList();

                    foreach (var cf in cfList)
                    {
                        totalAmount += cf.Quantity;
                        totalAmount += cf.PortionCount;
                    }

                }
            }
            return totalAmount;
        }

        /// <summary>
        /// Retrieves the total calorie count for a specific daily meal type of a user.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="mealType">Meal type to calculate the total calorie count</param>
        /// <returns>Total calorie count for the specified daily meal type</returns>
        public double GetCaloriesForDailyMeal(int id, MealType mealType)
        {
            User? user = db.Users.Find(id);
            TimeSpan timePassed = DateTime.Now - user.CreationTime;
            int day = (int)timePassed.TotalDays + 1;
            double totalCalorie = 0;


            var cfList = user.ConsumedFoods.Where(cf => cf.Day == day && cf.MealType == mealType).ToList();
            foreach (var cf in cfList)
            {
                if (cf.Quantity > 0)
                    totalCalorie += cf.Quantity * (double)cf.Food.CalorieFor100Gram;
                else if (cf.PortionCount > 0)
                {
                    if (cf.PortionType == PortionType.Full)
                        totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Full;
                    else if (cf.PortionType == PortionType.Half)
                        totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Half;
                    else if (cf.PortionType == PortionType.Quarter)
                        totalCalorie += cf.PortionCount * (double)(cf.Food.PortionGram / 100) * (double)cf.Food.CalorieFor100Gram / (int)PortionType.Quarter;
                }
            }
            return totalCalorie;

        }

        /// <summary>
        /// Calculates the basal metabolism of a user based on the Harris-Benedict formula and activity level.
        /// </summary>
        /// <param name="user">User object containing information such as gender, weight, height, age, and activity level</param>
        /// <returns>Basal metabolism of the user</returns>
        public double CalculateBasalMetabolism(string weight, User user)
        {
            double bmr = 0;

            // Calculate basal metabolism using the Harris-Benedict formula
            if (user.Gender == Gender.Male)
            {
                bmr = 88.362 + (13.397 * Convert.ToDouble(weight) + (4.799 * (double)user.Height) - (5.677 * user.Age));

            }
            else
            {
                bmr = 447.593 + (9.247 * (double)user.Weights.Last().Weight + (3.098 * (double)user.Height) - (4.330 * user.Age));
            }


            // Adjust basal metabolism based on activity level
            double activityFactor = 0;

            if (user.Activity == ActivityFrequency.LessExercise)
            {
                activityFactor = 1.2;
            }
            else if (user.Activity == ActivityFrequency.ModerateExercise)
            {
                activityFactor = 1.5;
            }
            else
            {
                activityFactor = 1.7;
            }

            double basalMetabolism = bmr * activityFactor;
            return basalMetabolism;
        }

        public User GetByEmail(string text)
        {
            return db.Users.SingleOrDefault(u => u.Mail == text);
        }
    }
}

using Castle.DynamicProxy.Generators;
using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{


    public class ConsumedFoodBLL : BaseClass<ConsumedFood>
    {
        /// <summary>
        /// Retrieves consumed foods for a specific user based on a given day and meal type.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="meal">Meal type</param>
        /// <returns>List of consumed foods</returns>
        public List<ConsumedFood> GetConsumedFoodsByDayAndMealType(int userId, MealType meal)
        {
            // Retrieve the user from the database
            User? user = db.Users.Find(userId);

            // Calculate the time elapsed since user creation
            TimeSpan timePassed = DateTime.Now - user.CreationTime;
            int day = (int)timePassed.TotalDays + 1;

            // Filter and retrieve consumed foods for the specified day and meal type
            var cfList = user.ConsumedFoods.Where(cf => cf.Day == day && cf.MealType == meal).ToList();

            return cfList;
        }

        /// <summary>
        /// Retrieves the ranking position of a food based on its ID and the meal types it is consumed in.
        /// </summary>
        /// <param name="foodId">Food ID</param>
        /// <param name="place">Ranking position of the food</param>
        /// <param name="mealTypes">Meal types in which the food is consumed</param>
        public void GetFoodConsumedPlaceByFoodId(int foodId, out int place, params MealType[] mealTypes)
        {
            place = 0;
            if (mealTypes.Count() == 1)
            {
                var list = db.ConsumedFoods.Where(cf => cf.MealType == mealTypes[0])
                                            .GroupBy(cf => cf.FoodId)
                                            .Select(group => new
                                            {
                                                FoodId = group.Key,
                                                TotalQuantityAndPortionCount = group.Sum(cf => cf.Quantity + cf.PortionCount)
                                            })
                                            .OrderByDescending(x => x.TotalQuantityAndPortionCount)
                                            .ToList();
                place = list.FindIndex(f => f.FoodId == foodId) + 1;
            }
            else if (mealTypes.Count() < 6)
            {
                var list = db.ConsumedFoods.Where(cf => cf.MealType != MealType.Breakfast && cf.MealType != MealType.Lunch && cf.MealType != MealType.Dinner)
                           .GroupBy(cf => cf.FoodId)
                           .Select(group => new
                           {
                               FoodId = group.Key,
                               TotalQuantityAndPortionCount = group.Sum(cf => cf.Quantity + cf.PortionCount)
                           })
                           .OrderByDescending(x => x.TotalQuantityAndPortionCount)
                           .ToList();
                place = list.FindIndex(f => f.FoodId == foodId) + 1;

            }
            else
            {
                var list = db.ConsumedFoods.GroupBy(cf => cf.FoodId)
                                            .Select(group => new
                                            {
                                                FoodId = group.Key,
                                                TotalQuantityAndPortionCount = group.Sum(cf => cf.Quantity + cf.PortionCount)
                                            })
                                             .OrderByDescending(x => x.TotalQuantityAndPortionCount)
                                             .ToList();
                place = list.FindIndex(f => f.FoodId == foodId) + 1;
            }
        }



        /// <summary>
        /// Retrieves the total quantity of a food consumed based on its ID and the meal types it is consumed in.
        /// </summary>
        /// <param name="foodId">Food ID</param>
        /// <param name="mealTypes">Meal types in which the food is consumed</param>
        /// <returns>Total quantity of the consumed food</returns>
        public int GetFoodConsumedTotalQuantityByFoodId(int foodId, params MealType[] mealTypes)
        {
            int totalQuantity = 0;
            if (mealTypes.Count() == 1)
            {
                var list = db.ConsumedFoods.Where(cf => cf.FoodId == foodId && cf.MealType == mealTypes[0] && cf.Quantity > 0).ToList();
                totalQuantity = list.Sum(cf => cf.Quantity);
            }
            else if (mealTypes.Count() < 6)
            {
                var list = db.ConsumedFoods.Where(cf => cf.FoodId == foodId && (cf.MealType != MealType.Breakfast && cf.MealType != MealType.Lunch && cf.MealType != MealType.Dinner) && cf.Quantity > 0).ToList();
                totalQuantity = list.Sum(cf => cf.Quantity);
            }
            else
            {
                var list = db.ConsumedFoods.Where(cf => cf.FoodId == foodId && cf.Quantity > 0).ToList();
                totalQuantity = list.Sum(cf => cf.Quantity);

            }
            return totalQuantity;
        }

        /// <summary>
        /// Retrieves the total portion count of a food consumed based on its ID and the meal types it is consumed in.
        /// </summary>
        /// <param name="foodId">Food ID</param>
        /// <param name="mealTypes">Meal types in which the food is consumed</param>
        /// <returns>Total portion count of the consumed food</returns>
        public int GetFoodConsumedTotalPortionCountByFoodId(int foodId, params MealType[] mealTypes)
        {
            int totalPortionCount = 0;
            if (mealTypes.Count() == 1)
            {
                var list = db.ConsumedFoods.Where(cf => cf.FoodId == foodId && cf.MealType == mealTypes[0] && cf.PortionCount > 0).ToList();
                totalPortionCount = list.Sum(cf => cf.PortionCount);
            }
            else if (mealTypes.Count() < 6)
            {
                var list = db.ConsumedFoods.Where(cf => cf.FoodId == foodId && (cf.MealType != MealType.Breakfast && cf.MealType != MealType.Lunch && cf.MealType != MealType.Dinner) && cf.PortionCount > 0).ToList();
                totalPortionCount = list.Sum(cf => cf.PortionCount);
            }
            else
            {
                var list = db.ConsumedFoods.Where(cf => cf.FoodId == foodId && cf.PortionCount > 0).ToList();
                totalPortionCount = list.Sum(cf => cf.PortionCount);

            }
            return totalPortionCount;
        }

        /// <summary>
        /// Retrieves the total number of unique days in the consumed foods.
        /// </summary>
        /// <returns>Total number of unique days in the consumed foods</returns>
        public int GetTotalDaysInConsumedFoods()
        {
            int totalDays = 0;
            var cflist = db.ConsumedFoods.GroupBy(cf => cf.Day);
            totalDays = cflist.Count();
            return totalDays;
        }

        /// <summary>
        /// Checks if there are any consumed foods for a specific user, day, and meal type.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="day">Day</param>
        /// <param name="type">Meal type</param>
        /// <returns>True if there are consumed foods for the specified user, day, and meal type; otherwise, false.</returns>
        public bool GetConsumedFoodsByUserID(int id, int day, MealType type)
        {
            int number = db.ConsumedFoods.Where(x => x.UserId == id && x.Day == day && x.MealType == type).Count();
            return number > 0;
        }
    }
}

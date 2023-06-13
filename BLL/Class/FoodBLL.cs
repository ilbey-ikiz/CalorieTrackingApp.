
using Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FoodBLL:BaseClass<Food>
    {
        /// <summary>
        /// Retrieves a list of foods that contain the specified word in their name.
        /// </summary>
        /// <param name="text">Word to search for in food names</param>
        /// <returns>List of foods that contain the specified word in their name</returns>
        public List<Food> GetFoodsByWord(string text)
        {
            List<Food> foods = db.Foods.Where(x=>x.Name.Contains(text)).ToList();    
            return foods;
        }

        /// <summary>
        /// Retrieves the ID of a food by its name.
        /// </summary>
        /// <param name="name">Name of the food</param>
        /// <returns>ID of the food</returns>
        public int GetFoodIdByFoodName(string name)
        {
            Food food =db.Foods.Where(x=>x.Name == name).SingleOrDefault();
            int id = food.ID;
            return id;

        }

        /// <summary>
        /// Retrieves a list of foods that contain the specified text in their name or category, owned by the specified owner ID.
        /// </summary>
        /// <param name="text">Text to search for in food names or categories</param>
        /// <param name="ownerId">Owner ID</param>
        /// <returns>List of foods that match the search criteria</returns>
        public List<Food> GetFoodsByContainTextAndOwnerId(string text,int ownerId)
        {
            
            List<Food> searchedFoods = new List<Food>();
            var foods = db.Foods.Where(f => f.OwnerId == 0 || f.OwnerId == ownerId);
            foreach (var food in foods)
            {
                if (food.Category.ToString().ToLower().Contains(text.ToLower()) || food.Name.ToLower().Contains(text.ToLower()))
                    searchedFoods.Add(food);
            }
            return searchedFoods;
        }

        /// <summary>
        /// Checks if a food with the specified name exists in the database.
        /// </summary>
        /// <param name="name">Name of the food</param>
        /// <returns>True if a food with the specified name exists; otherwise, false</returns>
        public bool IsFoodExist(string name)
        {
            return db.Foods.Any(f => f.Name == name);
        }
    }
}

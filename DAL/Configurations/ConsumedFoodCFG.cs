using Entities;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class ConsumedFoodCFG : IEntityTypeConfiguration<ConsumedFood>
    {

        public void Configure(EntityTypeBuilder<ConsumedFood> builder)
        {
            builder.Property(x => x.Quantity).HasDefaultValue(0);
            builder.Property(x => x.PortionCount).HasDefaultValue(0);
            builder.HasData(RandomData());

        }

        public List<ConsumedFood> RandomData()
        {
            Random rand = new Random();
            var consumedFoods = new List<ConsumedFood>();
            for (int i = 1; i <= 1250; i++)
            {
                consumedFoods.Add(new ConsumedFood
                {
                    ID = i,
                    MealType = (MealType)rand.Next(1, 4),
                    Quantity = rand.Next(1, 6),
                    Day = rand.Next(1, 150),
                    UserId = rand.Next(1, 3),
                    FoodId = rand.Next(1, 29),
                });
            }
            for (int i = 1251; i <= 2250; i++)
            {
                consumedFoods.Add(new ConsumedFood
                {
                    ID = i,
                    MealType = (MealType)rand.Next(1, 4),
                    PortionCount = rand.Next(1, 4),
                    PortionType = (PortionType)rand.Next(1, 3),
                    Day = rand.Next(1, 150),
                    UserId = rand.Next(1, 3),
                    FoodId = rand.Next(1, 29),
                });
            }
            for (int i = 2251; i <= 2500; i++)
            {
                consumedFoods.Add(new ConsumedFood
                {
                    ID = i,
                    MealType = (MealType)rand.Next(1, 4),
                    PortionCount = rand.Next(1, 4),
                    PortionType = (PortionType)4,
                    Day = rand.Next(1, 150),
                    UserId = rand.Next(1, 3),
                    FoodId = rand.Next(1, 29),
                });
            }
            for (int i = 2501; i <= 2700; i++)
            {
                consumedFoods.Add(new ConsumedFood
                {
                    ID = i,
                    MealType = (MealType)rand.Next(4, 9),
                    PortionCount = rand.Next(1, 4),
                    PortionType = (PortionType)rand.Next(1, 3),
                    Day = rand.Next(1, 150),
                    UserId = rand.Next(1, 3),
                    FoodId = rand.Next(1, 29),
                });
            }
            for (int i = 2701; i <= 2750; i++)
            {
                consumedFoods.Add(new ConsumedFood
                {
                    ID = i,
                    MealType = (MealType)rand.Next(4, 9),
                    PortionCount = rand.Next(1, 4),
                    PortionType = (PortionType)4,
                    Day = rand.Next(1, 150),
                    UserId = rand.Next(1, 3),
                    FoodId = rand.Next(1, 29),
                });
            }
            for (int i = 2751; i <= 3000; i++)
            {
                consumedFoods.Add(new ConsumedFood
                {
                    ID = i,
                    MealType = (MealType)rand.Next(4, 9),
                    Quantity = rand.Next(1, 6),
                    Day = rand.Next(1, 150),
                    UserId = rand.Next(1, 3),
                    FoodId = rand.Next(1, 29),
                });
            }

            return consumedFoods;
        }        
    }
}

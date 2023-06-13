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
    public class FoodCFG : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.Property(a => a.Name).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(a => a.CalorieFor100Gram).HasPrecision(5, 2);
            builder.Property(a => a.ProteinRateFor100Gram).HasPrecision(5, 2);
            builder.Property(a => a.FatRateFor100Gram).HasPrecision(5, 2);
            builder.Property(a => a.CarbonhydrateAmountFor100Gram).HasPrecision(5, 2);
            builder.Property(a => a.OwnerId).HasDefaultValue(0);
            builder.Property(p => p.Image).HasColumnType("varbinary(max)");



            builder.HasData(
                new Food
                {
                    ID = 1,
                    Name = "Hamburger",
                    Category = FoodCategory.FastFood,
                    PortionGram = 190,
                    CalorieFor100Gram = Convert.ToDecimal(164),
                    ProteinRateFor100Gram = 7,
                    FatRateFor100Gram = 6,
                    CarbonhydrateAmountFor100Gram = 20,
                },
                new Food
                {
                    ID = 2,
                    Name = "Pizza",
                    Category = FoodCategory.FastFood,
                    PortionGram = 185,
                    CalorieFor100Gram = Convert.ToDecimal(185),
                    ProteinRateFor100Gram = Convert.ToDecimal(6),
                    FatRateFor100Gram = Convert.ToDecimal(4),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(31),
                },                
                new Food
                {
                    ID = 3,
                    Name = "Mantı",
                    Category = FoodCategory.BakedGood,
                    PortionGram = 190,
                    CalorieFor100Gram = Convert.ToDecimal(169),
                    ProteinRateFor100Gram = Convert.ToDecimal(4),
                    FatRateFor100Gram = Convert.ToDecimal(4),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(30),
                },                
                new Food
                {
                    ID = 4,
                    Name = "Seasonal Salad ",
                    Category = FoodCategory.Salad,
                    PortionGram = 190,
                    CalorieFor100Gram = Convert.ToDecimal(31),
                    ProteinRateFor100Gram = Convert.ToDecimal(1),
                    FatRateFor100Gram = Convert.ToDecimal(1),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(4),
                },                
                new Food
                {
                    ID = 5,
                    Name = "Cola",
                    Category = FoodCategory.Beverage,
                    PortionGram = 250,
                    CalorieFor100Gram = Convert.ToDecimal(61),
                    ProteinRateFor100Gram = Convert.ToDecimal(0),
                    FatRateFor100Gram = Convert.ToDecimal(0),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(11),
                },                
                new Food
                {
                    ID = 6,
                    Name = "Ayran",
                    Category = FoodCategory.Beverage,
                    PortionGram = 250,
                    CalorieFor100Gram = Convert.ToDecimal(50),
                    ProteinRateFor100Gram = Convert.ToDecimal(2),
                    FatRateFor100Gram = Convert.ToDecimal(3),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(5),
                },                
                new Food
                {
                    ID = 7,
                    Name = "Cornflakes",
                    Category = FoodCategory.Cereals,
                    PortionGram = 30,
                    CalorieFor100Gram = Convert.ToDecimal(357),
                    ProteinRateFor100Gram = Convert.ToDecimal(8),
                    FatRateFor100Gram = Convert.ToDecimal(0.4),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(84),
                },                
                new Food
                {
                    ID = 8,
                    Name = "Chocolate Nesquik Flakes",
                    Category = FoodCategory.Cereals,
                    PortionGram = 40,
                    CalorieFor100Gram = Convert.ToDecimal(403),
                    ProteinRateFor100Gram = Convert.ToDecimal(8),
                    FatRateFor100Gram = Convert.ToDecimal(9),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(69),
                },                
                new Food
                {
                    ID = 9,
                    Name = "White Cheese",
                    Category = FoodCategory.Dairy,
                    PortionGram = 50,
                    CalorieFor100Gram = Convert.ToDecimal(310),
                    ProteinRateFor100Gram = Convert.ToDecimal(20),
                    FatRateFor100Gram = Convert.ToDecimal(24),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(3),
                },                
                new Food
                {
                    ID = 10,
                    Name = "Yogurt",
                    Category = FoodCategory.Dairy,
                    PortionGram = 160,
                    CalorieFor100Gram = Convert.ToDecimal(61),
                    ProteinRateFor100Gram = Convert.ToDecimal(3),
                    FatRateFor100Gram = Convert.ToDecimal(3),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(5),
                },                
                new Food
                {
                    ID = 11,
                    Name = "Puding",
                    Category = FoodCategory.Dessert,
                    PortionGram = 150,
                    CalorieFor100Gram = Convert.ToDecimal(119),
                    ProteinRateFor100Gram = Convert.ToDecimal(3),
                    FatRateFor100Gram = Convert.ToDecimal(3),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(20),
                },                
                new Food
                {
                    ID = 12,
                    Name = "Baklava",
                    Category = FoodCategory.Dessert,
                    PortionGram = 160,
                    CalorieFor100Gram = Convert.ToDecimal(411),
                    ProteinRateFor100Gram = Convert.ToDecimal(7),
                    FatRateFor100Gram = Convert.ToDecimal(22),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(49),
                },                
                new Food
                {
                    ID = 13,
                    Name = "Apple",
                    Category = FoodCategory.Fruit,
                    PortionGram = 120,
                    CalorieFor100Gram = Convert.ToDecimal(52),
                    ProteinRateFor100Gram = Convert.ToDecimal(0.26),
                    FatRateFor100Gram = Convert.ToDecimal(0.17),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(14),
                },                
                new Food
                {
                    ID = 14,
                    Name = "Banana",
                    Category = FoodCategory.Fruit,
                    PortionGram = 130,
                    CalorieFor100Gram = Convert.ToDecimal(88),
                    ProteinRateFor100Gram = Convert.ToDecimal(1),
                    FatRateFor100Gram = Convert.ToDecimal(0.3),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(23),
                },                
                new Food
                {
                    ID = 15,
                    Name = "Chickpeas",
                    Category = FoodCategory.Legume,
                    PortionGram = 80,
                    CalorieFor100Gram = Convert.ToDecimal(360),
                    ProteinRateFor100Gram = Convert.ToDecimal(21),
                    FatRateFor100Gram = Convert.ToDecimal(5),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(61),
                },                
                new Food
                {
                    ID = 16,
                    Name = "Roasted Chickpea",
                    Category = FoodCategory.Legume,
                    PortionGram = 45,
                    CalorieFor100Gram = Convert.ToDecimal(268),
                    ProteinRateFor100Gram = Convert.ToDecimal(20),
                    FatRateFor100Gram = Convert.ToDecimal(3),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(38),
                },                
                new Food
                {
                    ID = 17,
                    Name = "Steak",
                    Category = FoodCategory.Meat,
                    PortionGram = 140,
                    CalorieFor100Gram = Convert.ToDecimal(214),
                    ProteinRateFor100Gram = Convert.ToDecimal(23),
                    FatRateFor100Gram = Convert.ToDecimal(13),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(0),
                },                
                new Food
                {
                    ID = 18,
                    Name = "Meatball",
                    Category = FoodCategory.Meat,
                    PortionGram = 108,
                    CalorieFor100Gram = Convert.ToDecimal(204),
                    ProteinRateFor100Gram = Convert.ToDecimal(17.45),
                    FatRateFor100Gram = Convert.ToDecimal(13.08),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(7.04),
                },                
                new Food
                {
                    ID = 19,
                    Name = "Trout",
                    Category = FoodCategory.SeaFood,
                    PortionGram = 200,
                    CalorieFor100Gram = Convert.ToDecimal(121),
                    ProteinRateFor100Gram = Convert.ToDecimal(19),
                    FatRateFor100Gram = Convert.ToDecimal(15),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(0),
                },                
                new Food
                {
                    ID = 20,
                    Name = "Salmon",
                    Category = FoodCategory.SeaFood,
                    PortionGram = 57,
                    CalorieFor100Gram = Convert.ToDecimal(146),
                    ProteinRateFor100Gram = Convert.ToDecimal(21.62),
                    FatRateFor100Gram = Convert.ToDecimal(5.93),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(0),
                },                
                new Food
                {
                    ID = 21,
                    Name = "Caesar Salad",
                    Category = FoodCategory.Salad,
                    PortionGram = 108,
                    CalorieFor100Gram = Convert.ToDecimal(170),
                    ProteinRateFor100Gram = Convert.ToDecimal(5.03),
                    FatRateFor100Gram = Convert.ToDecimal(14.17),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(6.52),
                },                
                new Food
                {
                    ID = 22,
                    Name = "Lentil Soup ",
                    Category = FoodCategory.Soup,
                    PortionGram = 248,
                    CalorieFor100Gram = Convert.ToDecimal(186),
                    ProteinRateFor100Gram = Convert.ToDecimal(10.42),
                    FatRateFor100Gram = Convert.ToDecimal(4.59),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(26.61),
                },                
                new Food
                {
                    ID = 23,
                    Name = "Mushroom Soup",
                    Category = FoodCategory.Soup,
                    PortionGram = 244,
                    CalorieFor100Gram = Convert.ToDecimal(129),
                    ProteinRateFor100Gram = Convert.ToDecimal(2.32),
                    FatRateFor100Gram = Convert.ToDecimal(9.3),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(8.98),
                },                
                new Food
                {
                    ID = 24,
                    Name = "Cucumber",
                    Category = FoodCategory.Vegetable,
                    PortionGram = 60,
                    CalorieFor100Gram = Convert.ToDecimal(15),
                    ProteinRateFor100Gram = Convert.ToDecimal(0.67),
                    FatRateFor100Gram = Convert.ToDecimal(0.11),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(3.63),
                },                
                new Food
                {
                    ID = 25,
                    Name = "Carrot",
                    Category = FoodCategory.Vegetable,
                    PortionGram = 150,
                    CalorieFor100Gram = Convert.ToDecimal(25),
                    ProteinRateFor100Gram = Convert.ToDecimal(0.57),
                    FatRateFor100Gram = Convert.ToDecimal(0.15),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(5.84),
                },                
                new Food
                {
                    ID = 26,
                    Name = "Haydari",
                    Category = FoodCategory.Others,
                    PortionGram = 30,
                    CalorieFor100Gram = Convert.ToDecimal(19),
                    ProteinRateFor100Gram = Convert.ToDecimal(0.8),
                    FatRateFor100Gram = Convert.ToDecimal(1.32),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(1.1),
                },                
                new Food
                {
                    ID = 27,
                    Name = "Probis (Ulker)",
                    Category = FoodCategory.Others,
                    PortionGram = 9,
                    CalorieFor100Gram = Convert.ToDecimal(41),
                    ProteinRateFor100Gram = Convert.ToDecimal(1.25),
                    FatRateFor100Gram = Convert.ToDecimal(1.98),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(4.29),
                },                
                new Food
                {
                    ID = 28,
                    Name = "Bread",
                    Category = FoodCategory.BakedGood,
                    PortionGram = 60,
                    CalorieFor100Gram = Convert.ToDecimal(256),
                    ProteinRateFor100Gram = Convert.ToDecimal(12.87),
                    FatRateFor100Gram = Convert.ToDecimal(3.17),
                    CarbonhydrateAmountFor100Gram = Convert.ToDecimal(83.96),
                }
                );
        }
    }
}

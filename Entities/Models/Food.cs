using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Food:BaseClass
    {
        public Food()
        {
            ConsumedFoods = new HashSet<ConsumedFood>();
        }
        public string Name { get; set; }
        public decimal PortionGram { get; set; }
        public FoodCategory Category { get; set; }
        public decimal CalorieFor100Gram { get; set; }
        public decimal ProteinRateFor100Gram { get; set; }
        public decimal FatRateFor100Gram { get; set; }
        public decimal CarbonhydrateAmountFor100Gram { get; set; }
        public byte[]? Image { get; set; }
        public int OwnerId { get; set; }

        public virtual ICollection<ConsumedFood> ConsumedFoods { get; set; }

    }
} 

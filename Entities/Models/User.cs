using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : BaseClass
    {
        public User()
        {
            ConsumedFoods = new HashSet<ConsumedFood>();
            DayGoalCreationTime = DateTime.Now;
        }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => FirstName + " " + LastName; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Height { get; set; }
        //[NotMapped]
        //public decimal CurrentWeight { get; set; }
        public virtual ICollection<WeightHistory> Weights { get; set; }
        public int DailyGoalCalorie { get; set; }
        public decimal GoalWeight { get; set; }
        public Enums.ActivityFrequency Activity { get; set; }

        public DateTime DayGoalCreationTime {  get; private set; }
        [NotMapped]
        private int dayGoal;
        public int DayGoal
        {
            get { return dayGoal; }
            set
            {
                if (dayGoal != value)
                {
                    dayGoal = value;
                    if (dayGoal != 0)
                    {
                        DayGoalCreationTime = DateTime.Now;
                    }
                }
            }
        }
        public byte[]? Image { get; set; }
        //public decimal BodyMassIndex { get => Weight / (Height * Height); }

        public virtual ICollection<ConsumedFood> ConsumedFoods { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; }

    }
}

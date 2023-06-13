using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class ActivityCFG : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.HasData(
                new Activity { ID = 1, ActivityName = "Martial Arts", CaloriesBurnedPerMinute = 11 }, 
                new Activity { ID = 2, ActivityName = "Diving", CaloriesBurnedPerMinute = 8 }, 
                new Activity { ID = 3, ActivityName = "Football", CaloriesBurnedPerMinute = 8 }, 
                new Activity { ID = 4, ActivityName = "Running", CaloriesBurnedPerMinute = 15 }, 
                new Activity { ID = 5, ActivityName = "Climbing", CaloriesBurnedPerMinute = 13 }, 
                new Activity { ID = 6, ActivityName = "Cycling", CaloriesBurnedPerMinute = 14 }, 
                new Activity { ID = 7, ActivityName = "Swimming", CaloriesBurnedPerMinute = 12 }, 
                new Activity { ID = 8, ActivityName = "Basketball", CaloriesBurnedPerMinute = 9 }, 
                new Activity { ID = 9, ActivityName = "Jump Rope", CaloriesBurnedPerMinute = 13 }, 
                new Activity { ID = 10, ActivityName = "Tennis", CaloriesBurnedPerMinute = 8 } , 
                new Activity { ID=11 , ActivityName="Walking" , CaloriesBurnedPerMinute=4});
        }
    }
}

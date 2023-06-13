using Entities;
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
    public class UserActivityCFG : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.HasData(
                new UserActivity()
                {
                    ID = 1,
                    UserID = 1,
                    ActivityID = 2,
                    Minute = 20,
                    TotalBurnedCalorie = 200,
                },
                new UserActivity()
                {
                    ID = 2,
                    UserID = 1,
                    ActivityID = 5,
                    Minute = 10,
                    TotalBurnedCalorie = 100,
                },
                new UserActivity()
                {
                    ID = 3,
                    UserID = 1,
                    ActivityID = 7,
                    Minute = 30,
                    TotalBurnedCalorie = 300,
                },
                new UserActivity()
                {
                    ID = 4,
                    UserID = 2,
                    ActivityID = 1,
                    Minute = 40,
                    TotalBurnedCalorie = 400,
                },
                new UserActivity()
                {
                    ID = 5,
                    UserID = 2,
                    ActivityID = 3,
                    Minute = 30,
                    TotalBurnedCalorie = 300,
                },
                new UserActivity()
                {
                    ID = 6,
                    UserID = 2,
                    ActivityID = 6,
                    Minute = 50,
                    TotalBurnedCalorie = 500,
                }
                );
        }
    }
}

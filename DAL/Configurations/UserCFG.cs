using Entities;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class UserCFG : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).HasColumnType("nvarchar").HasMaxLength(30);
            builder.Property(x => x.LastName).HasColumnType("nvarchar").HasMaxLength(30);
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar").HasMaxLength(10);
            builder.Property(x => x.DayGoal).HasDefaultValue(0).HasMaxLength(90);
            builder.Property(x=>x.Age).HasMaxLength(120);
            builder.Ignore(p => p.DayGoalCreationTime);
            builder.Ignore(p => p.FullName);
            //builder.Ignore(p => p.BodyMassIndex);
            builder.Property(p => p.Image).HasColumnType("varbinary(max)");

            //.HasDefaultValue("C:\\Users\\llhol\\Source\\Repos\\kyruger\\Grup5_KaloriTakipProgrami\\Grup5_KaloriTakipProgrami\\Resources\\Icons\\Main Icons\\628298_anonym_avatar_default_head_person_icon.png");

            builder.HasData(
                new User
                {
                    ID = 1,
                    CreationTime = new DateTime(2023, 5, 20, 14, 30, 0),
                    Mail = "eneskurt@bilgeadam.com",
                    Password = "12345",
                    FirstName = "Enes",
                    LastName = "Kurt",
                    Gender = Gender.Male,
                    Age = 27,
                    DailyGoalCalorie = 3000,
                    PhoneNumber = "5553332211",
                    GoalWeight = 72,
                    Height = 170,
                    Activity = ActivityFrequency.LotsOfExercise

                },
                new User
                {
                    ID = 2,
                    CreationTime = new DateTime(2023, 5, 20, 14, 30, 0),
                    Mail = "ilbeyikiz@bilgeadam.com",
                    Password = "12345",
                    FirstName = "İlbey",
                    LastName = "İkiz",
                    Gender = Gender.Male,
                    Age = 25,
                    DailyGoalCalorie = 2800,
                    PhoneNumber = "1113332211",
                    GoalWeight = 82,
                    Height = 182,
                    Activity = ActivityFrequency.LessExercise

                }
                ) ; 
        }
    }
}

using Entities;
using Entities.Enums;
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
    public class WeightHsitoryCFG : IEntityTypeConfiguration<WeightHistory>
    {
        public void Configure(EntityTypeBuilder<WeightHistory> builder)
        {
            builder.HasData(
                new WeightHistory
                {
                    ID = 1,
                    UserId =1,
                    Weight = Convert.ToDecimal(66.5),
                    CreationTime = new DateTime(2023, 5, 20, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 2,
                    UserId = 1,
                    Weight = Convert.ToDecimal(69),
                    CreationTime = new DateTime(2023, 5, 24, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 3,
                    UserId = 1,
                    Weight = Convert.ToDecimal(67),
                    CreationTime = new DateTime(2023, 5, 27, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 4,
                    UserId = 1,
                    Weight = Convert.ToDecimal(63),
                    CreationTime = new DateTime(2023, 6, 1, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 5,
                    UserId = 1,
                    Weight = Convert.ToDecimal(65),
                    CreationTime = new DateTime(2023, 6, 6, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 6,
                    UserId = 1,
                    Weight = Convert.ToDecimal(66),
                    CreationTime = new DateTime(2023, 6, 10, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 7,
                    UserId = 2,
                    Weight = Convert.ToDecimal(83),
                    CreationTime = new DateTime(2023, 5, 20, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 8,
                    UserId = 2,
                    Weight = Convert.ToDecimal(79),
                    CreationTime = new DateTime(2023, 5, 23, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 9,
                    UserId = 2,
                    Weight = Convert.ToDecimal(83),
                    CreationTime = new DateTime(2023, 5, 28, 14, 30, 0),
                },
                new WeightHistory
                {
                    ID = 10,
                    UserId = 2,
                    Weight = Convert.ToDecimal(85),
                    CreationTime = new DateTime(2023, 6, 7, 14, 30, 0),
                }
                );
        }
    }
}

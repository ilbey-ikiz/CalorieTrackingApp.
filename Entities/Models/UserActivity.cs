using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UserActivity:BaseClass
    {
        public decimal TotalBurnedCalorie { get; set; }
        public int Minute { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public int ActivityID { get; set; }
        public virtual Activity Activity { get; set; }
    }
}

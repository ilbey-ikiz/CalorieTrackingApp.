using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class WeightHistory:BaseClass
    {
        public decimal Weight { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

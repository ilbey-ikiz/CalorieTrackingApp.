using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BaseClass
    {
        public BaseClass()
        {
            CreationTime = DateTime.Now;
        }
        public int ID { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

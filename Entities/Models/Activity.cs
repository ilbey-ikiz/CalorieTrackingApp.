using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Activity:BaseClass
    {
        public Activity()
        {
            UserActivities = new HashSet<UserActivity>();
        }
        public string ActivityName { get; set; }
        public decimal CaloriesBurnedPerMinute { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; }
    }
}

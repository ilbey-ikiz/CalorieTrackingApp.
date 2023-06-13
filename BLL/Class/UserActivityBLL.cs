using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BLL.Class
{
    public class UserActivityBLL : BaseClass<UserActivity>
    {
        public List<Tuple<int,string, decimal, int>> GetActivityListToday(int userId)
        {
            var querry= db.UserActivity.Where(d => d.CreationTime.Date == DateTime.Now.Date && d.UserID == userId).ToList();

            List<Tuple<int, string, decimal, int>> list = new List<Tuple<int,string,decimal, int>>();
            foreach (var userActivity in querry)
            {
                list.Add(new Tuple<int, string, decimal, int> (userActivity.ID ,userActivity.Activity.ActivityName,userActivity.TotalBurnedCalorie,userActivity.Minute));
            }
            return list;

        }

        public List<UserActivity> GetListByUserID(int id)
        {
            return db.UserActivity.Where(a => a.UserID == id).ToList();
        }

        public void GetUserTopActivity(int userID , out int totalDuration , out int count , out string activityName)
        {
            totalDuration = 0;
            count = 0;
            activityName = "";
            var list = db.UserActivity.Where(a => a.UserID == userID)
                 .GroupBy(a => a.Activity.ActivityName)
                 .Select(g => new
                 {
                     ActivityName = g.Key,
                     Count = g.Count(),
                     TotalDuration = g.Sum(a => a.Minute)
                 })
                 .OrderByDescending(d => d.TotalDuration).ToList();

            totalDuration = list.First().TotalDuration;
            count = list.First().Count;
            activityName = list.First().ActivityName;   
        }

        public List<Tuple<string, int, decimal>> GetActivitiesAndTimes(int userID )
        {
            var query = db.UserActivity.Where(s => s.UserID == userID)
                                  .GroupBy (s => s.Activity.ActivityName)
                                  .Select( g => new 
                                  { 
                                      ActivityName = g.Key ,
                                      TotalMinute = g.Sum(a => a.Minute),
                                      TotalCalorie = g.Sum(a => a.TotalBurnedCalorie)
                                  }).ToList();
            List<Tuple<string, int, decimal>> list = new List<Tuple<string, int, decimal>>();
            foreach (var activity in query)
            {
                list.Add(new Tuple<string , int , decimal > (activity.ActivityName , activity.TotalMinute , activity.TotalCalorie));
            }
            return list;
        }

    




    }
}

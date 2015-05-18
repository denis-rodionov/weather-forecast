using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// What is the average time of hottest daily temperature (over month)
    /// </summary>
    class Task1a : BaseTask
    {
        public Task1a(WeatherStatistics statistics) : base(statistics)
        {
        }

        public override List<string> Solve()
        {
            var res = new List<string>();
            var months = Statistics.Months;

            foreach (var m in months)
            {
                var averageTime = GetAverageTime(m);
                res.Add(string.Format("{0},{1}", m.ToString("MMMM yyyy"), averageTime.ToString("hh\\:mm")));
            }

            return res;
        }

        private TimeSpan GetAverageTime(DateTime m)
        {
            var monthStat = Statistics.GetData(m, m.AddMonths(1));

            var days = monthStat.Days;

            var sumTime = new TimeSpan(0);

            foreach (var day in days)
            {
                var hotestRecord = HottestTimeRecord(day, monthStat);
                sumTime += hotestRecord.Time.TimeOfDay;
            }
                
            return new TimeSpan(sumTime.Ticks / days.Count());
        }

        
    }
}

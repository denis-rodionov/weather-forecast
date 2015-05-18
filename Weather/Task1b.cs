using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// What time of the day is the most commonly occurring hottest time
    /// </summary>
    class Task1b : BaseTask
    {
        public Task1b(WeatherStatistics statistics)
            : base(statistics)
        {
        }

        public override List<string> Solve()
        {
            var res = new Dictionary<TimeSpan, int>();
            var days = Statistics.Days;

            foreach (var d in days)
            {
                var hottestTime = HottestTimeRecord(d, Statistics).Time.TimeOfDay;

                if (!res.ContainsKey(hottestTime))
                    res.Add(hottestTime, 1);
                else
                    res[hottestTime] += 1;
            }

            res = FilterValues(res);

            return ToReportFormat(res);
        }

        /// <summary>
        /// Leave only biggest values
        /// </summary>
        /// <param name="res"></param>
        private Dictionary<TimeSpan, int> FilterValues(Dictionary<TimeSpan, int> data)
        {
            var maxOccuranceValue = data.Values.Max();
            var res = new Dictionary<TimeSpan, int>();

            foreach (var key in data.Keys)
                if (data[key] == maxOccuranceValue)
                    res.Add(key, maxOccuranceValue);

            return res;
        }

        private List<string> ToReportFormat(Dictionary<TimeSpan, int> data)
        {
            var res = new List<string>();

            foreach (var key in data.Keys)
                res.Add(string.Format("{0} \t{1} times", key, data[key]));

            return res;
        }
    }
}

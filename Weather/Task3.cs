using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// 3.	You want to forecast the “Outside Temperature” for the first 9 days of the next month. 
    /// 
    /// Assume that:
    /// •	The average temperature for each day of July is constant and equal to 25 degrees;
    /// •	For the 1st of July, the pattern of the temperatures across the day with respect 
    ///     to the average temperature on that day is similar to the one found on 1st of June, for the 
    ///     2nd of July is similar to the average on the 2nd of June, etc.
    ///     
    /// Produce a “.txt” file with your forecast for July (from 1st July to 9th July) 
    /// with the sample values for each time for e.g. dd/mm/yyyy, Time, Outside Temperature. 
    /// </summary>
    class Task3 : BaseTask
    {
        const double AVERAGE_JULY = 25;

        public Task3(WeatherStatistics statistics)
            : base(statistics)
        {
        }

        public override List<string> Solve()
        {
            var start = new DateTime(2006, 06, 1);
            var end = new DateTime(2006, 06, 10);

            var stat = Statistics.GetData(start, end);
            var days = stat.Days;

            var res = new List<string>();
            foreach (var d in days)
                res.AddRange(MakeForecast(d));

            return res;
        }

        private IEnumerable<string> MakeForecast(DateTime day)
        {
            var stat = Statistics.GetData(day, day.AddDays(1));
            var average = stat.Records.Average(r => r.OutsideTemperature);
            var diff = AVERAGE_JULY - average;

            foreach (var r in stat.Records)
                yield return string.Format("{0:dd/MM/yyyy}, {0:hh\\:mm}, {1:F1}", r.Time, r.OutsideTemperature + diff);
        }
    }
}

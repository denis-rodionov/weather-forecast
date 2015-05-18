using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// 2.	Using the ‘Hi Temperature’ values produce a  “.txt”  file containing 
    /// all of the Dates and Times where the “Hi Temperature” was within +/-  1 degree of  22.3 or 
    /// the “Low Temperature” was within +/- 0.2 degree higher or lower of 10.3 over the first 9  days of June
    /// </summary>
    class Task2 : BaseTask
    {
        public Task2(WeatherStatistics statistics)
            : base(statistics)
        {
        }

        public override List<string> Solve()
        {
            var resSet = new List<StatisticRecord>();
            var startPeriod = new DateTime(2006, 6, 1);
            var endPeriod = startPeriod.AddDays(9);
            var t1 = 22.3;
            var diff1 = 1;
            var t2 = 10.3;
            var diff2 = 0.2;

            var period = Statistics.GetData(startPeriod, endPeriod);

            foreach (var r in period.Records)
            {
                // t1
                if ((r.HiTemperature > t1 - diff1) && (r.HiTemperature < t1 + diff1))
                    resSet.Add(r);

                // t2
                if ((r.HiTemperature > t2 - diff2) && (r.HiTemperature < t2 + diff2))
                    resSet.Add(r);
            }

            return ToStringView(resSet);
        }

        private List<string> ToStringView(List<StatisticRecord> records)
        {
            var res = new List<string>();

            foreach (var r in records)
                res.Add(string.Format("{0}\tHi Temperature: {1}", r.Time, r.HiTemperature));

            return res;
        }
    }
}

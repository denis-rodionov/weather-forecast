using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// Which are the Top Ten hottest times on distinct days, preferably sorted by date order
    /// </summary>
    class Task1c : BaseTask
    {
        public Task1c(WeatherStatistics statistics)
            : base(statistics)
        {
        }

        public override List<string> Solve()
        {
            var hottestRecordsByDays = GetHottestRecordsByDays();

            var top10 = hottestRecordsByDays.OrderByDescending(r => r.OutsideTemperature).Take(10);

            top10 = top10.OrderBy(r => r.Time);

            return ToStringView(top10);
        }

        private List<string> ToStringView(IEnumerable<StatisticRecord> records)
        {
            var res = new List<string>();

            foreach (var r in records)
                res.Add(string.Format("{0}\tTemperature: {1}", r.Time, r.OutsideTemperature));

            return res;
        }

        private IEnumerable<StatisticRecord> GetHottestRecordsByDays()
        {
            var res = new List<StatisticRecord>();
            var days = Statistics.Days;

            foreach (var d in days)
            {
                res.Add(HottestTimeRecord(d, Statistics));
            }

            return res;
        }
    }
}

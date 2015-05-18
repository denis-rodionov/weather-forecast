using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class WeatherStatistics
    {
        /// <summary>
        /// Data collection
        /// </summary>
        public List<StatisticRecord> Records { get; set; }

        /// <summary>
        /// Constructor for weather statistic class
        /// </summary>
        /// <param name="filePath">Outer CSV file with weather statistic</param>
        public WeatherStatistics(List<StatisticRecord> records)
        {
            Records = records;
        }

        public IEnumerable<DateTime> Months
        {
            get
            {
                var res = new List<DateTime>();

                foreach (var r in Records)
                    if (!res.Any(t => t.Month == r.Time.Month && t.Year == r.Time.Year))
                        res.Add(new DateTime(r.Time.Year, r.Time.Month, 1));

                return res;
            }
        }

        public IEnumerable<DateTime> Days
        {
            get
            {
                var res = new List<DateTime>();

                foreach (var r in Records)
                    if (!res.Any(t => t.Month == r.Time.Month && t.Year == r.Time.Year && t.Day == r.Time.Day))
                        res.Add(new DateTime(r.Time.Year, r.Time.Month, r.Time.Day));

                return res;
            }
        }

        public WeatherStatistics GetData(DateTime startTime, DateTime endTime)
        {
            var records = new List<StatisticRecord>();

            foreach (var r in Records)
                if (r.Time >= startTime && r.Time < endTime)
                    records.Add(r);

            return new WeatherStatistics(records);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    abstract class BaseTask
    {
        protected WeatherStatistics Statistics { get; set; }

        public BaseTask(WeatherStatistics statistics)
        {
            this.Statistics = statistics;
        }

        public abstract List<string> Solve();

        protected StatisticRecord HottestTimeRecord(DateTime day, WeatherStatistics stat)
        {
            var dayStat = stat.GetData(day, day.AddDays(1));
            var hotestRecord = dayStat.Records.OrderByDescending(r => r.OutsideTemperature).First();
            return hotestRecord;
        }
    }
}

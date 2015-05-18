using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class StatisticRecord
    {
        public DateTime Time { get; set; }

        public double TempHumidityIndex { get; set; }

        public double OutsideTemperature { get; set; }

        public double WindChill { get; set; }

        public double HiTemperature { get; set; }

        public double LowTemperature { get; set; }

        public double OutsideHumidity { get; set; }

        public double DewPoint { get; set; }

        public double WindSpeed { get; set; }

        public double High { get; set; }

        public WindDirection WindDirection { get; set; }

        public double Rain { get; set; }

        public double Barometer { get; set; }

        public double InsideTemperature { get; set; }

        public double InsideHumidity { get; set; }

        public int ArchivePeriod { get; set; }
    }
}

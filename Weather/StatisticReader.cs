using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class StatisticReader
    {
        const int DATE_INDEX = 0;
        const int TIME_INDEX = 1;
        const int TEMP_HUMIDITY_INDEX = 2;
        const int OUTSIDE_TEMPERATURE_INDEX = 3;
        const int WINDCHIL_INDEX = 4;
        const int HIGH_TEMPERATURE_INDEX = 5;
        const int LOW_TEMPERATURE_INDEX = 6;
        const int OUTSIDE_HUNIDITY_INDEX = 7;
        const int DEW_POINT_INDEX = 8;
        const int WIND_SPEED_INDEX = 9;
        const int HIGH_INDEX = 10;
        const int WIND_DIRECTION_INDEX = 11;
        const int RAIN_INDEX = 12;
        const int BAROMETER_INDEX = 13;
        const int INSIDE_TEMPERATURE_INDEX = 14;
        const int INSIDE_HUMIDITY_INDEX = 15;
        const int ARCHIVE_PERIOD = 16;

        event Action<int> Progressed;

        /// <summary>
        /// Constructor of the reader
        /// </summary>
        /// <param name="progressHandler"></param>
        public StatisticReader(Action<int> progressHandler)
        {
            Progressed += progressHandler;            
        }

        /// <summary>
        /// Read weather statistics from the file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public WeatherStatistics ReadFromFile(string fileName)
        {
            return new WeatherStatistics(ReadStatistic(fileName));
        }

        /// <summary>
        /// Parsing INPUT CSV file
        /// </summary>
        /// <param name="filePath">path of the file</param>
        /// <returns></returns>
        private List<StatisticRecord> ReadStatistic(string filePath)
        {
            int fileSize = (int)new FileInfo(filePath).Length;
            var reader = new StreamReader(filePath);
            int readCounter = 0;    // for progress event rise

            var res = new List<StatisticRecord>();

            for (int i = 0; ; i++)
            {
                var line = reader.ReadLine();

                if (i == 0)
                {
                    Progressed(0);
                    continue;
                }

                if (line == null)
                    break;

                readCounter += line.Length;

                var record = ProcessLine(line);

                res.Add(record);

                if (i % 100 == 0)
                    Progressed(readCounter * 100 / fileSize);
            }

            Progressed(100);

            return res;
        }

        /// <summary>
        /// Parsing line of the file
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private StatisticRecord ProcessLine(string line)
        {
            var arr = line.Split(',');
            var res = new StatisticRecord();
            var culture = new CultureInfo("en-GB");

            res.Time = DateTime.Parse(arr[DATE_INDEX], culture) + TimeSpan.Parse(arr[TIME_INDEX]);
            res.TempHumidityIndex = double.Parse(arr[TEMP_HUMIDITY_INDEX], culture);
            res.OutsideTemperature = double.Parse(arr[OUTSIDE_TEMPERATURE_INDEX], culture);
            res.WindChill = double.Parse(arr[WINDCHIL_INDEX], culture);
            res.HiTemperature = double.Parse(arr[HIGH_TEMPERATURE_INDEX], culture);
            res.LowTemperature = double.Parse(arr[LOW_TEMPERATURE_INDEX], culture);
            res.OutsideHumidity = double.Parse(arr[OUTSIDE_HUNIDITY_INDEX], culture);
            res.DewPoint = double.Parse(arr[DEW_POINT_INDEX], culture);
            res.WindSpeed = double.Parse(arr[WIND_SPEED_INDEX], culture);
            res.High = double.Parse(arr[HIGH_INDEX], culture);

            if (arr[WIND_DIRECTION_INDEX] == "---")
                res.WindDirection = WindDirection.None;
            else
                res.WindDirection = (WindDirection)Enum.Parse(typeof(WindDirection), arr[WIND_DIRECTION_INDEX]);

            res.Rain = double.Parse(arr[RAIN_INDEX], culture);
            res.Barometer = double.Parse(arr[BAROMETER_INDEX], culture);
            res.InsideTemperature = double.Parse(arr[INSIDE_TEMPERATURE_INDEX], culture);
            res.InsideHumidity = double.Parse(arr[INSIDE_HUMIDITY_INDEX], culture);
            res.ArchivePeriod = int.Parse(arr[ARCHIVE_PERIOD]);

            return res;
        }
    }
}

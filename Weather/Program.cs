using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class Program
    {
        const string FILENAME = "downloaded.csv";
        const string REPORT_1a = "1a.txt";
        const string REPORT_1b = "1b.txt";
        const string REPORT_1c = "1c.txt";
        const string REPORT_2 = "2.txt";
        const string REPORT_3 = "3.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Downloading file...");

            var client = new WebClient();
            client.DownloadFile(ConfigurationManager.AppSettings["url"], FILENAME);

            Console.WriteLine(string.Format("File downloaded (size = {0} Kb)", new FileInfo(FILENAME).Length / 1024));

            Console.WriteLine("File processing:");
            var reader = new StatisticReader(ProgressHandler);
            var stat = reader.ReadFromFile(FILENAME);

            Report(new Task1a(stat), REPORT_1a);
            Report(new Task1b(stat), REPORT_1b);
            Report(new Task1c(stat), REPORT_1c);

            Report(new Task2(stat), REPORT_2);
            Report(new Task3(stat), REPORT_3);

            Console.WriteLine(string.Format("\nFileProcessed {0} records!", stat.Records.Count));

            Console.WriteLine(string.Format("Reports saved in files '{0}', '{1}, '{2}', '{3}' and '{4}'", REPORT_1a, REPORT_1b, REPORT_1c, REPORT_2, REPORT_3));

            Console.ReadLine();
        }

        /// <summary>
        /// Creates report for base task
        /// </summary>
        /// <param name="task"></param>
        /// <param name="fileName"></param>
        private static void Report(BaseTask task, string fileName)
        {
            var writer = CreateWriter(fileName);
            var solution = task.Solve();

            foreach (var line in solution)
                writer.WriteLine(line);

            writer.Close();
        }

        private static StreamWriter CreateWriter(string filePath)
        {
            var writer = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write));
            return writer;
        }

        private static void ProgressHandler(int persentage)
        {
            Console.Write("\r{0}%", persentage);
        }

        
    }
}

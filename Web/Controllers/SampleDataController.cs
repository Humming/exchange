using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }


        [HttpGet("[action]")]
        public IEnumerable<ChartData> GetChartData(int startDateIndex)
        {
            List<ChartData> data = new List<ChartData>();
            /*data.Add(new ChartData() { Date = "1234", Open = 1, High = 2, Low = 0, Close = 1, Volume = 100 });
            return data;*/
            string executableLocation = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            string csvLocation = Path.Combine(executableLocation, "Data\\bitfinex_xbtusd_1m.csv");

            using (var reader = new StreamReader(csvLocation))
            {

                int counter = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.Contains("date"))
                        continue;
                    ++counter;
                    if (counter == 10)
                        break;
                    var values = line.Split(',');
                    //DateTime time;
                    //DateTime.TryParse(values[0], out time);
                    data.Add(new ChartData()
                    {
                        Date = values[0],
                        Open = decimal.Parse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture),
                        High = decimal.Parse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture),
                        Low = decimal.Parse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture),
                        Close = decimal.Parse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture),
                        Volume = decimal.Parse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture)
                    });
                }

            }

            return data;
        }

        public class ChartData
        {
            public string Date { get; set; }
            public decimal Open { get; set; }
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public decimal Close { get; set; }
            public decimal Volume { get; set; }

        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}

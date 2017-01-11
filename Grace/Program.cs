using Grace.Helper;
using Grace.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Grace.Computation;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Grace
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_Import();
            Shortest();

            ReadKey();
            ReadKey();
            ReadKey();
            ReadKey();
        }

        static void Test_Degree()
        {
            var sw = new Stopwatch();
            sw.Start();
            var fullUri = @"C:\Users\Shuxiao\Desktop\short.json";
            String reader1 = File.ReadAllText(fullUri);
            var result = JArray.Parse(reader1).Children().ToList();
            var shorts = new List<Shortest>();
            foreach (var item in result)
            {
                var shortest = JsonConvert.DeserializeObject<Shortest>(item.ToString());
                shorts.Add(shortest);
            }
            sw.Stop();
            WriteLine($"读入最短路数据耗时：{sw.ElapsedMilliseconds} 毫秒。");

            sw = new Stopwatch();
            sw.Start();
            var cities2 = new List<City>();
            String reader2 = File.ReadAllText(@"C:\Users\Shuxiao\Desktop\json.json");
            {
                var res1 = JArray.Parse(reader2);
                var res2 = res1.Children();
                var res3 = res2.ToList();
                foreach (var item in res3)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                }
            }
            sw.Stop();
            WriteLine($"读入城市数据耗时：{sw.ElapsedMilliseconds} 毫秒。");


            sw = new Stopwatch();
            sw.Start();
            var strings = new List<String>();
            foreach (var item in shorts)
            {
                if (item.InterCities.Count > 0)
                {
                    foreach (var item2 in item.InterCities)
                    {
                        cities2[item2].Bi += 1;
                    }
                }
                strings.Add($"城市 {item.StartCity} 与城市 {item.EndCity} 之间的最短路：{String.Join(",", item.InterCities)}");
            }
            using (StreamWriter file = new StreamWriter(@"C:\Users\Shuxiao\Desktop\CityShort.json", true))
            {
                file.WriteLine(JsonConvert.SerializeObject(strings));
            }
            sw.Stop();
            WriteLine($"求度耗时：{sw.ElapsedMilliseconds} 毫秒。");


            var Strings2 = new List<String>();
            foreach (var item in cities2)
            {
                Strings2.Add($"城市 {item.Name} - {item.No} 的介数： {item.Bi}");
            }
            using (StreamWriter file = new StreamWriter(@"C:\Users\Shuxiao\Desktop\CityBi.json", true))
            {
                file.WriteLine(JsonConvert.SerializeObject(Strings2));
            }
        }

        static void Test_Import()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\Data\\test2.xlsx");
            var cities = Import.FromExcel(fullUri);
            sw.Stop();
            WriteLine($"导入数据用时{sw.ElapsedMilliseconds / 1000}秒");
            sw = new Stopwatch();
            sw.Start();
            using (StreamWriter file = new StreamWriter($"{Environment.CurrentDirectory}\\Data\\city.json", false))
            {
                var json = JsonConvert.SerializeObject(cities);
                file.WriteLine(json);
            }
            sw.Stop();
            WriteLine($"序列化和写入数据用时{sw.ElapsedMilliseconds}毫秒");
        }

        public static void Shortest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var jsonUri = $"{Environment.CurrentDirectory}\\Data\\city.json";
            var cities = Import.FromJson(jsonUri);
            var result = FLOYD.Short(cities);
            sw.Stop();
            WriteLine($"导入数据用时{sw.ElapsedMilliseconds / 1000}秒");
            sw = new Stopwatch();
            sw.Start();
            using (StreamWriter file = new StreamWriter($"{Environment.CurrentDirectory}\\Data\\short.json", false))
            {
                var json = JsonConvert.SerializeObject(result);
                file.WriteLine(json);
            }
            sw.Stop();
            WriteLine($"序列化时间{sw.ElapsedMilliseconds}毫秒");
        }

    }
}

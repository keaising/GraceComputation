using Grace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grace.Computation
{
    public class FLOYD
    {
        public static List<Shortest> Short(List<City> cities)
        {
            var spot = new List<Shortest>();// 定义任意两点之间经过的点

            foreach (var city in cities)
            {
                foreach (var relationCity in city.Distances)
                {
                    //if (city.No == relationCity.Key)
                    //{
                    //    var newShortest = new Shortest();
                    //    newShortest.StartCity = city.No;
                    //    newShortest.EndCity = city.No;
                    //    newShortest.Distance = 0;
                    //    newShortest.NextCity = 0;
                    //    spot.Add(newShortest);
                    //}
                    //if (!spot.Any(s => (s.StartCity == relationCity.Key && s.EndCity == city.No)))
                    //{
                    var newShortest = new Shortest();
                    newShortest.StartCity = city.No;
                    newShortest.EndCity = relationCity.Key;
                    newShortest.Distance = relationCity.Value; // 假设任意两点之间的没有路径
                    newShortest.NextCity = relationCity.Key;
                    spot.Add(newShortest);
                    //}
                }
            }

            Parallel.For(1, cities.Count, (i) =>
            {
                foreach (var city in cities)
                {
                    foreach (var relationCity in city.Distances)
                    {
                        var d1 = spot.GetShortest(city.No, relationCity.Key).Distance;
                        var d2 = spot.GetShortest(city.No, i).Distance;
                        var d3 = spot.GetShortest(i, relationCity.Key).Distance;
                        if (d1 > (d2 + d3))
                        {
                            spot.GetShortest(city.No, relationCity.Key).Distance = d2 + d3; // 如果存在更短路径则取更短路径
                            spot.GetShortest(city.No, relationCity.Key).NextCity = spot.GetShortest(city.No, i).NextCity;  // 把经过的点加入
                        }
                    }
                }
                Console.WriteLine($"————————————第 {i} 行数据——————————");
            });

            //for (int i = 1; i <= cities.Count; i++)
            //{
            //    foreach (var city in cities)
            //    {
            //        foreach (var relationCity in city.Distances)
            //        {
            //            var d1 = spot.GetShortest(city.No, relationCity.Key).Distance;
            //            var d2 = spot.GetShortest(city.No, i).Distance;
            //            var d3 = spot.GetShortest(i, relationCity.Key).Distance;
            //            if (d1 > (d2 + d3))
            //            {
            //                spot.GetShortest(city.No, relationCity.Key).Distance = d2 + d3; // 如果存在更短路径则取更短路径
            //                spot.GetShortest(city.No, relationCity.Key).NextCity = spot.GetShortest(city.No, i).NextCity;  // 把经过的点加入
            //            }
            //        }
            //    }
            //    Console.WriteLine($"————————————第 {i} 行数据——————————");
            //}



            foreach (var sho in spot)
            {
                var index = sho.StartCity;
                while (index != sho.EndCity)
                {
                    var interCity = spot.GetShortest(index, sho.EndCity).NextCity;
                    sho.InterCities.Add(interCity);
                    index = interCity;
                }
                if (sho.InterCities != null && sho.InterCities.Count > 0 && sho.InterCities.Last() == sho.EndCity)
                {
                    sho.InterCities.Remove(sho.EndCity);
                }
            }
            return spot;
        }
    }

    public static class ShortestHelper
    {
        public static Shortest GetShortest(this List<Shortest> shorts, Int32 startCityNo, Int32 endCityNo)
        {
            var res = shorts.FirstOrDefault(s => s.StartCity == startCityNo && s.EndCity == endCityNo);
            if (res == null)
            {
                //res = shorts.FirstOrDefault(s => s.StartCity == endCityNo && s.EndCity == startCityNo);
                //if (res == null)
                //{
                throw new ArgumentNullException($"找不到这样的最短路，城市1：{startCityNo}，城市2：{endCityNo}。");
                //}
            }
            return res;
        }
    }
}

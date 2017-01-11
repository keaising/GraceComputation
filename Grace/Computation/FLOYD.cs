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
        public static List<Shortest> Short (List<City> cities)
        {
            //int MAX = 1000000;
            //int row = cities.Count;// 图G的行数
            var spot = new List<Shortest>();// 定义任意两点之间经过的点
            //var onePath = new List<int>();// 记录一条路径
            //for (int i = 0; i < row; i++)// 处理图两点之间的路径
            //    for (int j = 0; j < row; j++)
            //    {
            //        if (G[i][j] == 0) G[i][j] = MAX;// 没有路径的两个点之间的路径为默认最大
            //        if (i == j) G[i][j] = 0;// 本身的路径长度为0
            //    }

            //init
            foreach (var city in cities)
            {
                foreach (var relationCity in city.Distances)
                {
                    if (city.No == relationCity.Key)
                    {
                        var newShortest = new Shortest();
                        newShortest.StartCity = city.No;
                        newShortest.EndCity = city.No;
                        newShortest.Distance = 0;
                        spot.Add(newShortest);
                    }
                    if (!spot.Any(s => (s.StartCity == relationCity.Key && s.EndCity == city.No) ||
                                       (s.StartCity == city.No && s.EndCity == relationCity.Key)))
                    {
                        var newShortest = new Shortest();
                        newShortest.StartCity = city.No;
                        newShortest.EndCity = relationCity.Key;
                        newShortest.Distance = relationCity.Value; // 假设任意两点之间的没有路径
                        spot.Add(newShortest);
                    }
                }
            }
            #region
            //for (int i = 0; i < row; i++)// 假设任意两点之间的没有路径
            //    onePath[i] = -1;
            //for (int v = 0; v < row; ++v)
            //    for (int w = 0; w < row; ++w)
            //        length[v][w] = G[v][w];
            //for (int u = 0; u < row; ++u)
            //    for (int v = 0; v < row; ++v)
            //        for (int w = 0; w < row; ++w)
            //            if (length[v][w] > length[v][u] + length[u][w])
            //            {
            //                length[v][w] = length[v][u] + length[u][w];// 如果存在更短路径则取更短路径
            //                spot[v][w] = u;// 把经过的点加入
            //            }
            #endregion
            for (int i = 1; i <= cities.Count; i++)
            {
                foreach (var city in cities)
                {
                    foreach (var relationCity in city.Distances)
                    {

                        if (spot.GetShortest(city.No, relationCity.Key).Distance >
                            (spot.GetShortest(city.No, i).Distance + spot.GetShortest(i, relationCity.Key).Distance))
                        {
                            spot.GetShortest(city.No, relationCity.Key).Distance =
                                spot.GetShortest(city.No, i).Distance + spot.GetShortest(i, relationCity.Key).Distance; // 如果存在更短路径则取更短路径
                            spot.GetShortest(city.No, relationCity.Key).InterCities.Add(i);  // 把经过的点加入
                        }
                    }
                }
                Console.WriteLine($"————————————第 {i} 行数据——————————");
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
                res = shorts.FirstOrDefault(s => s.StartCity == endCityNo && s.EndCity == startCityNo);
                if (res == null)
                {
                    Console.WriteLine($"找不到这样的最短路，城市1：{startCityNo}，城市2：{endCityNo}。");
                    throw new ArgumentNullException();
                }
            }
            return res;
        }
    }
}

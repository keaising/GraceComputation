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

namespace Grace
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var jsonUri = @"C:\Users\Shuxiao\Desktop\json.json";
            var cities = Import.FromJson(jsonUri);
            var result = FLOYD.Short(cities);
            sw.Stop();
            WriteLine($"导入数据用时{sw.ElapsedMilliseconds / 1000}秒");
            sw = new Stopwatch();
            sw.Start();
            using (StreamWriter file = new StreamWriter(@"C:\Users\Shuxiao\Desktop\short.json", true))
            {
                var json = JsonConvert.SerializeObject(result);
                file.WriteLine(json);
            }
            sw.Stop();
            WriteLine($"序列化时间{sw.ElapsedMilliseconds}毫秒");
            ReadKey();
            ReadKey();
            ReadKey();
            ReadKey();
            ReadKey();
        }

        static void Test_Import()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var fullUri = @"C:\Users\Shuxiao\Documents\visual studio 2015\Projects\Grace\Grace\bin\Debug\test2.xlsx";
            var cities = Import.FromExcel(fullUri);
            sw.Stop();
            WriteLine($"导入数据用时{sw.ElapsedMilliseconds / 1000}秒");
            sw = new Stopwatch();
            sw.Start();
            using (StreamWriter file = new StreamWriter(@"C:\Users\Shuxiao\Desktop\json.json", true))
            {
                var json = JsonConvert.SerializeObject(cities);
                file.WriteLine(json);
            }
            sw.Stop();
            WriteLine($"序列化和写入数据用时{sw.ElapsedMilliseconds}毫秒");
            sw = new Stopwatch();
            sw.Start();
            var cities2 = new List<City>();
            String reader = File.ReadAllText(@"C:\Users\Shuxiao\Desktop\json.json");
            {
                var res1 = JArray.Parse(reader);
                var res2 = res1.Children();
                var res3 = res2.ToList();
                foreach (var item in res3)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                }
            }
            sw.Stop();
            WriteLine($"反序列化和读入数据用时{sw.ElapsedMilliseconds}毫秒");
        }

    }

    #region FLYOD
    ////以无向图G为入口，得出任意两点之间的路径长度length[i][j]，路径path[i][j][k]，
    ////途中无连接得点距离用0表示，点自身也用0表示
    //public class FLOYD
    //{
    //    int[][] length = null;// 任意两点之间路径长度
    //    int[][][] path = null;// 任意两点之间的路径
    //    public FLOYD(int[][] G)
    //    {
    //        int MAX = 100;
    //        int row = G.Length;// 图G的行数
    //        int[, ] spot = new int[row, row];// 定义任意两点之间经过的点
    //        int[] onePath = new int[row];// 记录一条路径
    //        length = new int[row][];
    //        path = new int[row][row][];
    //        for (int i = 0; i < row; i++)// 处理图两点之间的路径
    //            for (int j = 0; j < row; j++)
    //            {
    //                if (G[i][j] == 0) G[i][j] = MAX;// 没有路径的两个点之间的路径为默认最大
    //                if (i == j) G[i][j] = 0;// 本身的路径长度为0
    //            }
    //        for (int i = 0; i < row; i++)// 初始化为任意两点之间没有路径
    //            for (int j = 0; j < row; j++)
    //                spot[i][j] = -1;
    //        for (int i = 0; i < row; i++)// 假设任意两点之间的没有路径
    //            onePath[i] = -1;
    //        for (int v = 0; v < row; ++v)
    //            for (int w = 0; w < row; ++w)
    //                length[v][w] = G[v][w];
    //        for (int u = 0; u < row; ++u)
    //            for (int v = 0; v < row; ++v)
    //                for (int w = 0; w < row; ++w)
    //                    if (length[v][w] > length[v][u] + length[u][w])
    //                    {
    //                        length[v][w] = length[v][u] + length[u][w];// 如果存在更短路径则取更短路径
    //                        spot[v][w] = u;// 把经过的点加入
    //                    }
    //        for (int i = 0; i < row; i++)
    //        {// 求出所有的路径
    //            int[] point = new int[1];
    //            for (int j = 0; j < row; j++)
    //            {
    //                point[0] = 0;
    //                onePath[point[0]++] = i;
    //                outputPath(spot, i, j, onePath, point);
    //                path[i][j] = new int[point[0]];
    //                for (int s = 0; s < point[0]; s++)
    //                    path[i][j][s] = onePath[s];
    //            }
    //        }
    //    }
    //    void outputPath(int[][] spot, int i, int j, int[] onePath, int[] point)
    //    {// 输出i// 到j// 的路径的实际代码，point[]记录一条路径的长度
    //        if (i == j) return;
    //        if (spot[i][j] == -1)
    //            onePath[point[0]++] = j;
    //        // System.out.print(" "+j+" ");
    //        else
    //        {
    //            outputPath(spot, i, spot[i][j], onePath, point);
    //            outputPath(spot, spot[i][j], j, onePath, point);
    //        }
    //    }
    #endregion
}

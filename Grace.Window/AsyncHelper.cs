using Grace.Computation;
using Grace.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grace.Window
{
    public class AsyncHelper
    {
        public static async Task<Int32> Compute()
        {
            return await Task.Run(()=> {
                var sw = new Stopwatch();
                sw.Start();
                var fullUri = string.Format(Application.StartupPath + "\\Data\\test2.xlsx");
                var cities = Import.FromExcel(fullUri);
                var newfile = string.Format(Application.StartupPath + "\\Data\\city.json");
                using (StreamWriter file = new StreamWriter(newfile, false))
                {
                    var json = JsonConvert.SerializeObject(cities);
                    file.WriteLine(json);
                }

                var result = FLOYD.Short(cities);
                var newFile = string.Format(Application.StartupPath + "\\Data\\short.json");
                using (StreamWriter file = new StreamWriter(newFile, false))
                {
                    var json = JsonConvert.SerializeObject(result);
                    file.WriteLine(json);
                }
                sw.Stop();
                return (Int32)(sw.ElapsedMilliseconds / 1000);
            });
        }
    }
}

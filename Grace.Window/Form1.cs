using Grace.Computation;
using Grace.Helper;
using Grace.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;

namespace Grace.Window
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(Object sender, EventArgs e)
        {
            //初始化一个OpenFileDialog类 
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = $"{Application.StartupPath}";
            //判断用户是否正确的选择了文件 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择文件的后缀名 
                string fullUri = fileDialog.FileName;
                textBox1.Text = fullUri;
            }
        }

        private void button2_Click(Object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();
            button2.Text = "需要12分钟";
            var fullUri = textBox1.Text;
            textBox1.Text += string.Format(@"    现在时间：" + DateTime.Now);
            
            var cities = Import.FromExcel(fullUri);
            var newfile = string.Format(Application.StartupPath + "\\Data\\city.json");
            using (StreamWriter file = new StreamWriter(newfile, false))
            {
                var json = JsonConvert.SerializeObject(cities);
                file.WriteLine(json);
            }
            sw.Stop();
            textBox1.Text += string.Format(@"    完成时间：" + (DateTime.Now));
            MessageBox.Show($"任务完成! 耗时{sw.ElapsedMilliseconds / 1000} 秒");
        }

        private void button3_Click(Object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = $"{Application.StartupPath}";
            //判断用户是否正确的选择了文件 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择文件的后缀名 
                string fullUri = fileDialog.FileName;
                textBox2.Text = fullUri;
            }

        }

        private void button8_Click(Object sender, EventArgs e)
        {
            var newFile = string.Format(Application.StartupPath + "\\Data\\city.json");
            textBox2.Text = newFile;
        }

        private void button4_Click(Object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var jsonUri = textBox2.Text;
            textBox2.Text += string.Format(@"    现在时间：" + DateTime.Now);
            var cities = Import.FromJson(jsonUri);
            var result = FLOYD.Short(cities);
            var newFile = string.Format(Application.StartupPath + "\\Data\\short.json");
            using (StreamWriter file = new StreamWriter(newFile, false))
            {
                var json = JsonConvert.SerializeObject(result);
                file.WriteLine(json);
            }
            sw.Stop();
            textBox2.Text += string.Format(@"    完成时间：" + DateTime.Now);
            MessageBox.Show($"总耗时：{sw.ElapsedMilliseconds / 1000} 秒");
        }

        private void button5_Click(Object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = $"{Application.StartupPath}";
            //判断用户是否正确的选择了文件 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择文件的后缀名 
                string fullUri = fileDialog.FileName;
                textBox3.Text = fullUri;
            }
        }

        private void button6_Click(Object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = $"{Application.StartupPath}";
            //判断用户是否正确的选择了文件 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择文件的后缀名 
                string fullUri = fileDialog.FileName;
                textBox4.Text = fullUri;
            }
        }

        private void button9_Click(Object sender, EventArgs e)
        {
            var newFile = string.Format(Application.StartupPath + "\\Data\\city.json");
            textBox3.Text = newFile;
        }

        private void button10_Click(Object sender, EventArgs e)
        {
            var newFile = string.Format(Application.StartupPath + "\\Data\\short.json");
            textBox4.Text = newFile;
        }

        /// <summary>
        /// 计算城市介数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(Object sender, EventArgs e)
        {
            var shortJson = textBox4.Text;
            String reader1 = File.ReadAllText(shortJson);
            var result1 = JArray.Parse(reader1).Children().ToList();
            var shorts = new List<Shortest>();
            foreach (var item in result1)
            {
                var shortest = JsonConvert.DeserializeObject<Shortest>(item.ToString());
                shorts.Add(shortest);
            }

            var cities2 = new List<City>();
            var cityJson = textBox3.Text;
            String reader2 = File.ReadAllText(cityJson);
            {
                var result2 = JArray.Parse(reader2).Children().ToList();
                foreach (var item in result2)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                }
            }

            var strings = new List<String>();
            foreach (var item in shorts)
            {
                if (item.InterCities.Count > 0)
                {
                    foreach (var item2 in item.InterCities)
                    {
                        cities2[item2 - 1].Bi += 1;
                    }
                }
                strings.Add($"城市 {item.StartCity} 与城市 {item.EndCity} 之间的最短路：{String.Join(",", item.InterCities)}");
            }
            var cityShort = string.Format(Application.StartupPath + "\\Data\\CityShort.json");
            using (StreamWriter file = new StreamWriter(cityShort, false))
            {
                file.WriteLine(JsonConvert.SerializeObject(strings));
            }


            var Strings2 = new List<String>();
            foreach (var item in cities2)
            {
                Strings2.Add($"城市 {item.Name} - {item.No} 的介数： {item.Bi}");
            }
            var cityBi = string.Format(Application.StartupPath + "\\Data\\CityBi.json");
            using (StreamWriter file = new StreamWriter(cityBi, true))
            {
                file.WriteLine(JsonConvert.SerializeObject(Strings2));
            }
        }

        /// <summary>
        /// 输出度分布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(Object sender, EventArgs e)
        {
            var cities2 = new List<City>();
            var cityJson = textBox3.Text;
            String reader2 = File.ReadAllText(cityJson);
            {
                var result2 = JArray.Parse(reader2).Children().ToList();
                foreach (var item in result2)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    //判断是否连接的条件
                    city.Degree = city.Distances.Select(c => c.Value > 0 && c.Value < 100000).Count();
                    cities2.Add(city);
                }
            }

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("城市名称");
            row0.CreateCell(1).SetCellValue("城市编号");
            row0.CreateCell(2).SetCellValue("度");

            foreach(var city in cities2)
            {
                IRow row = sheet1.CreateRow(city.No);
                row.CreateCell(0).SetCellValue(city.Name);
                row.CreateCell(1).SetCellValue(city.No);
                row.CreateCell(2).SetCellValue(city.Degree);
            }
            var newFile = string.Format(Application.StartupPath + "\\Data\\度-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            var dia = MessageBox.Show($"求【城市的度】完成！文件在{newFile}");

        }

        /// <summary>
        /// 强度分布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(Object sender, EventArgs e)
        {
            var cities2 = new List<City>();
            var cityJson = textBox3.Text;
            String reader2 = File.ReadAllText(cityJson);
            {
                var result2 = JArray.Parse(reader2).Children().ToList();
                foreach (var item in result2)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                }
            }

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("城市名称");
            row0.CreateCell(1).SetCellValue("城市编号");
            row0.CreateCell(2).SetCellValue("si");
            row0.CreateCell(3).SetCellValue("Csi");



            var W = 0.0;
            foreach (var city in cities2)
            {
                foreach (var item in city.Distances)
                {
                    if (item.Value > 0 && item.Value < 90000)
                    {
                        W += 1 / item.Value;
                    }
                }
            }

            var sis = new List<Double>();
            foreach (var city in cities2)
            {
                var si = 0.0;
                foreach (var item in city.Distances)
                {
                    if (item.Value > 0 && item.Value < 90000)
                    {
                        si += 1 / item.Value;
                        sis.Add(si);
                    }
                }
                var Csi = si / W;
                IRow row = sheet1.CreateRow(city.No);
                row.CreateCell(0).SetCellValue(city.Name);
                row.CreateCell(1).SetCellValue(city.No);
                row.CreateCell(2).SetCellValue(si);
                row.CreateCell(3).SetCellValue(Csi);
            }

            ISheet sheet2 = workbook.CreateSheet("Sheet2");
            var row1Sheet2 = sheet2.CreateRow(0);
            row1Sheet2.CreateCell(0).SetCellValue("si");
            row1Sheet2.CreateCell(1).SetCellValue("次数");
            var times = new Dictionary<Int32, Int32>();
            foreach (var item in sis)
            {
                var ceil = (Int32)Math.Ceiling(item);
                if (times.Keys.Contains(ceil))
                {
                    times[ceil] += 1;
                }
                else
                {
                    times.Add(ceil, 1);
                }
            }
            times = times.OrderBy(t => t.Key).ToDictionary(t => t.Key, t => t.Value);
            var count = 1;
            foreach (var item in times)
            {
                IRow row = sheet2.CreateRow(count);
                row.CreateCell(0).SetCellValue(item.Key);
                row.CreateCell(1).SetCellValue(item.Value);
                count += 1;
            }

            var newFile = string.Format(Application.StartupPath + "\\Data\\Csi-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            var dia = MessageBox.Show($"【Csi】完成！文件在{newFile}");
        }

        /// <summary>
        /// Wij 分布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(Object sender, EventArgs e)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("城市名称");
            row0.CreateCell(1).SetCellValue("城市编号");
            row0.CreateCell(2).SetCellValue("Wij");

            var cities2 = new List<City>();
            var cityJson = textBox3.Text;
            var wijs = new List<Double>();
            String reader2 = File.ReadAllText(cityJson);
            {
                var result2 = JArray.Parse(reader2).Children().ToList();
                foreach (var item in result2)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                    var Wij = city.Distances.Where(d => d.Value > 0 && d.Value < 90000).Sum(d => d.Value);
                    IRow row = sheet1.CreateRow(city.No);
                    row.CreateCell(0).SetCellValue(city.Name);
                    row.CreateCell(1).SetCellValue(city.No);
                    row.CreateCell(2).SetCellValue(Wij);
                    wijs.Add(Wij);
                }
            }

            #region 计算次数
            ISheet sheet2 = workbook.CreateSheet("Sheet2");
            var row1Sheet2 = sheet2.CreateRow(0);
            row1Sheet2.CreateCell(0).SetCellValue("Wij");
            row1Sheet2.CreateCell(1).SetCellValue("次数");
            var times = new Dictionary<Int32, Int32>();
            foreach (var item in wijs)
            {
                var ceil = (Int32)Math.Ceiling(item / 500); //如果输出参数不合适，调整这里
                if (times.Keys.Contains(ceil))
                {
                    times[ceil] += 1;
                }
                else
                {
                    times.Add(ceil, 1);
                }
            }
            times = times.OrderBy(t => t.Key).ToDictionary(t => t.Key, t => t.Value);
            var count = 1;
            foreach (var item in times)
            {
                IRow row = sheet2.CreateRow(count);
                row.CreateCell(0).SetCellValue(item.Key);
                row.CreateCell(1).SetCellValue(item.Value);
                count += 1;
            }
            #endregion

            var newFile = string.Format(Application.StartupPath + "\\Data\\Wij-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            var dia = MessageBox.Show($"【Wij】完成！文件在{newFile}");
        }

        private void button14_Click(Object sender, EventArgs e)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow row0 = sheet1.CreateRow(0);
            //row0.CreateCell(0).SetCellValue("城市名称");
            row0.CreateCell(0).SetCellValue("城市编号");
            row0.CreateCell(1).SetCellValue("Cki");

            var cities2 = new List<City>();
            var cityJson = textBox3.Text;
            String reader2 = File.ReadAllText(cityJson);
            {
                var result2 = JArray.Parse(reader2).Children().ToList();
                foreach (var item in result2)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                }
            }
            var Ckis = CkiHelper.CkiList(cities2);
            var count = 1;
            Ckis = Ckis.OrderBy(c => c.Key).ToDictionary(c => c.Key, c => c.Value);
            foreach (var item in Ckis)
            {
                IRow row = sheet1.CreateRow(count);
                row.CreateCell(0).SetCellValue(item.Key);
                row.CreateCell(1).SetCellValue(item.Value);
                count += 1;
            }

            var newFile = string.Format(Application.StartupPath + "\\Data\\Cki-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            var dia = MessageBox.Show($"【Cki】完成！文件在{newFile}");
        }

        /// <summary>
        /// Cbi 介数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(Object sender, EventArgs e)
        {
            var shortJson = textBox4.Text;
            String reader1 = File.ReadAllText(shortJson);
            var result1 = JArray.Parse(reader1).Children().ToList();
            var shorts = new List<Shortest>();
            foreach (var item in result1)
            {
                var shortest = JsonConvert.DeserializeObject<Shortest>(item.ToString());
                shorts.Add(shortest);
            }

            var cities2 = new List<City>();
            var cityJson = textBox3.Text;
            String reader2 = File.ReadAllText(cityJson);
            {
                var result2 = JArray.Parse(reader2).Children().ToList();
                foreach (var item in result2)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                }
            }
            foreach (var item in shorts)
            {
                if (item.InterCities.Count > 0)
                {
                    foreach (var item2 in item.InterCities)
                    {
                        cities2[item2 - 1].Bi += 1;
                    }
                }
            }

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Bi");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("城市名称");
            row0.CreateCell(1).SetCellValue("城市编号");
            row0.CreateCell(2).SetCellValue("介数");
            var count = 1;
            cities2.OrderBy(c => c.No);
            foreach (var item in cities2)
            {
                IRow row = sheet1.CreateRow(count);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.No);
                row.CreateCell(2).SetCellValue(item.Bi);
                count += 1;
            }

            ISheet sheet2 = workbook.CreateSheet("Cbi");
            IRow row1 = sheet2.CreateRow(0);
            row1.CreateCell(0).SetCellValue("城市编号");
            row1.CreateCell(1).SetCellValue("Cbi");
            var Cbis = CbiHelper.CbiList(cities2);
            var count2 = 1;
            foreach (var item in Cbis)
            {
                IRow row = sheet2.CreateRow(count2);
                row.CreateCell(0).SetCellValue(item.Key);
                row.CreateCell(1).SetCellValue(item.Value);
                count2 += 1;
            }
            var newFile = string.Format(Application.StartupPath + "\\Data\\Cbi-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            var dia = MessageBox.Show($"【Cbi】完成！文件在{newFile}");

        }

        private void button17_Click(Object sender, EventArgs e)
        {
            var shortJson = textBox4.Text;
            String reader1 = File.ReadAllText(shortJson);
            var result1 = JArray.Parse(reader1).Children().ToList();
            var shorts = new List<Shortest>();
            foreach (var item in result1)
            {
                var shortest = JsonConvert.DeserializeObject<Shortest>(item.ToString());
                shorts.Add(shortest);
            }

            var cities2 = new List<City>();
            var cityJson = textBox3.Text;
            String reader2 = File.ReadAllText(cityJson);
            {
                var result2 = JArray.Parse(reader2).Children().ToList();
                foreach (var item in result2)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                }
            }

            foreach (var item in shorts)
            {
                if (item.InterCities.Count > 0)
                {
                    foreach (var item2 in item.InterCities)
                    {
                        cities2[item2 - 1].Bi += 1;
                    }
                }
            }

            var CciW = CciHelper.CciW(cities2, shorts); //最短距离
            var Cci2 = CciHelper.Cci2(cities2, shorts); //边数

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Ci");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("城市编号");
            row0.CreateCell(1).SetCellValue("CciW - 最短距离");
            row0.CreateCell(2).SetCellValue("Cci2 - 边数");
            for (int i = 1; i <= Cci2.Count; i++)
            {
                IRow row = sheet1.CreateRow(i);
                row.CreateCell(0).SetCellValue(i);
                row.CreateCell(1).SetCellValue(CciW[i]);
                row.CreateCell(2).SetCellValue(Cci2[i]);
            }
            var newFile = string.Format(Application.StartupPath + "\\Data\\Cci-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            var dia = MessageBox.Show($"【Cci】完成！文件在{newFile}");
        }

        private void button18_Click(Object sender, EventArgs e)
        {
            var cities2 = new List<City>();
            var cityJson = textBox3.Text;
            String reader2 = File.ReadAllText(cityJson);
            {
                var result2 = JArray.Parse(reader2).Children().ToList();
                foreach (var item in result2)
                {
                    var city = JsonConvert.DeserializeObject<City>(item.ToString());
                    cities2.Add(city);
                }
            }
            var cei = CEi.List(cities2);

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("CEi");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("城市编号");
            row0.CreateCell(1).SetCellValue("CEi");
            var count = 1;
            foreach (var item in cei)
            {
                IRow row = sheet1.CreateRow(count);
                row.CreateCell(0).SetCellValue(item.Key);
                row.CreateCell(1).SetCellValue(item.Value);
                count += 1;
            }
            var newFile = string.Format(Application.StartupPath + "\\Data\\CEi-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            var dia = MessageBox.Show($"【Cci】完成！文件在{newFile}");
        }

        /// <summary>
        /// 一键导入城市和计算最短路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button15_Click(Object sender, EventArgs e)
        {
            var time = await AsyncHelper.Compute();
            MessageBox.Show($"任务完成! 耗时{time} 秒");
        }

    }
}

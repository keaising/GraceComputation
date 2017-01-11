using Grace.Computation;
using Grace.Helper;
using Grace.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            textBox1.Text += string.Format(@"    现在时间是" + DateTime.Now);
            textBox1.Text += string.Format(@"    预计完成时间：" + (DateTime.Now.AddMinutes(12)));
            var cities = Import.FromExcel(fullUri);
            var newfile = string.Format(Application.StartupPath + "\\Data\\city.json");
            using (StreamWriter file = new StreamWriter(newfile, false))
            {
                var json = JsonConvert.SerializeObject(cities);
                file.WriteLine(json);
            }
            sw.Stop();
            textBox1.Text += $"任务完成! 耗时{sw.ElapsedMilliseconds / 60000}分钟";
        }

        private void button3_Click(Object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

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
            var cities = Import.FromJson(jsonUri);
            var result = FLOYD.Short(cities);
            var newFile = string.Format(Application.StartupPath + "\\Data\\short.json");
            using (StreamWriter file = new StreamWriter(newFile, false))
            {
                var json = JsonConvert.SerializeObject(result);
                file.WriteLine(json);
            }
            sw.Stop();
            textBox2.Text += ($"总耗时：{sw.ElapsedMilliseconds / 60000} 分钟");
        }

        private void button5_Click(Object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

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
                        cities2[item2].Bi += 1;
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
    }
}

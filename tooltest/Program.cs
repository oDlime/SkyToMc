using System;
using System.Text.RegularExpressions;

namespace RegExApplication
{
    class KeyInfo
    {
        public int time;
        public int key;
        public string type;
        public KeyInfo(int time, int key, string type)
        {
            this.time = time;
            this.key = key;
            this.type = type;
        }
    }
    class Program
    {
        public static List<KeyInfo> keyInfoList = new List<KeyInfo>();
        public static List<string> redblock1 = new List<string>() { "~-6 ~10 ~-2","~-4 ~10 ~-4","~-6 ~10 ~-5","~-4 ~10 ~-7","~-3 ~10 ~-5","~-2 ~10 ~-8","~-1 ~10 ~-8","~0 ~10 ~-8","~1 ~10 ~-8","~2 ~10 ~-8","~3 ~10 ~-5","~4 ~10 ~-7","~6 ~10 ~-5","~4 ~10 ~-4","~6 ~10 ~-2" };
        public static List<string> redblock2 = new List<string>() { "~-8 ~10 ~-7", "~-6 ~10 ~-9", "~-5 ~10 ~-9", "~-4 ~10 ~-11", "~-3 ~10 ~-11", "~-2 ~10 ~-11", "~-1 ~10 ~-11", "~0 ~10 ~-11", "~1 ~10 ~-11", "~2 ~10 ~-11", "~3 ~10 ~-11", "~4 ~10 ~-11", "~5 ~10 ~-9", "~6 ~10 ~-9", "~8 ~10 ~-7" };
        static void Main(string[] args)
        {
            Console.WriteLine("尊重SkyStudio谱作者的知识版权，未经谱作者允许，禁止利用本软件进行任何商业用途！\r\n任何基于授权而产生的一切法律纠纷和责任均与本软件作者无关");
            Console.WriteLine("输入乐谱的绝对路径");
            string url = Console.ReadLine().Replace("\"","");
            //获取
            StreamReader sr = new StreamReader(url);
            string content = sr.ReadLine();
            sr.Close();
            content = content.Split('[')[2].Split(']')[0].Replace("{\"time\":","").Replace("\"key\":", "").Replace("}","").Replace("\"","");
            string[] items = content.Split(',');
            string time ,key, type;
            time = key = type = "";
            for (int i = 0; i < items.Length; i+=2)
            {
                keyInfoList.Add(new KeyInfo(Convert.ToInt32(items[i]), Convert.ToInt32(items[i + 1].Split('y')[1]), items[i + 1][0] + ""));
            }
            StreamWriter sw = new StreamWriter("main.mcfunction");
            sw.WriteLine("scoreboard players add @e[tag=mate] stime 1");
            sw.WriteLine("execute as @e[tag=mate,scores={thr=10}] at @e[tag=mate] run function mcstudio:clrred");
            for (int i = 0; i < keyInfoList.Count; i++)
            {
                string typea = keyInfoList[i].type == "1" ? redblock1[keyInfoList[i].key] : redblock2[keyInfoList[i].key];
                string line = "execute as @e[tag=mate,scores={stime=" + (int)keyInfoList[i].time / 50 + "}] at @e[tag=mate] run summon minecraft:falling_block "+typea+" {Time:1,DropItem:0b,BlockState:{Name:\"redstone_block\"}}";
                sw.WriteLine(line);
            }
            sw.Close();
            Console.WriteLine("end");
        }
    }
}
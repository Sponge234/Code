using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework
{
    class Program
    {
         static void Main()
         {
            //泛型链表类
            GenericList<int> list = new GenericList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            int maximum = list.Head.Value;
            int minimum = list.Head.Value;
            int sum = 0;
            list.ForEach(num => Console.WriteLine(num));
            list.ForEach(num =>
            {
                if (num > maximum)
                {
                    maximum = num;
                }
                if (num < minimum)
                {
                    minimum = num;
                }
                sum += num;
            }
            );
            Console.WriteLine(
                $"最大值: {maximum}\n" +
                $"最小值:{minimum}\n" +
                $"和:{sum}"
            );

            Console.WriteLine("\n");
            //闹钟事件
            Clock clock = new Clock();
            clock.Tick += (sender, e) => Console.WriteLine($"嘀嗒：{e.Time:HH:mm:ss}");
            clock.Alarm += (sender, e) => Console.WriteLine($"闹钟响起！，已到设定时间：{e.Time:HH:mm:ss}");

            var now = DateTime.Now;
            clock.SetAlarm(now.Hour, now.Minute, now.Second + 5);

            clock.Start();

            // 保持程序运行
            Console.WriteLine("按任意键停止...");
            Console.ReadKey();
            clock.Stop();

        }
    }
}

// See https://aka.ms/new-console-template for more information
using System;
namespace CS.chapter1
{
    public class Calculator
    {
        public double x, y;
        public char op;
        public Calculator(double x, double y, char op)
        {
            this.x = x;
            this.y = y;
            this.op = op;
        }

        public void Calculate()
        {
            double result = 0;
            switch (op)
            {
                case '+':
                    result = x + y;
                    break;
                case '-':
                    result = x - y;
                    break;
                case '*':
                    result = x * y;
                    break;
                case '/':
                    if (y == 0)
                    {
                        Console.Write("除数不为零！");
                        break;
                    }
                    result = x / y;
                    break;
            }
            Console.WriteLine($"结果为：{result}");
        }
    }
    class Test
    { 
        static void Main()
        {
            
             Console.WriteLine("请输入一个整数：(确认请按回车)");
             double _x = double.Parse(Console.ReadLine());
             Console.WriteLine("请输入运算符：(确认请按回车)");
             char _op = char.Parse(Console.ReadLine());
             Console.WriteLine("请输入一个整数：(确认请按回车)");
             double _y = double.Parse(Console.ReadLine());

             Calculator cal = new Calculator( _x, _y, _op ); 
             cal.Calculate();
            
            //Calculator result = new Calculator(double.Parse(args[0]), double.Parse(args[2]), char.Parse(args[1]))该方法错误;
        }
    }
}

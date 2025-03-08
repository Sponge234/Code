using System;
namespace hwk_2nd
{
    public class Methods
    {
        //第一问:分解质因数
        public void primeFactor(int n)
        {
            n++;
            bool[] nums = new bool[n];
            for (int i = 4; i < n; i += 2)
            {
                nums[i] = true;
            }
            for (int i = 3; i < Math.Sqrt(n); i += 2)
            {
                for (int j = i * i; j < n; j += i)
                {
                    if (j % i == 0) nums[j] = true;
                }
            }
            int original_n = --n;
            for (int i = 2; i <= n && i!= original_n;)
            {
                if (!nums[i])
                {
                    if (n % i == 0)
                    {
                        Console.WriteLine(i);
                        n /= i;
                    }
                    else i++;
                }
                else i++;
            }
        }

        //第二问:最大值、最小值、平均值、和
        public void Maximum(int[] arr)
        {
            int maximum = arr[0];
            foreach(int i in arr)
            {
                if(i > maximum) maximum = i;
            }
            Console.WriteLine(maximum);
        }

        public void Minimum(int[] arr)
        {
            int minimum = arr[0];
            foreach (int i in arr)
            {
                if(i < minimum) minimum = i;
            }
            Console.WriteLine(minimum);
        }

        public void Sum(int[] arr)
        {
            int sum = 0;
            foreach (int i in arr)
            {
                sum += i;
            }
            Console.WriteLine(sum);
        }

        public void Mean(int[] arr)
        {
            int sum = 0;
            foreach (int i in arr)
            {
                sum += i;
            }
            int mean = sum / arr.Length;
            Console.WriteLine(mean);
        }

        //第三问:埃拉托斯特尼筛法
        public void SieveOfEratosthenes(int n)
        {
            n++;
            bool[] nums = new bool[n];
            for (int i = 4; i < n; i += 2)
            {
                nums[i] = true;
            }
            for (int i = 3; i < Math.Sqrt(n); i += 2)
            {
                for (int j = i * i; j < n; j += i)
                {
                    if (j % i == 0) nums[j] = true;
                }
            }    
            for(int i = 2; i < n; i++)
            {
                if (!nums[i]) Console.WriteLine(i);
            }
        }

        //第四问:托普利茨矩阵
        public bool ToplitzMatrix(int[,] matrix)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            for(int i = 0; i < row - 1; i++)
            {
                for(int j = 0; j < col - 1; j++)
                {
                    if (matrix[i, j] != matrix[i + 1, j + 1]) return false;
                }
            }
            return true;
        }
    }

    class MainClass
    {
        static void Main()
        {
            while (true)
            {
                Methods methods = new Methods();

                ///*
                Console.WriteLine("请输入一个整数：");
                int n = int.Parse(Console.ReadLine());
                methods.primeFactor(n);
                //*/

                /*
                Console.WriteLine("请输入一个数组长度：");
                int len = int.Parse(Console.ReadLine());
                int[] arr = new int[len];
                Console.WriteLine($"请输入{len}个整数：(按Enter确认)");
                for (int i = 0; i < len; i++)
                {
                    arr[i] = int.Parse(Console.ReadLine());
                }
                methods.Maximum(arr);
                methods.Minimum(arr);
                methods.Mean(arr);
                methods.Sum(arr);  
                */

                /*
                Console.WriteLine("请输入一个整数：");
                int n = int.Parse(Console.ReadLine());
                methods.SieveOfEratosthenes(n);
                */

                /*
                Console.WriteLine("请输入一个矩阵的行数和列数：(输入行数后按Enter确认再输入列数)");
                int row = int.Parse(Console.ReadLine());
                int col = int.Parse(Console.ReadLine());
                int[,] matrix = new int[row, col];
                Console.WriteLine($"请按照先行后列顺序输入{row * col}个整数：(按Enter确认)");
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        matrix[i, j] = int.Parse(Console.ReadLine());
                    }
                }
                Console.WriteLine(methods.ToplitzMatrix(matrix));
                */
            }
        }
    }

}

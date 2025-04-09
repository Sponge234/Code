using System;
namespace GeometryFigure
{
    public interface IShape
    {
        public abstract double Area();
    }
    public class Rectangle : IShape
    {
        public double Length { get; set; }
        public double Width { get; set; }
        public Rectangle(double length, double width)
        {
            if (length <= 0 || width <= 0)
            {
                throw new ArgumentException("长和宽必须是正数！");
            }

            Length = length;
            Width = width;
        }
        public double Area()
        {
            return Length * Width;
        }
    }
    public class Square : IShape
    {
        public double Side { get; set; }
        public Square(double side)
        {
            if (side <= 0)
            {
                throw new ArgumentException("边长必须是正数！");
            }

            Side = side;
        }
        public double Area()
        {
            return Side * Side;
        }
    }

    public class Triangle : IShape
    {
        public double Side_1 { get; set; }
        public double Side_2 { get; set; }
        public double Side_3 { get; set; }
        public Triangle(double side_1, double side_2, double side_3)
        {
            List<double> sides = new List<double> { side_1, side_2, side_3 };
            sides.Sort();
            if (side_1 <= 0 || side_2 <= 0 || side_3 <= 0)
            {
                throw new ArgumentException("边长必须是正数！");
            }
            if (sides[0] + sides[1] <= sides[2])
            {
                throw new ArgumentException("三角形的两边之和必须大于第三边！");
            }
            Side_1 = sides[0];
            Side_2 = sides[1];
            Side_3 = sides[2];
        }
        public  double Area()
        {
            double s = (Side_1 + Side_2 + Side_3) / 2;
            return Math.Sqrt(s * (s - Side_1) * (s - Side_2) * (s - Side_3));
        }
    }
    public class Circle : IShape
    {
        public double Radius { get; set; }
        public Circle(double radius)
        {
            if (radius <= 0)
            {
                 throw new ArgumentException("半径必须是正数！");
            }

            Radius = radius;
        }
        public double Area()
        {        
            return Math.PI * Radius * Radius;
        }
    }

    public class SimpleFactory
    {
        public static IShape CreateShape(string shapeType)
        {
            switch(shapeType)
            {
                case "Rectangle":
                    Console.WriteLine("请输入长和宽：");
                    double length = Convert.ToDouble(Console.ReadLine());
                    double width = Convert.ToDouble(Console.ReadLine());
                    return new Rectangle(length, width);
                case "Square":
                    Console.WriteLine("请输入边长：");
                    double side = Convert.ToDouble(Console.ReadLine());
                    return new Square(side);
                case "Triangle":
                    Console.WriteLine("请输入三条边长：");
                    double side_1 =Convert.ToDouble(Console.ReadLine());
                    double side_2 = Convert.ToDouble(Console.ReadLine());
                    double side_3 = Convert.ToDouble(Console.ReadLine());
                    return new Triangle(side_1, side_2, side_3);
                case "Circle":
                    Console.WriteLine("请输入半径：");
                    double radius = Convert.ToDouble(Console.ReadLine());
                    return new Circle(radius);
                default:
                    return null;
            }
        }
    }
    class TestClass
    {
        static void Main(string[] args)
        {
            IShape shape1 = new Rectangle(3, 4);
            IShape shape2 = new Square(5);
            IShape shape3 = new Triangle(3, 4, 5);
            IShape shape4 = new Circle(2.5);

            Console.WriteLine($"示例图形的面积和是： {shape1.Area() + shape2.Area() + shape3.Area() + shape4.Area()}");

            bool flag = true;
            while (flag)
            {
                Console.WriteLine("请输入要创建的图形类型（Rectangle/Square/Triangle/Circle，输入exit退出）：");
                string shapeType = Console.ReadLine();
                if (shapeType == "exit")
                {
                    flag = false;
                    break;
                }
                IShape shape = SimpleFactory.CreateShape(shapeType);
                Console.WriteLine($"图形的面积是： {shape.Area()}\n");
            }
        }
    }
}
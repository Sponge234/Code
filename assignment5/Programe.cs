using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrderManagement
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"产品ID: {Id}, 名称: {Name}, 价格: {Price:C}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Product p = (Product)obj;
            return Id == p.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"客户ID: {Id}, 姓名: {Name}";

        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Customer c = (Customer)obj;
            return Id == c.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class OrderDetails
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;

        public override string ToString()
        {
            return $"{Product}, 数量: {Quantity}, 总价: {TotalPrice:C}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            OrderDetails od = (OrderDetails)obj;
            return Product.Equals(od.Product);
        }

        public override int GetHashCode()
        {
            return Product.GetHashCode();
        }
    }

    public class Order : IComparable<Order>
    {
        public string Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetails> Details { get; set; }
        public decimal TotalAmount => Details.Sum(d => d.TotalPrice);

        public void AddDetail(OrderDetails detail)
        {
            if (Details.Contains(detail))
                throw new Exception("订单明细已存在!");
            Details.Add(detail);
        }

        public void RemoveDetail(Product product)
        {
            var detail = Details.FirstOrDefault(d => d.Product.Equals(product));
            if (detail == null)
                throw new Exception("订单明细不存在!");
            Details.Remove(detail);
        }

        public override string ToString()
        {
            string detailsStr = string.Join("\n  ", Details);
            return $"订单号: {Id}, 客户: {Customer}, 下单时间: {OrderDate}\n" +
                   $"  明细:\n  {detailsStr}\n" +
                   $"  总计: {TotalAmount:C}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Order o = (Order)obj;
            return Id == o.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public int CompareTo(Order other)
        {
            if (other == null) return 1;
            return Id.CompareTo(other.Id);
        }
    }

    public class OrderService
    {
        private List<Order> orders = new List<Order>();

        // 添加订单
        public void AddOrder(Order order)
        {
            if (orders.Contains(order))
                throw new Exception($"订单 {order.Id} 已存在!");
            orders.Add(order);
        }

        // 删除订单
        public void RemoveOrder(string orderId)
        {
            var order = orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                throw new Exception($"订单 {orderId} 不存在!");
            orders.Remove(order);
        }

        // 修改订单
        public void UpdateOrder(Order order)
        {
            var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder == null)
                throw new Exception($"订单 {order.Id} 不存在!");
            orders.Remove(existingOrder);
            orders.Add(order);
        }

        // 查询所有订单
        public List<Order> GetAllOrders()
        {
            return orders.OrderBy(o => o.TotalAmount).ToList();
        }

        // 按订单号查询
        public List<Order> QueryByOrderId(string orderId)
        {
            return orders.Where(o => o.Id.Contains(orderId))
                        .OrderBy(o => o.TotalAmount)
                        .ToList();
        }

        // 按客户查询
        public List<Order> QueryByCustomer(string customerName)
        {
            return orders.Where(o => o.Customer.Name.Contains(customerName))
                        .OrderBy(o => o.TotalAmount)
                        .ToList();
        }

        // 按商品名称查询
        public List<Order> QueryByProduct(string productName)
        {
            return orders.Where(o => o.Details.Any(d => d.Product.Name.Contains(productName)))
                        .OrderBy(o => o.TotalAmount)
                        .ToList();
        }

        // 按金额查询
        public List<Order> QueryByAmount(decimal min, decimal max)
        {
            return orders.Where(o => o.TotalAmount >= min && o.TotalAmount <= max)
                        .OrderBy(o => o.TotalAmount)
                        .ToList();
        }

        // 按订单号
        public void SortOrders(Comparison<Order> comparison = null)
        {
            if (comparison == null)
                orders.Sort();
            else
                orders.Sort(comparison);
        }
    }
    class Program
    {
        static OrderService orderService = new OrderService();
        static List<Product> products = new List<Product>();
        static List<Customer> customers = new List<Customer>();

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("\n订单管理系统");
                Console.WriteLine("1. 添加订单");
                Console.WriteLine("2. 删除订单");
                Console.WriteLine("3. 修改订单");
                Console.WriteLine("4. 查询订单");
                Console.WriteLine("5. 显示所有订单");
                Console.WriteLine("6. 排序订单");
                Console.WriteLine("0. 退出");
                Console.Write("请选择操作: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("无效输入，请输入数字!");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddOrder();
                            break;
                        case 2:
                            RemoveOrder();
                            break;
                        case 3:
                            UpdateOrder();
                            break;
                        case 4:
                            QueryOrders();
                            break;
                        case 5:
                            DisplayAllOrders();
                            break;
                        case 6:
                            SortOrders();
                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("无效选择!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"错误: {ex.Message}");
                }
            }
        }

        static void AddOrder()
        {
            Console.Write("输入订单号: ");
            string id = Console.ReadLine();

            Console.WriteLine("选择客户:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customers[i]}");
            }
            Console.Write("选择客户id: ");
            int CustomerID = int.Parse(Console.ReadLine());
            Customer customer = customers[CustomerID];

            Order order = new Order { Id = id, Customer = customer };

            while (true)
            {
                Console.WriteLine("\n选择产品添加到订单 (输入0完成):");
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {products[i]}");
                }
                Console.Write("选择产品编号: ");
                int productID = int.Parse(Console.ReadLine());
                if (productID == 0) break;

                Console.Write("输入数量: ");
                int quantity = int.Parse(Console.ReadLine());

                Product product = products[productID - 1];
                order.AddDetail(new OrderDetails { Product = product, Quantity = quantity });
            }

            orderService.AddOrder(order);
            Console.WriteLine("订单添加成功!");
        }

        static void RemoveOrder()
        {
            Console.Write("输入要删除的订单号: ");
            string id = Console.ReadLine();
            orderService.RemoveOrder(id);
            Console.WriteLine("订单删除成功!");
        }

        static void UpdateOrder()
        {
            Console.Write("输入要修改的订单号: ");
            string id = Console.ReadLine();

            var existingOrders = orderService.QueryByOrderId(id);
            if (existingOrders.Count == 0)
            {
                Console.WriteLine("订单不存在!");
                return;
            }

            Console.WriteLine("原订单信息:");
            Console.WriteLine(existingOrders[0]);

            Console.WriteLine("选择新客户:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customers[i]}");
            }
            Console.Write("选择客户编号: ");
            int CustomerID = int.Parse(Console.ReadLine()) - 1;
            Customer customer = customers[CustomerID];

            Order updatedOrder = new Order{ Id = id, Customer = customer };

            while (true)
            {
                Console.WriteLine("\n选择产品添加到订单 (输入0完成):");
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {products[i]}");
                }
                Console.Write("选择产品编号: ");
                int productID = int.Parse(Console.ReadLine());
                if (productID == 0) break;

                Console.Write("输入数量: ");
                int quantity = int.Parse(Console.ReadLine());

                Product product = products[productID - 1];
                updatedOrder.AddDetail(new OrderDetails{ Product = product, Quantity = quantity });
            }

            orderService.UpdateOrder(updatedOrder);
            Console.WriteLine("订单修改成功!");
        }

        static void QueryOrders()
        {
            Console.WriteLine("查询方式:");
            Console.WriteLine("1. 按订单号");
            Console.WriteLine("2. 按客户");
            Console.WriteLine("3. 按商品名称");
            Console.WriteLine("4. 按金额范围");
            Console.Write("请选择查询方式: ");
            int choice = int.Parse(Console.ReadLine());

            List<Order> result = new List<Order>();
            switch (choice)
            {
                case 1:
                    Console.Write("输入订单号(或部分): ");
                    string id = Console.ReadLine();
                    result = orderService.QueryByOrderId(id);
                    break;
                case 2:
                    Console.Write("输入客户姓名(或部分): ");
                    string customer = Console.ReadLine();
                    result = orderService.QueryByCustomer(customer);
                    break;
                case 3:
                    Console.Write("输入商品名称(或部分): ");
                    string product = Console.ReadLine();
                    result = orderService.QueryByProduct(product);
                    break;
                case 4:
                    Console.Write("输入最小金额: ");
                    decimal min = decimal.Parse(Console.ReadLine());
                    Console.Write("输入最大金额: ");
                    decimal max = decimal.Parse(Console.ReadLine());
                    result = orderService.QueryByAmount(min, max);
                    break;
                default:
                    Console.WriteLine("无效选择!");
                    return;
            }

            Console.WriteLine($"找到 {result.Count} 条订单:");
            foreach (var order in result)
            {
                Console.WriteLine(order);
            }
        }

        static void DisplayAllOrders()
        {
            var orders = orderService.GetAllOrders();
            Console.WriteLine($"共有 {orders.Count} 条订单:");
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }

        static void SortOrders()
        {
            Console.WriteLine("排序方式:");
            Console.WriteLine("1. 按订单号(默认)");
            Console.WriteLine("2. 按总金额");
            Console.WriteLine("3. 按下单时间");
            Console.Write("请选择排序方式: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    orderService.SortOrders();
                    break;
                case 2:
                    orderService.SortOrders((o1, o2) => o1.TotalAmount.CompareTo(o2.TotalAmount));
                    break;
                case 3:
                    orderService.SortOrders((o1, o2) => o1.OrderDate.CompareTo(o2.OrderDate));
                    break;
                default:
                    Console.WriteLine("无效选择，使用默认排序!");
                    orderService.SortOrders();
                    break;
            }

            Console.WriteLine("排序完成!");
            DisplayAllOrders();
        }
    }

    [TestClass]
    public class OrderServiceTests
    {
        private OrderService orderService;
        private Product product1, product2;
        private Customer customer1, customer2;
        private Order order1, order2;

        [TestInitialize]
        public void Initialize()
        {
            orderService = new OrderService();

            product1 = new Product { Id = "P001", Name = "笔记本电脑", Price = 5999 };
            product2 = new Product { Id = "P002", Name = "手机", Price = 3999 };

            customer1 = new Customer { Id = "C001", Name = "A" };
            customer2 = new Customer { Id = "C002", Name = "B" };

            order1 = new Order { Id = "O001", Customer = customer1 };
            order1.AddDetail(new OrderDetails { Product = product1, Quantity = 1 });
            order1.AddDetail(new OrderDetails{ Product = product2, Quantity = 2 });

            order2 = new Order{ Id = "O002", Customer = customer2 };
            order2.AddDetail(new OrderDetails { Product = product2, Quantity = 1 });

            orderService.AddOrder(order1);
            orderService.AddOrder(order2);
        }

        [TestMethod]
        public void TestAddOrder()
        {
            var newOrder = new Order { Id = "O003", Customer = customer1 };
            newOrder.AddDetail(new OrderDetails{ Product = product1, Quantity = 1 });
            orderService.AddOrder(newOrder);

            var result = orderService.QueryByOrderId("O003");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("O003", result[0].Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddDuplicateOrder()
        {
            var duplicateOrder = new Order { Id = "O001", Customer = customer1 };
            orderService.AddOrder(duplicateOrder);
        }

        [TestMethod]
        public void TestRemoveOrder()
        {
            orderService.RemoveOrder("O001");
            var result = orderService.QueryByOrderId("O001");
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRemoveNonExistentOrder()
        {
            orderService.RemoveOrder("O999");
        }

        [TestMethod]
        public void TestUpdateOrder()
        {
            var updatedOrder = new Order{ Id = "O001", Customer = customer2 };
            updatedOrder.AddDetail(new OrderDetails{ Product = product1, Quantity = 3 });
            orderService.UpdateOrder(updatedOrder);

            var result = orderService.QueryByOrderId("O001");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].Details[0].Quantity);
            Assert.AreEqual("B", result[0].Customer.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateNonExistentOrder()
        {
            var nonExistentOrder = new Order{ Id = "O999", Customer = customer1 };
            orderService.UpdateOrder(nonExistentOrder);
        }

        [TestMethod]
        public void TestQueryByOrderId()
        {
            var result = orderService.QueryByOrderId("O001");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("O001", result[0].Id);
        }

        [TestMethod]
        public void TestQueryByCustomer()
        {
            var result = orderService.QueryByCustomer("A");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("A", result[0].Customer.Name);
        }

        [TestMethod]
        public void TestQueryByProduct()
        {
            var result = orderService.QueryByProduct("手机");
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0].Details.Any(d => d.Product.Name.Contains("手机")));
        }

        [TestMethod]
        public void TestQueryByAmountRange()
        {
            var result = orderService.QueryByAmount(3000, 10000);
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].TotalAmount >= 3000 && result[0].TotalAmount <= 10000);
        }

        [TestMethod]
        public void TestSortOrders()
        {
            // 默认按订单号排序
            orderService.SortOrders();
            var orders = orderService.GetAllOrders();
            Assert.IsTrue(orders[0].Id.CompareTo(orders[1].Id) < 0);

            // 按总金额排序
            orderService.SortOrders((o1, o2) => o1.TotalAmount.CompareTo(o2.TotalAmount));
            orders = orderService.GetAllOrders();
            Assert.IsTrue(orders[0].TotalAmount <= orders[1].TotalAmount);
        }
    }
}


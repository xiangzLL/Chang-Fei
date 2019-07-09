using System;

namespace DSS
{
    /// <summary>
    /// 数据库存储服务，不支持横向扩展
    /// 从消息队列里读取数据，并将其保存到数据库中
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}

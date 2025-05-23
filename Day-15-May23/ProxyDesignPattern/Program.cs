

using ProxyDesignPattern.Interfaces;
using ProxyDesignPattern.Services;

namespace ProxyDesignPattern
{
    class Program
    {
        static void Main()
        {
            Files file = new Files();
            ProxyFile proxy = new ProxyFile(file);
            string contents = proxy.Read();
            Console.WriteLine(contents);
        }
    }
}


using _2_OpenClose.Models;
using _2_OpenClose.Services;

namespace _2_OpenClose
{
    class Program
    {
        static void Main()
        {
            Order order = new Order("Hammer");          // Single Responsibility Good Design

            OrderProcessor orderProcessor = new OrderProcessor();

            orderProcessor.Process(order);

        }
    }
}
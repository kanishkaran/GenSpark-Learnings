
// SOLID
// S -> Single Responsibilty
// O -> Open for Extension , Closed for Modification
// L -> Liskov Substitution
// I -> Interface Segregation
// D -> Dependency Inversion


using _1_SingleResponsibility.Models;
using _1_SingleResponsibility.Services;

namespace _1_SingleResponsibility
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
    // S -> Single Responsibilty - Bad Design
    class Orders
    {
        public void ProcessOrder(string order)
        {
            // Save Logic
            Console.WriteLine($"Order Saved {order}");

            // Notification Logic
            Console.WriteLine($"Notification Sent for the order {order}");
        }
    }
}
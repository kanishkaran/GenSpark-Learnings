
using _5_DependencyInversion.Interfaces;
using _5_DependencyInversion.Models;
using _5_DependencyInversion.Repository;
using _5_DependencyInversion.Services;

namespace _5_DependencyInversion
{
    class Program
    {
        static void Main()
        {
            var order = new Order("Order #123");

            // Create dependencies
            var repo = new OrderRepository();
            var notifiers = new List<INotifier>
        {
            new EmailNotifier(),
            new SmsNotifier()
        };

            // Inject dependencies
            var processor = new OrderProcessor(repo, notifiers);
            processor.Process(order);
        }
    }
}
using _4_InterfaceSegregation.Interfaces;
using _4_InterfaceSegregation.ISP;

namespace _4_InterfaceSegregation
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Interface Segregation Principle Example\n");

            // BAD DESIGN: Fat interface forces implementation of unused methods
            Console.WriteLine("Bad Design (Fat Interface):");
            IWorker chef = new Chef();
            chef.Cook();
            try
            {
                chef.Clean(); // NotImplementedException
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Chef does not clean (method not implemented).");
            }
            try
            {
                chef.Serve(); // NotImplementedException
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("Chef does not serve (method not implemented).");
            }

            Console.WriteLine("\nGood Design (Interface Segregation):");
            // GOOD DESIGN: Segregated interfaces
            ICook cook = new Cook();
            cook.Cooks();

            ICleaner janitor = new Janitor();
            janitor.Cleans();

            IServer waiter = new Waiter();
            waiter.Serve();
        }
    }
}
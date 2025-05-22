
using _4_InterfaceSegregation.Interfaces;

namespace _4_InterfaceSegregation.ISP
{
    public class Janitor : ICleaner
    {
        public void Cleans()
        {
            Console.WriteLine("Cleaning...");
        }
    }
}
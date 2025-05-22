
using _4_InterfaceSegregation.Interfaces;

namespace _4_InterfaceSegregation.ISP
{
    public class Cook : ICook
    {
        public void Cooks()
        {
            Console.WriteLine("Cooking...");
        }
    }
}


using _4_InterfaceSegregation.Interfaces;

namespace _4_InterfaceSegregation.ISP
{
    public class Waiter : IServer
    {
        public void Serve()
        {
            Console.WriteLine("Serving...");
        }

    }
}

using _4_InterfaceSegregation.Interfaces;

namespace _4_InterfaceSegregation
{
    public class Chef : IWorker
    {
        public void Clean()
        {
            throw new NotImplementedException();
        }

        public void Cook()
        {
            Console.WriteLine("Cooking");
        }

        public void Serve()
        {
            throw new NotImplementedException();
        }

        // Does not Serve

        // Does not Clean (Tables in this Context)
    }
}
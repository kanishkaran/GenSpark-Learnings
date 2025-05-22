using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4_InterfaceSegregation.Interfaces
{
    public interface IWorker        //// I -> Interface Segregation - Bad Design
    {
        void Cook();
        void Clean();
        void Serve();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryDesign.Interfaces;

namespace FactoryDesign
{
    public abstract class HandlerCreator
    {
        public abstract IFileHandler CreateHandler();
    }
}
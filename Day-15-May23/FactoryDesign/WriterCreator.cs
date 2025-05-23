using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryDesign.Handlers;
using FactoryDesign.Interfaces;

namespace FactoryDesign
{
    public class WriterCreator : HandlerCreator
    {
        public override IFileHandler CreateHandler()
        {
            return new FileWriter();
        }
    }
}
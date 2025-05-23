
using FactoryDesign.Handlers;
using FactoryDesign.Interfaces;

namespace FactoryDesign
{
    public class ReadCreator : HandlerCreator
    {
        public override IFileHandler CreateHandler()
        {
            return new FileReader();
        }
    }
}
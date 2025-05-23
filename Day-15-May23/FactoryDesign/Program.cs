
namespace FactoryDesign
{
    class Program
    {
        static void Main()
        {
            string filePath = "sample.txt";

            // Write to file
            HandlerCreator writerHandler = new WriterCreator();
            var writer = writerHandler.CreateHandler();
            writer.Handle(filePath, FileAccess.Write, "Factory Pattern!");

            // Read from file
            HandlerCreator readCreator = new ReadCreator();
            var reader = readCreator.CreateHandler();
            reader.Handle(filePath, FileAccess.Read);
        }
    }
}
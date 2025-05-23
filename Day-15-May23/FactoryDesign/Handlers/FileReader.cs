using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryDesign.Interfaces;

namespace FactoryDesign.Handlers
{
    public class FileReader : IFileHandler
    {
        public void Handle(string filePath, FileAccess access, string? content = null )
        {
            using (var reader = new StreamReader(new FileStream(filePath, FileMode.OpenOrCreate,access)))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine("Read content:");
                Console.WriteLine(result);
            }
        }
    }
}
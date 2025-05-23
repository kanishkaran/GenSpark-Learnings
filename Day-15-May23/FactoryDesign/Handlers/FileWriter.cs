using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryDesign.Interfaces;

namespace FactoryDesign.Handlers
{
    public class FileWriter : IFileHandler
    {
        public void Handle(string filePath, FileAccess access, string? content = null)
        {
            using (var writer = new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate, access)))
            {
                writer.Write(content);
                Console.WriteLine("Content written to file.");
            }
        }
    }
}
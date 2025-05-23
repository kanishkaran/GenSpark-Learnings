
using ProxyDesignPattern.Interfaces;

namespace ProxyDesignPattern.Services
{
    public class Files : IFile
    {
        public string _sensitiveFilePath = "/Users/kanishkaran/Desktop/GenSpark-Learnings/Day-15-May23/ProxyDesignPattern/SensitiveFile.txt";
        public string Read()
        {
            Console.WriteLine("Reading Sensitive Content...");

            string content = File.ReadAllText(_sensitiveFilePath);

            return content;
        }
    }
}


namespace SingletonApp
{
    public sealed class FileManager
    {

        private FileManager() { }

        private static FileManager? _instance;
        private FileStream? _fileStream;
        private StreamReader? _reader;
        private StreamWriter? _writer;

        public static FileManager GetInstance()
        {
            if (_instance == null)
                _instance = new FileManager();
            return _instance;
        }

        public void ReadFile()
        {
            if (_reader == null)
                Console.WriteLine("File not opened for reading");
            else
            {
                var content = _reader.ReadLine();
                Console.WriteLine(content);
            }
        }

        public void WriteFile(string line)
        {
            if (_writer == null)
                Console.WriteLine("File not opened for writing");
            else
            {
                _writer.WriteLine(line);
                _writer.Flush();
                Console.WriteLine("Written Successfully");
            }

        }

        public void OpenFile(string path, FileAccess access)
        {

            _fileStream = new FileStream(path, FileMode.OpenOrCreate, access);

            if (access == FileAccess.Read)
                _reader = new StreamReader(_fileStream);
            else if (access == FileAccess.Write)
                _writer = new StreamWriter(_fileStream);
        }

        public void CloseFile()
        {
            _writer?.Dispose();
            _reader?.Dispose();
            _fileStream?.Dispose();
            _writer = null;
            _reader = null;
            _fileStream = null;
        }
    }
}
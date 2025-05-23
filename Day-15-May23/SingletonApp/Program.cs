
namespace SingletonApp
{
    class Program
    {
        static void Main()
        {
            var manager = FileManager.GetInstance();
            var manager_2 = FileManager.GetInstance();

            if (manager == manager_2)
                Console.WriteLine("Singleton Design Works");
            manager.OpenFile("test.txt", FileAccess.Write);
            manager.WriteFile("This is a Singleton design");
            manager.CloseFile();
            // manager.OpenFile("test.txt", FileAccess.Write);

            manager.OpenFile("test.txt", FileAccess.Read);
            manager.ReadFile();
            manager.CloseFile();
        }
    }
}
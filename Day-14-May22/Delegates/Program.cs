


using Delegates.Extension;

namespace Delegates
{
    class Program
    {

        public delegate void MyDelegate(int num_1, int num_2);
        public delegate void NewDelegate<T>(T num_1, T num_2);

        public static List<Employee> list = new List<Employee>
        {
            new Employee(101, 21, "Ben", 2100),
            new Employee(102, 23, "Adam", 2139),
            new Employee(103, 22, "Zoro", 2678)
        };
        static void Main()
        {
            MyDelegate del = new MyDelegate(Add);
            del += Product; // Multi-Cast
            del(10, 20);

            Console.WriteLine("Generic Delegate");
            NewDelegate<double> dele = new(AddDecimal);
            dele(20.3, 30.7);

            Console.WriteLine("Predefined Delegate");
            Action<int, int> deleg = Add;
            deleg += Product;

            deleg(200, 300);

            Console.WriteLine("Predicates");

            Predicate<Employee> predicate = new(e => e.Id == 103);
            var employee = list.Find(predicate);
            Console.WriteLine(employee);

            Console.WriteLine("\n\nFunc");

            var emp = list.OrderBy(e => e.Name);

            foreach (Employee e in emp)
                Console.WriteLine(e);

            string words = "This is a string";
            int word_count = words.WordCount();
            Console.WriteLine($"String: {words} \nWord Count: {word_count}");

        }

        static void Add(int n1, int n2)
        {
            Console.WriteLine($"The sum of {n1} and {n2} is {n1 + n2}");
        }
        static void Product(int n1, int n2)
        {
            Console.WriteLine($"The Product of {n1} and {n2} is {n1 * n2}");
        }

        static void AddDecimal(double n1, double n2)
        {
            Console.WriteLine($"The Sum of {n1} and {n2} is {n1 + n2}");
        }


    }
}
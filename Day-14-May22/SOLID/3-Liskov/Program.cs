using System;

namespace _3_Liskov
{
    // LSP Violation Example
    public class Animal
    {
        public virtual void Hunt()
        {
            Console.WriteLine("Animal Hunting");
        }

        public virtual void Eat()
        {
            Console.WriteLine("Animal Eating");
        }
    }

    public class Tiger : Animal
    {
        public override void Hunt()
        {
            Console.WriteLine("Tiger Hunting");
        }

        public override void Eat()
        {
            Console.WriteLine("Tiger Eating");
        }
    }

    public class Deer : Animal // Deer does not hunt, but forced to inherit Hunt()
    {
        public override void Hunt()
        {
            throw new NotImplementedException("Deer cannot hunt!");
        }

        public override void Eat()
        {
            Console.WriteLine("Deer Eating");
        }
    }

    // LSP Correct Example
    public class BaseAnimal
    {
        public virtual void Eat()
        {
            Console.WriteLine("Animal Eating");
        }
    }

    public interface IPredator
    {
        void Hunt();
    }

    public class Lion : BaseAnimal, IPredator
    {
        public override void Eat()
        {
            Console.WriteLine("Lion Eating");
        }

        public void Hunt()
        {
            Console.WriteLine("Lion Hunting");
        }
    }

    public class Elephant : BaseAnimal
    {
        public override void Eat()
        {
            Console.WriteLine("Elephant Eating");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Liskov Substitution Principle Example\n");

            // LSP Violation
            Console.WriteLine("LSP Violation:");
            Animal tiger = new Tiger();
            tiger.Hunt();
            tiger.Eat();

            Animal deer = new Deer();
            try
            {
                deer.Hunt(); // This will throw NotImplementedException
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            deer.Eat();

            Console.WriteLine("\nLSP Correct Design:");
            BaseAnimal lion = new Lion();
            lion.Eat();
            if (lion is IPredator predator)
            {
                predator.Hunt();
            }

            BaseAnimal elephant = new Elephant();
            elephant.Eat();
        }
    }
}

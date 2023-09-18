namespace stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2454();
            Welcome3323();
            Console.ReadKey();
        }
        static partial void Welcome3323();
        private static void Welcome2454()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
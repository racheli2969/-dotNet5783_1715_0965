// See https://aka.ms/new-console-template for more information

namespace stage0
{
   partial class P
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Welcome1715();
            Console.ReadKey();
        }
        static partial void Welcome0965();
        private static void Welcome1715()
        {
            Console.WriteLine("Enter your name: ");
            string v = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", v);
        }
       
    }
}
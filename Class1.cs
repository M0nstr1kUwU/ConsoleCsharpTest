using System.Security.Cryptography;

namespace ClassTest
{
    namespace Class1NS
    {
        public class Class1
        {
            private string hw = "Hello World";

            public void print_hw()
            {
                Console.WriteLine(hw);
            }

            public void addAB(int a, int b)
            {
                Console.WriteLine($"Result: {a + b}");
            }
        }
    }

    namespace CalculatorNS
    {
        public class Calculator
        {
            public double div(double a, double b)
            {
                if (b == 0) throw new DivideByZeroException("Деление на ноль!");
                return a / b;
            }

            public double add(double a, double b)
            {
                return a + b;
            }

            public double sub(double a, double b)
            {
                return a - b;
            }

            public double mul(double a, double b)
            {
                return a * b;
            }
        }
    
    }
}
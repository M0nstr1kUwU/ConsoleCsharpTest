using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Channels;

internal class Program
{
    private static void Main(string[] args)
    {
        choice_test();
    }

    static void choice_test()
    {
        int choice;
        while (true)
        {
            fill_char('~', 30);
            Console.Write("Choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case int n when n == 1:
                    fill_char('~', 30);
                    test1();
                    continue;
                case int n when n == 2:
                    fill_char('~', 30);
                    test2();
                    continue;
                case int n when n == 3:
                    fill_char('~', 30);
                    test3();
                    continue;
                case int n when n == 0:
                    return;
                default:
                    Console.WriteLine("Неверный выбор\n");
                    continue;
            }
        }
    }

    static void test1()
    {
        Console.Write("A: ");
        int a = Convert.ToInt32(Console.ReadLine());
        Console.Write("B: ");
        int b = Convert.ToInt32(Console.ReadLine());
        int c = 0;
        Console.WriteLine($"Сумма: {a + b}");

        for (int i = 0; i < 4; i++)
        {
            c += a + b;
            Console.WriteLine($"Сложение {i + 1}: {c}");
        }
        Console.WriteLine($"Result: {c}");
    }

    static void test2()
    {
        int input;
        int secret_num = RandomNumberGenerator.GetInt32(1, 100);
        while (true)
        {
            Console.Write("Число: ");
            input = Convert.ToInt32(Console.ReadLine());

            if (input < secret_num)
            {
                Console.WriteLine("Меньше");
            }
            else if (input > secret_num)
            {
                Console.WriteLine("Больше");
            }
            else
            {
                Console.WriteLine($"Победа! Загаданное число: {secret_num}");
                break;
            }
        }
    }

    static void test3()
    {
        Console.Write("Строка: ");
        string input = Console.ReadLine()??"";

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Строка пустая");
            return;
        }

        string input2 = "";
        for (int i = input.Length - 1; i >= 0; i--)
        {
            input2 += input[i];
        }

        if (input == input2)
        {
            Console.WriteLine("Палиндром");
        }
        else
        {
            Console.WriteLine("Не палиндром");
        }
        fill_char('=', 30);
    }

    static void check_password(){
        Console.Write("Password: ");
        string? password = Console.ReadLine();

        string current_password = "qwerty1234";

        if (string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Строка пустая");
            return;
        }

        if ()
        {

        }
    }

    static void fill_char(char c, int num)
    {
        for (int i = 0; i < num; i++)
        {
            Console.Write(c);
        }
        Console.WriteLine("\n");
    }
}
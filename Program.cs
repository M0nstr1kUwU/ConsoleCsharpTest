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
            Console.WriteLine(
                "1. Сложение\n" +
                "2. Шазам с числами\n" +
                "3. Палиндром\n" +
                "4. Максимальное, минимальное и среднее число из массива\n" +
                "5. Сортировка пузырьком\n" +
                "6. Перемножение и сложение массивов");
            Console.Write("Choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    fill_char('~', 30);
                    test1();
                    continue;
                case 2:
                    fill_char('~', 30);
                    test2();
                    continue;
                case 3:
                    fill_char('~', 30);
                    test3();
                    continue;
                case 4:
                    fill_char('~', 30);
                    sort_max_in_arr_sr();
                    continue;
                case 5:
                    fill_char('~', 30);
                    sort_bottle();
                    continue;
                case 6:
                    fill_char('~', 30);
                    peremnogenie_clogenie();
                    continue;
                case 0:
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
        fill_char('=', 30);
    }

    static void test3()
    {
        Console.Write("Строка: ");
        string input = Console.ReadLine() ?? "";

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

    //static void check_password(){
    //    Console.Write("Password: ");
    //    string? password = Console.ReadLine();

    //    string current_password = "qwerty1234";

    //    if (string.IsNullOrEmpty(password))
    //    {
    //        Console.WriteLine("Строка пустая");
    //        return;
    //    }
    //}

    static void sort_bottle()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine());
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr[i] = int.Parse(Console.ReadLine());
        }
        Console.Write("[");
        for (int i = 0; i < size; i++)
        {
            if (size - 1 == i)
            {
                Console.Write($"{arr[i]}]\n");
            }
            else
            {
                Console.Write($"{arr[i]}, ");
            }
        }

        for (int i = 0; i < size - 1; i++)
        {
            for (int j = 0; j < size - 1 - i; j++)
            {
                if (arr[j] < arr[j + 1])
                {
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }
        }

        Console.WriteLine("Sort array:");
        Console.Write("[");
        for (int i = 0; i < size; i++)
        {
            if (size - 1 == i)
            {
                Console.Write($"{arr[i]}]\n");
            }
            else
            {
                Console.Write($"{arr[i]}, ");
            }
        }
        fill_char('=', 30);
    }

    static void sort_max_in_arr_sr()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine());
        if (size == 0)
        {
            Console.WriteLine("Size can`t be null!");
            return;
        }
        int sum = 0;
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr[i] = int.Parse(Console.ReadLine());
            sum += arr[i];
        }
        int max_num = -9999;
        int min_num = 9999;
        for (int i = 0; i < size - 1; i++)
        {
            if (arr[i + 1] > max_num)
            {
                max_num = arr[i + 1];
            }
            if (arr[i + 1] < min_num)
            {
                min_num = arr[i + 1];
            }
        }
        Console.WriteLine($"Наибольшее: {max_num}");
        Console.WriteLine($"Наименьшее: {min_num}");
        Console.WriteLine($"Среднее арифметическое: {(float)sum / (float)size}");
        fill_char('=', 30);
    }

    static void peremnogenie_clogenie()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine());
        if (size == 0)
        {
            Console.WriteLine("Size can`t be null!");
            return;
        }
        int[] arr1 = new int[size];
        Console.WriteLine("Array 1:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr1[i] = int.Parse(Console.ReadLine());
        }
        int[] arr2 = new int[size];
        Console.WriteLine("Array 2:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr2[i] = int.Parse(Console.ReadLine());
        }
        int[] arr3 = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr3[i] = arr1[i] + arr2[i];
        }
        int[] arr4 = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr4[i] = arr1[i] * arr2[i];
        }
        Console.Write("Массив суммы: [");
        for (int i = 0; i < size; i++)
        {
            if (size - 1 == i)
            {
                Console.Write($"{arr3[i]}]\n");
            }
            else
            {
                Console.Write($"{arr3[i]}, ");
            }
        }
        Console.Write("Массив перемножения: [");
        for (int i = 0; i < size; i++)
        {
            if (size - 1 == i)
            {
                Console.Write($"{arr4[i]}]\n");
            }
            else
            {
                Console.Write($"{arr4[i]}, ");
            }
        }
        fill_char('=', 30);
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
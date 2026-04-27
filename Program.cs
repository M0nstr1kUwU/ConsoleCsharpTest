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
                "5. Сортировка пузырьком int\n" +
                "6. Перемножение и сложение массивов\n" +
                "7. Сортировка массива string\n" +
                "8. Сортировка массива string по lenght\n" +
                "9. Проверка на одинаковые слова в массиве без регистрозависимости\n" +
                "10. Шифр Цезаря\n" +
                "11. Проверка на существование символа в тексте\n" +
                "12. Сравнение строк без регистрозависимости\n" +
                "13. Ввод строки по буквам => целая строка\n" +
                "14. Вывод каждого четного символа из строки\n" +
                "15. Проверка слова в заготовленном массиве\n" +
                "16. Проверка аннограмы из всего массива\n" +
                "17. Сортировка по последней букве\n" +
                "18. Фиббоначи\n" +
                "19. Площадь круга\n" +
                "20. Факториал\n" +
                "21. Поиск наибольшего префикса\n" +
                "22. Перевёрнутый массив функцией\n");
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
                case 7:
                    fill_char('~', 30);
                    sort_bottle_string();
                    continue;
                case 8:
                    fill_char('~', 30);
                    sort_bottle_string_lenght();
                    continue;
                case 9:
                    fill_char('~', 30);
                    check_similar_word_in_array();
                    continue;
                case 10:
                    fill_char('~', 30);
                    crypto_cesar();
                    continue;
                case 11:
                    fill_char('~', 30);
                    check_char_in_text();
                    continue;
                case 12:
                    fill_char('~', 30);
                    check_text_a_similar_text();
                    continue;
                case 13:
                    fill_char('~', 30);
                    constructor_text_for_char();
                    continue;
                case 14:
                    fill_char('~', 30);
                    output_text_chet_char();
                    continue;
                case 15:
                    fill_char('~', 30);
                    check_word_in_array_50();
                    continue;
                case 16:
                    fill_char('~', 30);
                    check_annogram_in_array();
                    continue;
                case 17:
                    fill_char('~', 30);
                    sort_for_last_index();
                    continue;
                case 18:
                    fill_char('~', 30);
                    int a = int.Parse(Console.ReadLine() ?? "");
                    fibonachi(a);
                    continue;
                case 19:
                    fill_char('~', 30);
                    s_dev();
                    continue;
                case 20:
                    fill_char('~', 30);
                    factorial();
                    continue;
                case 21:
                    fill_char('~', 30);
                    seyau();
                    continue;
                case 22:
                    fill_char('~', 30);
                    perevertish();
                    continue;
                case 0:
                    fill_char('~', 30);
                    return;
                default:
                    fill_char('~', 30);
                    Console.WriteLine("Неверный выбор\n");
                    continue;
            }
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
                Console.WriteLine("Больше");
            }
            else if (input > secret_num)
            {
                Console.WriteLine("Меньше");
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
    }

    static void sort_bottle()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");
        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr[i] = int.Parse(Console.ReadLine() ?? "");
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
    }

    static void sort_max_in_arr_sr()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");
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
            arr[i] = int.Parse(Console.ReadLine() ?? "");
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
    }

    static void peremnogenie_clogenie()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");
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
            arr1[i] = int.Parse(Console.ReadLine() ?? "");
        }
        int[] arr2 = new int[size];
        Console.WriteLine("Array 2:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr2[i] = int.Parse(Console.ReadLine() ?? "");
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
    }

    static void sort_bottle_string()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");
        string[] arr = new string[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr[i] = Console.ReadLine() ?? "";
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
                if (arr[j].CompareTo(arr[j + 1]) > 0)
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
    }

    static void sort_bottle_string_lenght()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");
        string[] arr = new string[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr[i] = Console.ReadLine() ?? "";
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
                if (arr[j].Length > arr[j + 1].Length)
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
    }

    static void check_similar_word_in_array()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");
        string[] arr = new string[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr[i] = Console.ReadLine() ?? "";
        }

        int[] arr_similar = new int[size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (arr[i].ToLower() == arr[j].ToLower())
                {
                    arr_similar[i]++;
                }
            }
        }

        Console.WriteLine("Check array:");
        Console.Write("[ ");
        for (int i = 0; i < size; i++)
        {
            if (arr[i].Length > 0)
            {
                if (size - 1 == i)
                {
                    Console.Write($"[{arr[i]}: {arr_similar[i]}] ]\n");
                }
                else
                {
                    Console.Write($"[{arr[i]}: {arr_similar[i]}], ");
                }
            }

        }
    }

    static void crypto_cesar()
    {
        Console.Write("Текст: ");
        string text = Console.ReadLine() ?? "";
        Console.Write("Сдвиг: ");
        int s = Convert.ToInt32(Console.ReadLine());
        string result = "";

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (c >= 'a' && c <= 'z')
            {
                int ns = s % 26;
                int cd = c + ns;
                if (cd > 'z') cd -= 26;
                if (cd < 'a') cd += 26;
                result += (char)cd;
            }
            else if (c >= 'A' && c <= 'Z')
            {
                int ns = s % 26;
                int cd = c + ns;
                if (cd > 'Z') cd -= 26;
                if (cd < 'A') cd += 26;
                result += (char)cd;
            }
            else if (c >= 'а' && c <= 'я')
            {
                int ns = s % 32;
                int cd = c + ns;
                if (cd > 'я') cd -= 32;
                if (cd < 'а') cd += 32;
                result += (char)cd;
            }
            else if (c >= 'А' && c <= 'Я')
            {
                int ns = s % 32;
                int cd = c + ns;
                if (cd > 'Я') cd -= 32;
                if (cd < 'А') cd += 32;
                result += (char)cd;
            }
            else
            {
                result += c;
            }
        }
        Console.WriteLine($"Результат: {result}");
    }

    static void check_char_in_text()
    {
        Console.Write("Символ: ");
        string c = Console.ReadLine() ?? "";
        Console.Write("Текст: ");
        string text = Console.ReadLine() ?? "";
        if (c == null || text == null)
        {
            Console.WriteLine("Текст или символ не могут быть пустыми!");
            return;
        }
        int result = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i].ToString().ToLower() == char.Parse(c).ToString().ToLower())
            {
                result++;
            }
        }
        Console.WriteLine($"Результат: {result}");
    }

    static void check_text_a_similar_text()
    {
        Console.Write("Текст: ");
        string text1 = Console.ReadLine() ?? "";
        Console.Write("Текст: ");
        string text2 = Console.ReadLine() ?? "";
        if (text1 == null || text2 == null)
        {
            Console.WriteLine("Тексты не могут быть пустыми!");
            return;
        }
        if (text1.ToLower() == text2.ToLower())
        {
            Console.WriteLine("Строки одинаковы!");
        }
        else
        {
            Console.WriteLine("Строки не одинаковы!");
        }
    }

    static void constructor_text_for_char()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");
        string[] arr = new string[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr[i] = Console.ReadLine() ?? "";
            if (arr[i] == null)
            {
                arr[i] = "*";
            }
        }
        Console.Write("Строка: ");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{arr[i]}");
        }
        Console.Write("\n");
    }

    static void output_text_chet_char()
    {
        Console.Write("Текст: ");
        string text = Console.ReadLine() ?? "";
        if (text == null)
        {
            Console.WriteLine("Текст не может быть пустым!");
            return;
        }
        Console.Write("Результат: ");
        for (int i = 0; i < text.Length; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write(text[i]);
            }
        }
        Console.Write("\n");
    }

    static void check_word_in_array_50()
    {
        string[] arr =
        {

        };
    }

    static void check_annogram_in_array()
    {

    }

    static void sort_for_last_index()
    {

    }

    static int fibonachi(int a)
    {
        if (a <= 1)
        {
            return a;
        }
        else
        {
            return fibonachi(a - 1) + fibonachi(a - 2);
        }
    }

    static void s_dev()
    {
        Console.Write("Радиус: ");
        double r = double.Parse(Console.ReadLine() ?? "");
        Console.WriteLine($"Площадь: {Math.PI * Math.Pow(r, 2)}");
    }

    static void factorial()
    {
        Console.Write("Число: ");
        int n = int.Parse(Console.ReadLine() ?? "");
        long result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        Console.WriteLine($"Факториал {n}! = {result}");
    }

    static void seyau()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");

        string[] str = new string[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i+1}. ");
            str[i] = Console.ReadLine() ?? "";
        }

        if (str.Length == 0)
        {
            Console.WriteLine("Не разберу я твой префикс, потому что ты овощ, что ты вводишь вообще?! venom");
            return;
        }

        string prefix = str[0];
        for (int i = 1; i < str.Length; i++)
        {
            while (!str[i].StartsWith(prefix))
            {
                prefix = prefix.Substring(0, prefix.Length - 1);
                if (string.IsNullOrEmpty(prefix))
                {
                    Console.WriteLine("Нет тут твоего префикса общего");
                    return;
                }
            }
        }
        Console.WriteLine($"Результат: '{prefix}'");
    }

    static void perevertish()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");

        int[] str = new int[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            str[i] = int.Parse(Console.ReadLine() ?? "");
        }
        int[] str_pere = new int[size];
        for (int i = 0;i < size; i++)
        {
            str_pere[i] = str[size - 1 - i];
        }
        Console.Write($"Результат: [");
        for (int i = 0; i < size; i++)
        {
            if (i < size - 1)
            {
                Console.Write($"{str_pere[i]}, ");
            }
            else
            {
                Console.Write($"{str_pere[i]}]\n");
            }
        }
    }
}

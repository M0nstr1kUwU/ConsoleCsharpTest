using System.ComponentModel;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using static System.Net.Mime.MediaTypeNames;
using ClassTest.Class1NS;
using ClassTest.CalculatorNS;
using System.Data.Common;

class Student
{
    public string name;
    public string group;
    public string birthDate;

    public Student(string name, string group, string birthDate)
    {
        this.name = name;
        this.group = group;
        this.birthDate = birthDate;
    }

    public int GetAge(string currentDate)
    {
        string[] birthParts = birthDate.Split('.');
        int birthDay = int.Parse(birthParts[0]);
        int birthMonth = int.Parse(birthParts[1]);
        int birthYear = int.Parse(birthParts[2]);

        string[] currentParts = currentDate.Split('.');
        int currentDay = int.Parse(currentParts[0]);
        int currentMonth = int.Parse(currentParts[1]);
        int currentYear = int.Parse(currentParts[2]);

        int age = currentYear - birthYear;

        if (currentMonth < birthMonth ||
            (currentMonth == birthMonth && currentDay < birthDay))
        {
            age--;
        }

        return age;
    }
}

class Bank
{
    private string name;
    private string pinCode;
    private decimal money;

    public Bank(string name, string pinCode, decimal initialMoney = 0)
    {
        this.name = name;
        this.pinCode = pinCode;
        this.money = initialMoney;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            return;
        }
        money += amount;
        Console.WriteLine($"Счёт пополнен на {amount} руб. Текущий баланс: {money} руб");
    }

    public bool Withdraw(decimal amount, string pin)
    {
        if (pin != pinCode)
        {
            Console.WriteLine("Неверный пин-код!");
            return false;
        }
        if (amount <= 0)
        {
            Console.WriteLine("Сумма снятия должна быть +");
            return false;
        }
        if (amount > money)
        {
            Console.WriteLine("Недостаточно средств на счёте");
            return false;
        }
        money -= amount;
        Console.WriteLine($"Снято {amount} руб. Остаток: {money} руб");
        return true;
    }

    public void ShowBalance(string pin)
    {
        if (pin != pinCode)
        {
            Console.WriteLine("Неверный пин-код!");
            return;
        }
        Console.WriteLine($"Баланс счёта: {money} руб");
    }
}

class TestMoney
{
    public string Name;
    public int Value;

    public TestMoney(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public TestMoney add(TestMoney other)
    {
        if (Name != other.Name) Console.WriteLine("Ошибка: разные вылюты");
        return new TestMoney(Value + other.Value, Name);
    }

    public TestMoney sub(TestMoney other)
    {
        if (Name != other.Name) Console.WriteLine("Ошибка: разные вылюты");
        return new TestMoney(Value - other.Value, Name);
    }

    public TestMoney div(TestMoney other)
    {
        if (Name != other.Name) Console.WriteLine("Ошибка: разные вылюты");
        if (other.Value == 0)
        {
            Console.WriteLine("Ошибка: на 0 делить нельзя!");
            return other;
        }
        return new TestMoney(Value / other.Value, Name);
    }

    public TestMoney mul(TestMoney other)
    {
        if (Name != other.Name) Console.WriteLine("Ошибка: разные вылюты");
        return new TestMoney(Value * other.Value, Name);
    }


    public static TestMoney operator +(TestMoney a, TestMoney b)
    {
        return a.add(b);
    }

    public static TestMoney operator -(TestMoney a, TestMoney b)
    {
        return a.sub(b);
    }

    public static TestMoney operator /(TestMoney a, TestMoney b)
    {
        return a.div(b);
    }

    public static TestMoney operator *(TestMoney a, TestMoney b)
    {
        return a.mul(b);
    }
}

class ArrayTwo
{
    public int[,] Array;

    public ArrayTwo(int[,] array)
    {
        Array = array;
    }

    public ArrayTwo add(ArrayTwo other)
    {
        for (int i = 0; i < Array.Length; i++){
            for (int j = 0; j < Array.Length; j++)
            {
                Array[i, j] += other.Array[i, j];
            }
        }
        return new ArrayTwo(Array);
    }

    public ArrayTwo sub(ArrayTwo other)
    {
        for (int i = 0; i < Array.Length; i++)
        {
            for (int j = 0; j < Array.Length; j++)
            {
                Array[i, j] -= other.Array[i, j];
            }
        }
        return new ArrayTwo(Array);
    }

    public ArrayTwo mul(ArrayTwo other)
    {
        for (int i = 0; i < Array.Length; i++)
        {
            for (int j = 0; j < Array.Length; j++)
            {
                Array[i, j] *= other.Array[i, j];
            }
        }
        return new ArrayTwo(Array);
    }

    public ArrayTwo div(ArrayTwo other)
    {
        for (int i = 0; i < Array.Length; i++)
        {
            for (int j = 0; j < Array.Length; j++)
            {
                if (other.Array[i, j] != 0) Array[i, j] /= other.Array[i, j];
                else Array[i, j] = 0;
            }
        }
        return new ArrayTwo(Array);
    }

    public static ArrayTwo operator +(ArrayTwo a, ArrayTwo b)
    {
        return a.add(b);
    }

    public static ArrayTwo operator -(ArrayTwo a, ArrayTwo b)
    {
        return a.sub(b);
    }

    public static ArrayTwo operator *(ArrayTwo a, ArrayTwo b)
    {
        return a.mul(b);
    }

    public static ArrayTwo operator /(ArrayTwo a, ArrayTwo b)
    {
        return a.div(b);
    }
}

internal class TimeAtomic
{
    public int Hour;
    public int Minute;


    public TimeAtomic(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
    }

    public static bool operator >(TimeAtomic a, TimeAtomic b)
    {
        int af = a.Hour * 60 + a.Minute;
        int bf = b.Hour * 60 + b.Minute;
        if (af > bf) return true;
        else return false;
    }

    public static bool operator <(TimeAtomic a, TimeAtomic b)
    {
        int af = a.Hour * 60 + a.Minute;
        int bf = b.Hour * 60 + b.Minute;
        if (af < bf) return true;
        else return false;
    }

    public static bool operator ==(TimeAtomic a, TimeAtomic b)
    {
        int af = a.Hour * 60 + a.Minute;
        int bf = b.Hour * 60 + b.Minute;
        if (af == bf) return true;
        else return false;
    }

    public static bool operator !=(TimeAtomic a, TimeAtomic b)
    {
        int af = a.Hour * 60 + a.Minute;
        int bf = b.Hour * 60 + b.Minute;
        if (af != bf) return true;

        else return false;
    }
}

class OperatorPatches
{
    public int a, b, c;
    public OperatorPatches(int a, int b, int c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public static OperatorPatches operator ++(OperatorPatches part)
    {
        part.c++;
        return part;
    }

    public static OperatorPatches operator +(OperatorPatches part, int p)
    {
        switch (p)
        {
            case 1:
                part.a++;
                return part;
            case 2:
                part.b++;
                return part;
            case 3:
                part.c++;
                return part;
            default:
                return part;
        }
    }

    public static OperatorPatches operator -(OperatorPatches part, int p)
    {
        switch (p)
        {
            case 1:
                part.a--;
                return part;
            case 2: 
                part.b--;
                return part;
            case 3:
                part.c--;
                return part;
            default:
                return part;
        }
    }

    public static bool operator >(OperatorPatches patch1, OperatorPatches patch2)
    {
       if (patch1.a > patch2.a)
            return true;
       else if (patch1.a <= patch2.a)
            if (patch1.a == patch2.a)
                if (patch1.b > patch2.b)
                    return true;
                else if (patch1.b <= patch2.b)
                    if (patch1.b == patch2.b)
                        if (patch1.c > patch2.c)
                            return true;
                        else return false;
    }

    public static bool operator <(OperatorPatches patch1, OperatorPatches patch2)
    {
        if (patch1.a < patch2.a)
            return true;
        else if (patch1.a >= patch2.a)
            if (patch1.a == patch2.a)
                if (patch1.b < patch2.b)
                    return true;
                else if (patch1.b >= patch2.b)
                    if (patch1.b == patch2.b)
                        if (patch1.c < patch2.c)
                            return true;
                        else return false;
    }

    public static bool operator ==(OperatorPatches patch1, OperatorPatches patch2)
    {
        if (patch1.a == patch2.a && patch1.b == patch2.b && patch1.c == patch2.c)
            return true;
        else return false;
    }

    public static bool operator !=(OperatorPatches patch1, OperatorPatches patch2)
    {
        if (patch1.a != patch2.a || patch1.b != patch2.b || patch1.c != patch2.c)
            return true;
        else return false;
    }
}


class MainBank
{
    private string name;
    private int balance;
    public MainBank(string name, int balance)
    {
        this.name = name;
        this.balance = balance;
    }

    public void deposit(UserMainBank user, int amount)
    {
        user.balance += amount;
        Console.WriteLine($"{user.name} положил {amount}$");
    }

    public void withdraw(UserMainBank user, int amount)
    {
        user.balance -= amount;
        Console.WriteLine($"{user.name} снял {amount}$");
    }

    public void transaction(UserMainBank user1, UserMainBank user2, int amount)
    {
        if (user1.balance >= amount)
        {
            user1.balance -= amount;
            user2.balance += amount;
            Console.WriteLine($"{user1.name} перевёл {user2.name} {amount}$");
        }
        else Console.WriteLine($"У {user1.name} недостаточно средст на балансе для перевода {amount}$");
        user1.show(user1);
    }

    public void show(UserMainBank user)
    {
        Console.WriteLine($"Баланс {user.name}: {user.balance}$");
    }
}

class UserMainBank : MainBank
{
    public UserMainBank(string name_c, int balance_c) : base(name_c, balance_c){}
}

class Matrix
{
    private int[,] matrix;
    public int R { get; }
    public int C { get; }

    public Matrix(int r, int c)
    {
        R = r;
        C = c;
        matrix = new int[r, c];
    }

    public int this[int i, int j]
    {
        get { return matrix[i, j]; }
        set { matrix[i, j] = value; }
    }

    public static Matrix sum(Matrix a, Matrix b)
    {
        if (a.R != b.R || a.C != b.C)
        {
            throw new Exception("Размеры не совпадают, не могу посчитать :(");
        }


        Matrix result = new Matrix(a.R, a.C);
        for (int i = 0; i < a.R; i++)
            for (int j = 0; j < a.C; j++)
                result[i, j] = a[i, j] + b[i, j];
        return result;
    }

    public void p()
    {
        Console.WriteLine("Result:");
        for (int i = 0; i < C; i++)
        {
            Console.Write("{ ");
            for (int j = 0; j < C; j++)
            {
                if (j < C - 1)
                {
                    Console.Write($"{matrix[i, j]}, ");
                }
                else
                {
                    Console.Write($"{matrix[i, j]} ");
                }
            }
            Console.Write("}\n");
        }
    }
}

class Vehicle
{
    public int Speed { get; set; }
    public int Distance { get; set; }
    public int Time { get; set; }

    public Vehicle(int speed, int time, int distance = 0)
    {
        Speed = speed;
        Distance = distance;
        Time = time;
    }
    public virtual void move(Vehicle f)
    {
        f.Distance = f.Speed * f.Time;
        Console.WriteLine($"Distance: {f.Distance}, (Speed: {f.Speed}, Time: {f.Time})");
    }
}

class Bicycle : Vehicle
{
    public Bicycle(int speed, int time, int distance = 0) : base(speed + 1, time + 1, distance + 1) { }
    public override void move(Vehicle f)
    {
        base.move(f);
    }
}

class Car : Vehicle
{
    public Car(int speed, int time, int distance = 0) : base(speed + 10, time, distance) { }

    public override void move(Vehicle f)
    {
        base.move(f);
    }
}

public class Game
{
    protected int hp;
    public int HP => hp;
    protected int count_regen;
    protected int armor;
    public Game(int hp, int c_regen, int armor)
    {
        this.hp = hp;
        count_regen = c_regen;
        this.armor = armor;
    }

    public void attack(Game target, int damage)
    {
        target.hp -= (damage - target.armor);
        Console.WriteLine($"Нанесено {damage} урона");
    }

    public void regen(Game target, int amount)
    {
        if (count_regen > 0)
        {
            target.hp += amount;
            target.count_regen--;
            Console.WriteLine($"Исцелено {amount} хп");
        }
        else
        {
            Console.WriteLine("Хилок больше не осталось, атакуем");
            target.hp -= (amount - target.armor);
            Console.WriteLine($"Нанесено {amount} урона");
        }
    }
}

public class Character : Game
{
    private string name_c;
    private int damage_c;
    private int amount_c;
    private int count_call;
    private int count_explosion;
    private int c_attack_for_exp = 3;

    public Character(string name, int hp, int c_regen, int armor, int c_call, int c_explosion) : base(hp, c_regen, armor)
    {
        name_c = name;
        damage_c = 0;
        amount_c = 0;
        count_call = c_call;
        count_regen = c_regen;
        count_explosion = c_explosion;
    }

    public void attack(Game target)
    {
        damage_c = RandomNumberGenerator.GetInt32(5, 15);
        Console.WriteLine($"{name_c} атакует");
        base.attack(target, damage_c);
        if (c_attack_for_exp > 0) c_attack_for_exp--;
        else
        {
            c_attack_for_exp = 3;
            count_explosion++;
        }
    }

    public void regen(Game target)
    {
        amount_c = RandomNumberGenerator.GetInt32(10, 15);
        Console.WriteLine($"{name_c} исцеляется");
        base.regen(target, amount_c);
    }

    public void call(Teamate merlin, Game target)
    {
        if (count_call > 0 && !merlin.is_called())
        {
            Console.WriteLine($"{name_c} призывает союзника");
            count_call--;
            merlin.regen(merlin);
            merlin.call_add();
            merlin.attack(target);
        }
        else
        {
            Console.WriteLine("Невозможно призвать Мерлин сейчас, но она выслала тебе хилку");
            count_regen++;
        }

    }

    public void explosion(Game target, Game player)
    {
        if (count_explosion > 0)
        {
            Console.WriteLine($"{name_c} использует !EXPLOSION!");
            count_explosion--;
            player.attack(target, 100);
            player.regen(player, 20);
        }
        else
        {
            Console.WriteLine($"{name_c} не может кастануть имбу, атак осталось ({c_attack_for_exp})");
        }
    }

    public void status()
    {
        Console.WriteLine($"{name_c} HP: {hp} | Regen: {count_regen} | Exp: {count_explosion}");
    }
}

public class Teamate : Game
{
    private string name_c;
    private int damage_c;
    private int amount_c;
    private bool is_call = false;

    public Teamate(string name, int hp, int c_regen, int armor) : base(hp, c_regen, armor)
    {
        name_c = name;
        damage_c = 0;
        amount_c = 0;
        count_regen = c_regen;
    }

    public void attack(Game target)
    {
        damage_c = RandomNumberGenerator.GetInt32(5, 15);
        Console.WriteLine($"{name_c} атакует");
        base.attack(target, damage_c);
    }

    public void regen(Game target)
    {
        amount_c = RandomNumberGenerator.GetInt32(50, 100);
        Console.WriteLine($"{name_c} исцеляется");
        base.regen(target, amount_c);
    }

    public void status()
    {
        Console.WriteLine($"{name_c} HP: {hp} | Regen: {count_regen}");
        if (hp <= 0)
        {
            Console.WriteLine("Мерлин отступает, дальше ты сам!");
            is_call = false;
        }
    }

    public void call_add()
    {
        is_call = true;
    }

    public bool is_called()
    {
        return is_call;
    }
}



public class Enemy : Game
{
    private string name_c;
    private int damage_c;
    private int amount_c;

    public Enemy(string name, int hp, int c_regen, int armor_c) : base(hp, c_regen, armor_c)
    {
        name_c = name;
        damage_c = 0;
        amount_c = 0;
        count_regen = c_regen;
    }

    public void attack(Game target)
    {
        damage_c = RandomNumberGenerator.GetInt32(8, 10);
        Console.WriteLine($"{name_c} атакует");
        base.attack(target, damage_c);
    }
    public void regen(Game target)
    {
        amount_c = RandomNumberGenerator.GetInt32(10, 15);
        Console.WriteLine($"{name_c} исцеляется");
        base.regen(target, amount_c);
    }

    public void ultimate(Game target, Game enemy_c)
    {
        damage_c = RandomNumberGenerator.GetInt32(15, 30);
        Console.WriteLine($"{name_c} использует ultimate");
        base.attack(target, damage_c);
        count_regen++;
        base.regen(enemy_c, RandomNumberGenerator.GetInt32(5, 20));
    }

    public void status()
    {
        Console.WriteLine($"{name_c} HP: {hp} | Regen: {count_regen}");
    }
}

class CalculatorProtecteionError
{
    public void addition()
    {
        try
        {
            Console.Write("A: ");
            int a = int.Parse(Console.ReadLine() ?? "");
            Console.Write("B: ");
            int b = int.Parse(Console.ReadLine() ?? "");
            Console.WriteLine($"{a} + {b} = {a + b}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nОшибка {e.GetType().Name}: {e.Message}\n" +
                $"[{e.TargetSite}, {e.StackTrace}, {e.Source}]\n" +
                $"{e.HResult}\n");
        }
    }

    public void substraction()
    {
        try
        {
            Console.Write("A: ");
            int a = int.Parse(Console.ReadLine() ?? "");
            Console.Write("B: ");
            int b = int.Parse(Console.ReadLine() ?? "");
            Console.WriteLine($"{a} - {b} = {a - b}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nОшибка {e.GetType().Name}: {e.Message}\n" +
                $"[{e.TargetSite}, {e.StackTrace}, {e.Source}]\n" +
                $"{e.HResult}\n");
        }
    }

    public void multiplication()
    {
        try
        {
            Console.Write("A: ");
            int a = int.Parse(Console.ReadLine() ?? "");
            Console.Write("B: ");
            int b = int.Parse(Console.ReadLine() ?? "");
            Console.WriteLine($"{a} * {b} = {a * b}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nОшибка {e.GetType().Name}: {e.Message}\n" +
                $"[{e.TargetSite}, {e.StackTrace}, {e.Source}]\n" +
                $"{e.HResult}\n");
        }
    }

    public void division()
    {
        try
        {
            Console.Write("A: ");
            int a = int.Parse(Console.ReadLine() ?? "");
            Console.Write("B: ");
            int b = int.Parse(Console.ReadLine() ?? "");
            Console.WriteLine($"{a} / {b} = {a / b}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nОшибка {e.GetType().Name}: {e.Message}\n" +
                $"[{e.TargetSite}, {e.StackTrace}, {e.Source}]\n" +
                $"{e.HResult}\n");
        }
    }

    public void percentage_division()
    {
        try
        {
            Console.Write("A: ");
            int a = int.Parse(Console.ReadLine() ?? "");
            Console.Write("B: ");
            int b = int.Parse(Console.ReadLine() ?? "");
            Console.WriteLine($"{a} % {b} = {a % b}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nОшибка {e.GetType().Name}: {e.Message}\n" +
                $"[{e.TargetSite}, {e.StackTrace}, {e.Source}]\n" +
                $"{e.HResult}\n");
        }
    }

    public void addition_array()
    {
        try
        {
            Console.Write("Size: ");
            int size = int.Parse(Console.ReadLine() ?? "");
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
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nОшибка {e.GetType().Name}: {e.Message}\n" +
                $"[{e.TargetSite}, {e.StackTrace}, {e.Source}]\n" +
                $"{e.HResult}\n");
        }
    }
}

class ErrorBalance : Exception
{
    private int balance;

    public ErrorBalance(int balance) : base($"Недостаточно средств! Баланс: {balance}$")
    {
        this.balance = balance;
    }

    public void input(int amount)
    {
        try
        {
            balance += amount;
            Console.WriteLine($"Баланс пополнен на {amount}$");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.GetType().Name}: {e.Message}, {e.TargetSite}");
        }

    }

    public void output(int amount)
    {
        try
        {
            if (amount > balance)
            {
                throw new ErrorBalance(balance);
            }
            else
            {
                balance -= amount;
                Console.WriteLine($"Баланс снят на {amount}$");
            }
        }
        catch (ErrorBalance e) { Console.WriteLine(e.Message); }
    }

    public void show_balance()
    {
        Console.WriteLine($"Баланс: {balance}$");
    }
}

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
                "22. Перевёрнутый массив функцией\n" +
                "23. Вывод только четных значений массива через функцию\n" +
                "24. Сортировка массива по кол-ву делителей\n" +
                "25. Сортировку пузырьком двумерного массива\n" +
                "26. Сумма главной диагонали матрицы\n" +
                "27. Реверс каждой строки матрицы\n" +
                "28. Вычисление возраста студента\n" +
                "29. Банк Демоверсия (full в стране Россия)\n" +
                "30. Сумма матриц (class)\n" +
                "31. Родительские и дочерние классы\n" +
                "32. Демо игра\n" +
                "33. Симулятор плохого программиста (специально пишем код с ошибками и ловим их)\n" +
                "34. Калькулятор с обработчиками ошибок\n" +
                "35. Банк с перехватом ошибок\n" +
                "36. Вывод hw из другого файла\n" +
                "37. Калькулятор с обработкой ошибок в другом файле\n" +
                "38. Банк из родительского и дочерного классов\n" +
                "39. Перегрузка операторов\n" +
                "40. Перугрузка двумерного вектора\n" +
                "41. Перегрузка условных операторов (просто есть)\n" +
                "42. Перегрузка условных операторов для патчей\n"
            );
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
                case 23:
                    fill_char('~', 30);
                    output_chet_num_array();
                    continue;
                case 24:
                    fill_char('~', 30);
                    sort_array_for_count_del();
                    continue;
                case 25:
                    fill_char('~', 30);
                    sort_bottle_2_array();
                    continue;
                case 26:
                    fill_char('~', 30);
                    sum_main_diagonal_matrix();
                    continue;
                case 27:
                    fill_char('~', 30);
                    reverse_str_matrix();
                    continue;
                case 28:
                    fill_char('~', 30);
                    solve_student_age();
                    continue;
                case 29:
                    fill_char('~', 30);
                    bankDemo();
                    continue;
                case 30:
                    fill_char('~', 30);
                    sum_matrix_class();
                    continue;
                case 31:
                    fill_char('~', 30);
                    nafdkodf();
                    continue;
                case 32:
                    fill_char('~', 30);
                    demo_game();
                    continue;
                case 33:
                    fill_char('~', 30);
                    exception_result();
                    continue;
                case 34:
                    fill_char('~', 30);
                    exception_class_result();
                    continue;
                case 35:
                    fill_char('~', 30);
                    bank_class_exception();
                    continue;
                case 36:
                    fill_char('~', 30);
                    Class1 dafae = new Class1();
                    dafae.print_hw();
                    Console.Write("A: ");
                    int a_f = int.Parse(Console.ReadLine() ?? "");
                    Console.Write("B: ");
                    int b_f = int.Parse(Console.ReadLine() ?? "");
                    dafae.addAB(a_f, b_f);
                    continue;
                case 37:
                    fill_char('~', 30);
                    Calculator danon = new Calculator();
                    try
                    {
                        Console.Write("A: ");
                        double ad = double.Parse(Console.ReadLine() ?? "");
                        Console.Write("Операция (+ - * /): ");
                        string op = Console.ReadLine() ?? "";
                        Console.Write("B: ");
                        double bd = double.Parse(Console.ReadLine() ?? "");
                        double result = 0;
                        switch (op)
                        {
                            case "+":
                                result = danon.add(ad, bd);
                                break;

                            case "-":
                                result = danon.sub(ad, bd);
                                break;

                            case "*":
                                result = danon.mul(ad, bd);
                                break;

                            case "/":
                                result = danon.div(ad, bd);
                                break;
                            default:
                                Console.WriteLine("Неизвестная операция");
                                return;
                        }
                        Console.WriteLine($"Результат: {result}");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: введено не число");
                    }
                    catch (DivideByZeroException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
                    }
                    continue;
                case 38:
                    fill_char('~', 30);
                    // dodelat
                    continue;
                case 39:
                    fill_char('~', 30);
                    atomic_operators();
                    continue;
                case 40:
                    fill_char('~', 30);
                    atomic_array_two();
                    continue;
                case 41:
                    fill_char('~', 30);
                    atomic_operators();
                    continue;
                case 42:
                    fill_char('~', 30);
                    patch_operators();
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

    static void solve_student_age()
    {
        Student s = new Student("Иван Иванович", "ГД-2", "04.12.2008");
        string today = "04.05.2028";
        Console.WriteLine($"Возраст: {s.GetAge(today)}");
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

    static string[] words =
    {
         "кот", "ток", "дом", "мод", "нос", "сон",
         "мир", "рим", "лес", "сел", "рак", "кар"
    };

    static void check_word_in_array_50()
    {
        Console.Write("Слово: ");
        string searchWord = Console.ReadLine() ?? "";
        if (words.Contains(searchWord))
            Console.WriteLine("Найдено");
        else
            Console.WriteLine("Нет");
        Console.WriteLine();
    }

    static void check_annogram_in_array()
    {
        Console.Write("Слово: ");
        string input = Console.ReadLine() ?? "";
        var anagrams = words.Where(w =>
            w.Length == input.Length &&
            String.Concat(w.OrderBy(c => c)) ==
            String.Concat(input.OrderBy(c => c))
        );
        Console.WriteLine("Анаграммы:");
        foreach (var word in anagrams)
        {
            Console.WriteLine(word);
        }
        Console.WriteLine();
    }

    static void sort_for_last_index()
    {
        var sorted = words.OrderBy(w => w[w.Length - 1]);
        Console.WriteLine("Сортировка по последней букве:");
        foreach (var word in sorted)
        {
            Console.WriteLine(word);
        }
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
            Console.Write($"{i + 1}. ");
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
        for (int i = 0; i < size; i++)
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

    static void output_chet_num_array()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");

        int[] num = new int[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            num[i] = int.Parse(Console.ReadLine() ?? "");
        }

        for (int i = 0; i < size; i++)
        {
            if (num[i] % 2 == 0)
            {
                Console.Write($"{num[i]}");
            }
        }
    }

    static void sort_array_for_count_del()
    {
        Console.Write("Size: ");
        int size = int.Parse(Console.ReadLine() ?? "");
        if (size <= 0) return;

        int[] arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{i + 1}. ");
            arr[i] = int.Parse(Console.ReadLine() ?? "");
        }

        Console.Write("Массив: [");
        for (int i = 0; i < size; i++)
        {
            if (i == size - 1)
                Console.Write($"{arr[i]}]\n");
            else
                Console.Write($"{arr[i]}, ");
        }

        static int CountDivisors(int n)
        {
            n = Math.Abs(n);
            if (n == 0) return 0;
            int count = 0;
            for (int i = 1; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    count++;
                    if (i != n / i) count++;
                }
            }
            return count;
        }

        int[] dc = new int[size];
        for (int i = 0; i < size; i++)
            dc[i] = CountDivisors(arr[i]);

        for (int i = 0; i < size - 1; i++)
        {
            for (int j = 0; j < size - 1 - i; j++)
            {
                if (dc[j] > dc[j + 1] ||
                    (dc[j] == dc[j + 1] && arr[j] > arr[j + 1]))
                {
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    (dc[j], dc[j + 1]) = (dc[j + 1], dc[j]);
                }
            }
        }

        Console.Write("Результат: [");
        for (int i = 0; i < size; i++)
        {
            if (i == size - 1)
                Console.Write($"{arr[i]}]\n");
            else
                Console.Write($"{arr[i]}, ");
        }
    }

    static void sort_bottle_2_array()
    {
        Console.Write("Столбцов: ");
        int str = int.Parse(Console.ReadLine() ?? "");
        Console.Write("Строк: ");
        int stb = int.Parse(Console.ReadLine() ?? "");

        int[,] arr = new int[str, stb];

        for (int i = 0; i < str; i++)
        {
            for (int j = 0; j < stb; j++)
            {
                Console.Write($"{i + 1}.{j + 1}: ");
                arr[i, j] = int.Parse(Console.ReadLine() ?? "");
            }
        }

        Console.WriteLine("Current array: ");
        for (int i = 0; i < str; i++)
        {
            Console.Write("{ ");
            for (int j = 0; j < stb; j++)
            {
                if (j < stb - 1)
                {
                    Console.Write($"{arr[i, j]}, ");
                }
                else
                {
                    Console.Write($"{arr[i, j]} ");
                }
            }
            Console.Write("}\n");
        }

        int f = str * stb;
        int[] arr1 = new int[f];
        int ix = 0;

        foreach (int x in arr)
        {
            arr1[ix++] = x;
        }

        for (int i = 0; i < f - 1; i++)
        {
            for (int j = 0; j < f - 1 - i; j++)
            {
                if (arr1[j] > arr1[j + 1])
                {
                    int temp = arr1[j];
                    arr1[j] = arr1[j + 1];
                    arr1[j + 1] = temp;
                }
            }

        }

        ix = 0;
        for (int i = 0; i < str; i++)
        {
            for (int j = 0; j < stb; j++)
            {
                arr[i, j] = arr1[ix++];
            }
        }

        Console.WriteLine("Sort array: ");
        for (int i = 0; i < str; i++)
        {
            Console.Write("{ ");
            for (int j = 0; j < stb; j++)
            {
                if (j < stb - 1)
                {
                    Console.Write($"{arr[i, j]}, ");
                }
                else
                {
                    Console.Write($"{arr[i, j]} ");
                }
            }
            Console.Write("}\n");
        }
    }

    static void sum_main_diagonal_matrix()
    {
        Console.Write("Столбцов: ");
        int str = int.Parse(Console.ReadLine() ?? "");
        Console.Write("Строк: ");
        int stb = int.Parse(Console.ReadLine() ?? "");

        int[,] arr = new int[str, stb];
        int sum = 0;

        for (int i = 0; i < str; i++)
        {
            for (int j = 0; j < stb; j++)
            {
                Console.Write($"{i + 1}.{j + 1}: ");
                arr[i, j] = int.Parse(Console.ReadLine() ?? "");
            }
        }

        Console.WriteLine("Array: ");
        for (int i = 0; i < str; i++)
        {
            Console.Write("{ ");
            for (int j = 0; j < stb; j++)
            {
                if (j < stb - 1)
                {
                    Console.Write($"{arr[i, j]}, ");
                }
                else
                {
                    Console.Write($"{arr[i, j]} ");
                }
            }
            Console.Write("}\n");
        }

        for (int i = 0; i < str; i++)
        {
            for (int j = 0; j < stb; j++)
            {
                if (i == j)
                {
                    sum += arr[i, j];
                }
            }
        }
        Console.WriteLine($"Сумма: {sum}");
    }

    static void reverse_str_matrix()
    {
        Console.Write("Столбцов: ");
        int str = int.Parse(Console.ReadLine() ?? "");
        Console.Write("Строк: ");
        int stb = int.Parse(Console.ReadLine() ?? "");

        int[,] arr = new int[str, stb];

        for (int i = 0; i < str; i++)
        {
            for (int j = 0; j < stb; j++)
            {
                Console.Write($"{i + 1}.{j + 1}: ");
                arr[i, j] = int.Parse(Console.ReadLine() ?? "");
            }
        }

        Console.WriteLine("Current array: ");
        for (int i = 0; i < str; i++)
        {
            Console.Write("{ ");
            for (int j = 0; j < stb; j++)
            {
                if (j < stb - 1)
                {
                    Console.Write($"{arr[i, j]}, ");
                }
                else
                {
                    Console.Write($"{arr[i, j]} ");
                }
            }
            Console.Write("}\n");
        }

        int[,] arr_rev = new int[str, stb];

        for (int i = 0; i < str; i++)
        {
            for (int j = 0; j < stb; j++)
            {
                arr_rev[i, j] = arr[i, stb - 1 - j];
            }
        }

        Console.WriteLine("Reverse array: ");
        for (int i = 0; i < str; i++)
        {
            Console.Write("{ ");
            for (int j = 0; j < stb; j++)
            {
                if (j < stb - 1)
                {
                    Console.Write($"{arr_rev[i, j]}, ");
                }
                else
                {
                    Console.Write($"{arr_rev[i, j]} ");
                }
            }
            Console.Write("}\n");
        }
    }

    static void sort_by_last_letter()
    {
        string[] wds = { "кот", "дом", "арбуз", "лес", "нос" };
        var sorted = wds.OrderBy(w => w[w.Length - 1]);
        foreach (var word in sorted)
        {
            Console.WriteLine(word);
        }
        Console.WriteLine();
    }

    static void sum_numbers_from_string_array()
    {
        string[] arr = { "10", "кот", "25", "abc", "5" };
        int sum = 0;
        foreach (string i in arr)
        {
            if (int.TryParse(i, out int number))
            {
                sum += number;
            }
        }
        Console.WriteLine($"Сумма чисел: {sum}");
    }

    static void check_power_two()
    {
        Console.Write("Введите число: ");
        int number = Convert.ToInt32(Console.ReadLine());
        if (IsPowerOfTwo(number))
            Console.WriteLine("Является");
        else
            Console.WriteLine("Не является");
    }

    static bool IsPowerOfTwo(int n)
    {
        if (n < 1) return false;
        while (n % 2 == 0)
        {
            n /= 2;
        }
        return n == 1;
    }

    static void bankDemo()
    {
        Console.Write("Пользователь: ");
        string name = Console.ReadLine() ?? "Unknown";
        Console.Write("Установите пин-код (цифры): ");
        string pin = Console.ReadLine() ?? "0000";
        Console.Write("Начальный взнос: ");
        decimal init = decimal.Parse(Console.ReadLine() ?? "0");

        Bank account = new Bank(name, pin, init);

        while (true)
        {
            Console.WriteLine($"\n--- БАНК: {name} ---\n" +
            "1. Пополнить счёт\n" +
            "2. Снять деньги\n" +
            "3. Показать баланс\n" +
            "Выбор: ");

            string choice = Console.ReadLine() ?? "0";

            if (choice == "0") break;

            if (name == "Unknown" || name == string.Empty)
            {
                Console.WriteLine("Сначала введите имя пользвателя!");
                Console.Write("Пользователь: ");
                name = Console.ReadLine() ?? "Unknown";
                continue;
            }

            switch (choice)
            {
                case "1":
                    Console.Write("Сумма пополнения: ");
                    decimal dep = decimal.Parse(Console.ReadLine() ?? "0");
                    account.Deposit(dep);
                    break;
                case "2":
                    Console.Write("Сумма снятия: ");
                    decimal wit = decimal.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Пин-код: ");
                    string p = Console.ReadLine() ?? "";
                    account.Withdraw(wit, p);
                    break;
                case "3":
                    Console.Write("Пин-код: ");
                    string p2 = Console.ReadLine() ?? "";
                    account.ShowBalance(p2);
                    break;
                default:
                    break;
            }
        }
    }

    static void sum_matrix_class()
    {
        Console.Write("Столбцов: ");
        int c = int.Parse(Console.ReadLine() ?? "");
        Console.Write("Строк: ");
        int r = int.Parse(Console.ReadLine() ?? "");

        Matrix A = new Matrix(r, c);
        Matrix B = new Matrix(r, c);

        Console.WriteLine("A Matrix: ");
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                Console.Write($"{i + 1}.{j + 1}: ");
                A[i, j] = int.Parse(Console.ReadLine() ?? "");
            }
        }

        Console.WriteLine("B Matrix:");
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                Console.Write($"{i + 1}.{j + 1}: ");
                B[i, j] = int.Parse(Console.ReadLine() ?? "");
            }
        }

        Matrix C = Matrix.sum(A, B);
        C.p();
    }

    static void nafdkodf()
    {
        Console.WriteLine("Vehicle:");
        Console.Write("Speed: ");
        int s = int.Parse(Console.ReadLine() ?? "");
        Console.Write("Time: ");
        int t = int.Parse(Console.ReadLine() ?? "");
        Vehicle A = new Vehicle(s, t);
        Bicycle B = new Bicycle(s, t);
        Car C = new Car(s, t);

        while (true)
        {
            Console.Write("Choice: ");
            int choice = int.Parse(Console.ReadLine() ?? "");
            switch (choice)
            {
                case 1:
                    A.move(A);
                    continue;
                case 2:
                    A.move(B);
                    continue;
                case 3:
                    A.move(C);
                    continue;
                case 0:
                    break;
            }
        }
    }

    static void demo_game()
    {
        Console.Write("Имя героя: ");
        string name = Console.ReadLine() ?? "";

        if (name == string.Empty || name.Length <= 2)
        {
            name = "Мелиодас";
        }

        Character hero = new Character(name, 100, 1, 0, 1, 1);
        Teamate team = new Teamate("Мерлин", 10, 1, 1);
        Enemy enemy = new Enemy("Зелдрис", 100, 7, 5);

        int choice, choice_enemy;
        while (true)
        {

            Console.WriteLine(
                "\n1. Атака\n" +
                "2. Исцеление\n" +
                "3. Призыв\n" +
                "4. [!] EXPLOSION"
                );
            Console.Write("> ");
            choice = int.Parse(Console.ReadLine() ?? "");
            switch (choice)
            {
                case 1:
                    fill_char('=', 30);
                    hero.attack(enemy);
                    if (team.is_called()) team.attack(enemy);
                    break;
                case 2:
                    fill_char('=', 30);
                    hero.regen(hero);
                    break;
                case 3:
                    fill_char('=', 30);
                    hero.call(team, enemy);
                    break;
                case 4:
                    fill_char('=', 30);
                    hero.explosion(enemy, hero);
                    break;
                default:
                    continue;
            }

            choice_enemy = RandomNumberGenerator.GetInt32(1, 4);
            switch (choice_enemy)
            {
                case 1:
                    if (team.is_called()) enemy.attack(team);
                    else enemy.attack(hero);
                    break;
                case 2:
                    fill_char('=', 30);
                    enemy.regen(enemy);
                    break;
                case 3:
                    fill_char('=', 30);
                    if (team.is_called()) enemy.ultimate(team, enemy);
                    else enemy.ultimate(hero, enemy);
                    break;

            }

            fill_char('-', 30);
            hero.status();
            if (team.is_called()) team.status();
            enemy.status();

            if (hero.HP <= 0 && enemy.HP <= 0)
            {
                Console.WriteLine("Оба пали в бою");
                break;
            }
            else if (hero.HP <= 0 && enemy.HP > 0)
            {
                Console.WriteLine("Враг победил");
                break;
            }
            else if (hero.HP > 0 && enemy.HP <= 0)
            {
                Console.WriteLine("Вы победили");
                break;
            }
        }
        fill_char('=', 30);
    }

    static void exception_result()
    {
        string er = "0";
        try
        {
            Console.Write("A: ");
            int a = int.Parse(Console.ReadLine() ?? "");
            Console.Write("B: ");
            int b = int.Parse(Console.ReadLine() ?? "");
            Console.WriteLine($"{a} / {b} = {a / b}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nОшибка {e.GetType().Name}: {e.Message}\n" +
                $"[{e.TargetSite}, {e.StackTrace}, {e.Source}]\n" +
                $"{e.HResult}\n");
            er = $"{e.HResult}";
        }
        finally
        {
            Console.WriteLine($"Метод завершен! {er}");
        }
    }

    static void exception_class_result()
    {
        int choice;
        CalculatorProtecteionError calculator = new CalculatorProtecteionError();
        while (true)
        {
            Console.Write("\n" +
                "1. Сложение\n" +
                "2. Вычитание\n" +
                "3. Умножение\n" +
                "4. Деление\n" +
                "5. Деление с остатком\n" +
                "6. Сложение массивов\n" +
                "> ");
            choice = int.Parse(Console.ReadLine() ?? "");
            switch (choice)
            {
                case 1:
                    calculator.addition();
                    continue;
                case 2:
                    calculator.substraction();
                    continue;
                case 3:
                    calculator.multiplication();
                    continue;
                case 4:
                    calculator.division();
                    continue;
                case 5:
                    calculator.percentage_division();
                    continue;
                case 6:
                    calculator.addition_array();
                    continue;
                case 0:
                    break;
            }
        }
    }

    static void bank_class_exception()
    {
        int choice, amount;
        ErrorBalance bank = new ErrorBalance(100);
        while (true)
        {
            try
            {
                Console.Write("\n" +
                    "1. Положить\n" +
                    "2. Вывести\n" +
                    "3. Баланс\n" +
                    "> ");
                choice = int.Parse(Console.ReadLine() ?? "");
                switch (choice)
                {
                    case 1:
                        Console.Write("Сумма: ");
                        amount = int.Parse(Console.ReadLine() ?? "");
                        bank.input(amount);
                        continue;
                    case 2:
                        Console.Write("Сумма: ");
                        amount = int.Parse(Console.ReadLine() ?? "");
                        bank.output(amount);
                        continue;
                    case 3:
                        bank.show_balance();
                        continue;
                    case 0:
                        break;
                    default:
                        continue;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType().Name}: {e.Message}, {e.TargetSite}");
            }
        }
    }

    static void atomic_operators()
    {
        int a = 20;
        int b = 10;

        TestMoney ad = new TestMoney(a, "rub");
        TestMoney bd = new TestMoney(b, "rub");
        Console.WriteLine(
            $"Add: {ad.Value + bd.Value}\n" +
            $"Sub: {ad.Value - bd.Value}\n" +
            $"Mul: {ad.Value * bd.Value}\n" +
            $"Div: {ad.Value / bd.Value}"
        );
    }

    static void atomic_array_two()
    {
        int[,] arr1 = new int[,] { 
            { 1, 2, 3 }, 
            { 4, 5, 6 }
        };
        int[,] arr2 = new int[,] {
            { 6, 5, 4 },
            { 3, 2, 1 }
        };

        ArrayTwo ad = new ArrayTwo(arr1);
        ArrayTwo bd = new ArrayTwo(arr2);
        for (int i = 0; i < arr1.Length; i++)
        {
            Console.Write("{ ");
            for (int j = 0; j < arr2.Length; j++)
            {
                if (j < arr2.Length - 1) Console.Write($"{ad.Array[i, j] + bd.Array[i, j]}, ");
                else Console.Write($"{ad.Array[i, j] + bd.Array[i, j]}");
            }
            Console.WriteLine("}");
        }
    }

    static void patch_operators()
    {
        int choice;
        int patch;
        int cp;
        OperatorPatches patch1 = new OperatorPatches(1, 0, 0);
        OperatorPatches patch2 = new OperatorPatches(1, 2, 0);
        while (true)
        {
            Console.WriteLine(
                "Patches: 1, 2\n" +
                "Actions:\n" +
                "1. version++\n" +
                "2. version+ patch\n" +
                "3. v > v\n" +
                "4. v < v\n" +
                "5. v == v\n" +
                "6. v != v"
            );
            Console.Write("> ");
            choice = int.Parse(Console.ReadLine() ?? "");
            Console.Write("Patch: ");
            patch = int.Parse(Console.ReadLine() ?? "");
            switch (choice)
            {
                case 1:
                    if (patch == 1) patch1++;
                    else if (patch == 2) patch2++;
                    continue;
                case 2:
                    if (patch == 1)
                    {
                        Console.Write("Part: ");
                        cp = int.Parse(Console.ReadLine() ?? "");
                        if (cp == 1) patch1.a += 1;
                        else if (cp == 2) patch1.b += 1;
                        else if (cp == 3) patch1.c += 1;
                    }
                    else if (patch == 2)
                    {
                        Console.Write("Part: ");
                        cp = int.Parse(Console.ReadLine() ?? "");
                        if (cp == 1) patch2.a += 1;
                        else if (cp == 2) patch2.b += 1;
                        else if (cp == 3) patch2.c += 1;
                    }
                    continue;
                case 3:
                    continue;
                case 4:
                    continue;
                case 5:
                    continue;
                case 6:
                    continue;
                case 0:
                    break;
                default:
                    continue;
            }
            Console.WriteLine(
                $"Patch1: {patch1.a}.{patch1.b}.{patch1.c}\n" +
                $"Patch2: {patch2.a}.{patch2.b}.{patch2.c}\n"
            );
        }
        
    }
}

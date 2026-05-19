using System;
using System.Globalization;
interface IMovable
{
    string Move();
}
abstract class Animal : IMovable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public string Habitat { get; set; }
    public abstract string Species { get; }
    public virtual string Chain => "Животное";
    protected Animal(string name, int age, double weight, string habitat)
    {
        Name = name;
        Age = age;
        Weight = weight;
        Habitat = habitat;
    }
    public abstract string Move();
    public virtual void PrintInfo()
    {
        Console.WriteLine("Вид: " + Species);
        Console.WriteLine("Иерархия: " + Chain);
        Console.WriteLine("Название: " + Name);
        Console.WriteLine("Возраст: " + Age);
        Console.WriteLine("Вес: " + Weight);
        Console.WriteLine("Среда обитания: " + Habitat);
        Console.WriteLine("Move(): " + Move());
    }
    ~Animal() { }
}
class Fish : Animal
{
    public string WaterType { get; set; }
    public override string Species => "Рыба";
    public override string Chain => "Животное -> Рыба";
    public Fish(string name, int age, double weight, string habitat, string waterType)
        : base(name, age, weight, habitat)
    {
        WaterType = waterType;
    }
    public override string Move() => "плавает с помощью плавников";
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("Тип воды: " + WaterType);
    }
    ~Fish() { }
}
abstract class Bird : Animal
{
    public double WingSpan { get; set; }
    public override string Chain => "Животное -> Птица";
    protected Bird(string name, int age, double weight, string habitat, double wingSpan)
        : base(name, age, weight, habitat)
    {
        WingSpan = wingSpan;
    }
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("Размах крыльев: " + WingSpan);
    }
    ~Bird() { }
}
abstract class Mammal : Animal
{
    public bool HasFur { get; set; }
    public override string Chain => "Животное -> Млекопитающее";
    protected Mammal(string name, int age, double weight, string habitat, bool hasFur)
        : base(name, age, weight, habitat)
    {
        HasFur = hasFur;
    }
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("Есть шерсть: " + (HasFur ? "да" : "нет"));
    }
    ~Mammal() { }
}
class Penguin : Bird
{
    public int ColonySize { get; set; }
    public override string Species => "Пингвин";
    public override string Chain => "Животное -> Птица -> Пингвин";
    public Penguin(string name, int age, double weight, string habitat, double wingSpan, int colonySize)
        : base(name, age, weight, habitat, wingSpan)
    {
        ColonySize = colonySize;
    }
    public override string Move() => "ходит по суше и плавает в воде";
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("Размер колонии: " + ColonySize);
    }
    ~Penguin() { }
}
class Dolphin : Mammal
{
    public double SonarDistance { get; set; }
    public override string Species => "Дельфин";
    public override string Chain => "Животное -> Млекопитающее -> Дельфин";
    public Dolphin(string name, int age, double weight, string habitat, bool hasFur, double sonarDistance)
        : base(name, age, weight, habitat, hasFur)
    {
        SonarDistance = sonarDistance;
    }
    public override string Move() => "плавает с помощью хвостового плавника";
    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("Дальность эхолокации: " + SonarDistance);
    }
    ~Dolphin() { }
}
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Иерархия: Animal -> Fish; Animal -> Bird -> Penguin; Animal -> Mammal -> Dolphin\n");
        Fish fish = new Fish(Read("Название рыбы: "), ReadInt("Возраст: "),
            ReadDouble("Вес: "), Read("Среда обитания: "), Read("Тип воды: "));
        Penguin penguin = new Penguin(Read("\nНазвание пингвина: "), ReadInt("Возраст: "),
            ReadDouble("Вес: "), Read("Среда обитания: "), ReadDouble("Размах крыльев: "),
            ReadInt("Размер колонии: "));
        Dolphin dolphin = new Dolphin(Read("\nНазвание дельфина: "), ReadInt("Возраст: "),
            ReadDouble("Вес: "), Read("Среда обитания: "), false,
            ReadDouble("Дальность эхолокации: "));
        Animal[] animals = { fish, penguin, dolphin };
        Console.WriteLine("\nМассив Animal[] содержит:");
        foreach (Animal animal in animals)
        {
            Console.WriteLine("------------------------------");
            animal.PrintInfo();
        }
        Console.WriteLine("\nОдин вызов Move() дал разные результаты, потому что классы переопределяют метод.");
    }
    static string Read(string text)
    {
        Console.Write(text);
        return Console.ReadLine() ?? "";
    }
    static int ReadInt(string text) => int.Parse(Read(text));
    static double ReadDouble(string text)
    {
        return double.Parse(Read(text).Replace(',', '.'), CultureInfo.InvariantCulture);
    }
}
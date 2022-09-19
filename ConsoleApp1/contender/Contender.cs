namespace ConsoleApp1.contender;

public class Contender
{
    public string Name { get; }
    public string Patronymic { get; }
    public int Prettiness { get; }

    public Contender(string name, string patronymic, int prettiness)
    {
        Name = name;
        Patronymic = patronymic;
        Prettiness = prettiness;
    }

    public bool IsBetter(object? obj)
    {
        if (obj is not Contender contender) return false;
        return Prettiness > contender.Prettiness;
    }
}
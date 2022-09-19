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

    public override bool Equals(object? obj)
    {
        if (obj is not Contender contender) return false;
        if (!Name.Equals(contender.Name)) return false;
        if (!Patronymic.Equals(contender.Patronymic)) return false;
        return Prettiness == contender.Prettiness;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Patronymic, Prettiness);
    }
}
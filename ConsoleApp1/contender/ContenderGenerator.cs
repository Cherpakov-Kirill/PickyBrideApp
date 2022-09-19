namespace ConsoleApp1.contender;

public class ContenderGenerator
{
    private static readonly string[] Names = new string[10]
        { "Виктор", "Владимир", "Владислав", "Петр", "Абрам", "Аввакум", "Август", "Аверьян", "Никифор", "Родион" };

    private readonly Random Random;

    public ContenderGenerator()
    {
        Random = new Random(DateTime.Now.Millisecond);
    }

    private string GetRandomName()
    {
        return Names[Random.Next(0, Names.Length - 1)];
    }

    private string GetPatronymic()
    {
        return GetRandomName() + "ович";
    }

    public List<Contender> GetContenders(int numberOfContenders)
    {
        var contenders = new List<Contender>();
        for (var i = 1; i <= numberOfContenders; i++)
        {
            contenders.Add(new Contender(GetRandomName(), GetPatronymic(), i));
        }
        return contenders;
    }
}
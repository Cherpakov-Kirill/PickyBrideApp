namespace PickyBride.contender;

public class ContenderGenerator : IContenderGenerator
{
    private static readonly string[] Names = { "Виктор", "Владимир", "Владислав", "Петр", "Абрам", "Аввакум", "Август", 
        "Аверьян", "Никифор", "Родион", "Кирилл", "Максим", "Артём", "Роман", "Семён"};

    private readonly Random _random;

    public ContenderGenerator()
    {
        _random = new Random(DateTime.Now.Millisecond);
    }

    private string GetRandomName()
    {
        return Names[_random.Next(0, Names.Length - 1)];
    }

    private string GetPatronymic()
    {
        return GetRandomName() + "ович";
    }

    public List<Contender> GetContenders(int numberOfContenders)
    {
        if (numberOfContenders <= 0) throw new ApplicationException("numberOfContenders should be more then zero");
        var contenders = new List<Contender>();
        var usedNames = new HashSet<string>();
        for (var i = 1; i <= numberOfContenders; i++)
        {
            var name = GetRandomName();
            var patronymic = GetPatronymic();
            while (usedNames.Contains(name + " " + patronymic))
            {
                name = GetRandomName();
                patronymic = GetPatronymic();
            }
            usedNames.Add(name + " " + patronymic);
            contenders.Add(new Contender(name, patronymic, i));
        }
        return contenders;
    }
}
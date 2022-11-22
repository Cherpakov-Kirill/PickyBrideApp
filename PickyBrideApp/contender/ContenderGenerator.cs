using PickyBride.database;

namespace PickyBride.contender;

public class ContenderGenerator : IContenderGenerator
{
    private static readonly string[] Names = { "Виктор", "Владимир", "Владислав", "Петр", "Абрам", "Аввакум", "Август", 
        "Аверьян", "Никифор", "Родион", "Кирилл", "Максим", "Артём", "Роман", "Семён"};

    private readonly int _numberOfContenders;

    private readonly Random _random;
    private readonly IDbController _dbController;

    public ContenderGenerator(IDbController dbController, int numberOfContenders)
    {
        _numberOfContenders = numberOfContenders;
        _random = new Random(DateTime.Now.Millisecond);
        _dbController = dbController;
    }

    private string GetRandomName()
    {
        return Names[_random.Next(0, Names.Length - 1)];
    }

    private string GetPatronymic()
    {
        return GetRandomName() + "ович";
    }

    public async Task<List<Contender>> GetContenders(int attemptNumber)
    {
        if (_numberOfContenders <= 0) throw new ApplicationException(resources.NumberOfContendersShouldBeMoreThenZero);
        var contenders = new List<Contender>();
        var usedNames = new HashSet<string>();
        
        for (var i = 1; i <= _numberOfContenders; i++)
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

        var random = new Random();
        contenders = contenders.OrderBy(x => random.Next(1, _numberOfContenders)).ToList();
        await _dbController.SaveAllContendersToDb(contenders, attemptNumber);
        return contenders;
    }
}
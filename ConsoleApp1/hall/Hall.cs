using ConsoleApp1.contender;

namespace ConsoleApp1.hall;

public class Hall : IHallForFriend, IHallForPrincess
{
    private readonly List<Contender> waitingContenders;
    private readonly Dictionary<int, Contender> visitedContenders;
    private readonly Random Random;

    public Hall(int maxNumberOfContenders)
    {
        var generator = new ContenderGenerator();
        waitingContenders = generator.GetContenders(maxNumberOfContenders);
        visitedContenders = new Dictionary<int, Contender>();
        Random = new Random(DateTime.Now.Millisecond);
    }

    public int GetNextContender()
    {
        if (waitingContenders.Count == 0) return -1;
        var randomValue = Random.Next(0, waitingContenders.Count - 1);
        var contender = waitingContenders[randomValue];
        waitingContenders.Remove(contender);
        var currentNumber = visitedContenders.Count + 1;
        visitedContenders.Add(currentNumber, contender);
        return currentNumber;
    }

    private void PrintAllVisitedContenders()
    {
        for (var princeId = 1; princeId <= visitedContenders.Count; princeId++)
        {
            var contender = visitedContenders[princeId];
            Console.WriteLine("#" + princeId + " : " + contender.Name + " " + contender.Patronymic + " : " +
                              contender.Prettiness);
        }
    }

    public int ChooseAPrince(int princeId)
    {
        //PrintAllVisitedContenders();
        if (princeId == -1)
        {
            Console.WriteLine("Result : " + 10);
            return 10;
        }
        if (visitedContenders.Count >= 50)
        {
            Console.WriteLine("Result : " + 0);
            return 0;
        }
        Console.WriteLine("Chosen contender id = " + princeId + " : " + visitedContenders[princeId].Prettiness);
        return visitedContenders[princeId].Prettiness;
    }

    public int IsFirstBetterThenSecond(int first, int second)
    {
        if (!visitedContenders.ContainsKey(first) || !visitedContenders.ContainsKey(second)) return -1;
        var firstContender = visitedContenders[first];
        var secondContender = visitedContenders[second];
        return firstContender.IsBetter(secondContender) ? 1 : 0;
    }
}
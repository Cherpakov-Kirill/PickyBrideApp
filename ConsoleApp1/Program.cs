using ConsoleApp1.friend;
using ConsoleApp1.hall;
using ConsoleApp1.princess;

namespace ConsoleApp1;

public static class Program
{
    private const int MaxNumberOfContenders = 100;
    private const double IterNumber = 100;

    public static void Main(string[] args)
    {
        const int skip = 12;
        const int percent = 93;
        double sum = 0;
        for (double i = 0; i < IterNumber; i++)
        {
            var hall = new Hall(MaxNumberOfContenders);
            var friend = new Friend(hall);
            var princess = new Princess(hall, friend);
            var localResult = princess.FindPrince(skip, percent);
            sum += localResult;
            Thread.Sleep(2);
        }

        var result = sum / IterNumber;
        Console.WriteLine("Average result = " + result);
    }
}
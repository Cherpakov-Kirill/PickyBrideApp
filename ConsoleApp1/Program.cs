using ConsoleApp1.friend;
using ConsoleApp1.hall;
using ConsoleApp1.princess;

namespace ConsoleApp1;

public static class Program
{
    private const int MaxNumberOfContenders = 100;

    public static void Main()
    {
        var hall = new Hall();
        hall.GenerateContenders(MaxNumberOfContenders);
        var friend = new Friend(hall);
        var princess = new Princess(hall, friend);
        princess.FindContender();
    }
}
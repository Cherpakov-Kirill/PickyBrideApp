using PickyBride.princess;
using PickyBride.friend;
using PickyBride.hall;

namespace PickyBride;

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
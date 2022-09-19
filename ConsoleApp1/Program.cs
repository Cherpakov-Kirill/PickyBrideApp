// See https://aka.ms/new-console-template for more information

using ConsoleApp1;
using ConsoleApp1.friend;
using ConsoleApp1.hall;
using ConsoleApp1.princess;

const int maxNumberOfContenders = 100;
const double iterNumber = 100;
double sum = 0;
for (double i = 0; i < iterNumber; i++)
{
    var hall = new Hall(maxNumberOfContenders);
    var friend = new Friend(hall);
    var princess = new Princess(hall, friend);
    var result = princess.FindPrince();
    sum += result;
    Thread.Sleep(50);
}

Console.WriteLine("Average for " + iterNumber + " iterations = " + sum / iterNumber);
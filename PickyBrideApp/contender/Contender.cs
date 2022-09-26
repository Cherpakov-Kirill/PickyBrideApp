namespace PickyBride.contender;

public class Contender : IComparable
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

    /// <summary>
    /// Compares this instance with instance in params
    /// </summary>
    /// <param name="obj">instance of Contender</param>
    /// <returns>
    /// Result of calling firstContenderPrettiness.CompareTo(secondContenderPrettiness)
    /// </returns>
    /// <exception cref="ArgumentException">Object is not a Contender</exception>
    public int CompareTo(object? obj)
    {
        if (obj is Contender otherContender)
            return Prettiness.CompareTo(otherContender.Prettiness);
        throw new ArgumentException("Object is not a Contender");
    }
}
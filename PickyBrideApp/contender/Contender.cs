namespace ConsoleApp1.contender;

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
    /// <list type="table">
    /// <item>
    /// <term><b>Returns</b></term>
    /// <description>
    /// <b>Description</b>
    /// </description>
    /// </item>
    /// <item>
    /// <term>Less than zero</term>
    /// <description>
    /// First contender is worse than second contender.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description>
    /// First contender is equal to second contender.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <description>
    /// First contender is better than second contender.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    /// <exception cref="ArgumentException">Object is not a Contender</exception>
    public int CompareTo(object? obj)
    {
        if (obj is Contender otherContender)
            return Prettiness.CompareTo(otherContender.Prettiness);
        throw new ArgumentException("Object is not a Contender");
    }
}
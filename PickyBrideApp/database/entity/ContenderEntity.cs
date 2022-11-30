using PickyBride.contender;

namespace PickyBride.database.entity;

public class ContenderEntity
{
    public int Id { set; get; }
    public string Name { set; get; }
    public string Patronymic { set; get; }
    public int Prettiness { set; get; }

    public override string ToString()
    {
        return $"{{Id={Id} Name={Name} Patronymic={Patronymic} Prettiness={Prettiness}}}";
    }

    public Contender ToContender()
    {
        return new Contender(Name, Patronymic, Prettiness);
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is ContenderEntity entity)
        {
            return Id == entity.Id &&
                   Name == entity.Name &&
                   Patronymic == entity.Patronymic &&
                   Prettiness == entity.Prettiness;
        }

        return false;
    }

    public override int GetHashCode() => Id.GetHashCode() + Name.GetHashCode() +
                                         Patronymic.GetHashCode() + Prettiness.GetHashCode();
}
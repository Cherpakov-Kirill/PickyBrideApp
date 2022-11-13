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
}
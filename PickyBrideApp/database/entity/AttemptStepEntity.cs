namespace PickyBride.database.entity;

public class AttemptStepEntity
{
    public int Id { set; get; }
    public int AttemptNumber { set; get; }
    public ContenderEntity Contender { set; get; }
    public int ContenderPosition { set; get; }

    public override string ToString()
    {
        return $"{{Id={Id} ContenderPosition={ContenderPosition} AttemptNumber={AttemptNumber} ContenderData={Contender.ToString()}}}";
    }
}
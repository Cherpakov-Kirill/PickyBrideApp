namespace HallWebApi.model.database.entity;

public class AttemptStepEntity
{
    public int Id { set; get; }
    public int AttemptNumber { set; get; }
    public ContenderEntity Contender { set; get; }
    public int ContenderPosition { set; get; }

    public override string ToString()
    {
        return
            $"{{Id={Id} ContenderPosition={ContenderPosition} AttemptNumber={AttemptNumber} ContenderData={Contender.ToString()}}}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is AttemptStepEntity entity)
        {
            return Id == entity.Id &&
                   AttemptNumber == entity.AttemptNumber &&
                   ContenderPosition == entity.ContenderPosition &&
                   Contender.Equals(entity.Contender);
        }

        return false;
    }

    public override int GetHashCode() => Id.GetHashCode() + AttemptNumber.GetHashCode() +
                                         ContenderPosition.GetHashCode() + Contender.GetHashCode();
}
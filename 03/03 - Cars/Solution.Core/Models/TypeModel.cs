namespace Solution.Core.Models;

public partial class TypeModel: ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string name;

    public TypeModel()
    {
    }

    public TypeModel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public TypeModel(CarTypeEntity entity)
    {
        if (entity is null)
        {
            return;
        }

        Id = entity.Id;
        Name = entity.Name;
    }

    public CarTypeEntity ToEntity()
    {
        return new CarTypeEntity
        {
            Id = this.Id,
            Name = this.Name
        };
    }

    public void ToEntity(CarTypeEntity entity)
    {
        entity.Id = this.Id;
        entity.Name = this.Name;
    }
}

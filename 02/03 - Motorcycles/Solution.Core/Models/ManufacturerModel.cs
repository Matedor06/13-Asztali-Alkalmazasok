namespace Solution.Core.Models;

public partial class ManufacturerModel : ObservableObject
{
    [ObservableProperty]
    [JsonPropertyName("id")]
    private int id;


    [ObservableProperty]
    [JsonPropertyName("name")]
    private string name;

    public ManufacturerModel()
    {
    }

    public ManufacturerModel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public ManufacturerModel(ManufacturerEntity entity)
    {
        if(entity is null)
        {
            return;
        }

        Id = entity.Id;
        Name = entity.Name;
    }

    public ManufacturerEntity ToEntity()
    {
        return new ManufacturerEntity
        {
            Id = this.Id,
            Name = this.Name
        };
    }

    public void ToEntity(ManufacturerEntity entity)
    {
        if (entity is null)
        {
            return;
        }

        entity.Id = this.Id;
        entity.Name = this.Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is ManufacturerModel model &&
               this.Id == model.Id &&
               this.Name == model.Name;
    }
}

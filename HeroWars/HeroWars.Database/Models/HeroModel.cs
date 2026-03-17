using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HeroWars.Database.Entities;
using HeroWars.Database.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HeroWars.Database.Models;

public class HeroModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("role")]
    public HeroRole Role { get; set; }

    [JsonPropertyName("intelligence")]
    public int Intelligence { get; set; }

    [JsonPropertyName("agility")]
    public int Agility { get; set; }

    [JsonPropertyName("strength")]
    public int Strength { get; set; }

    [JsonPropertyName("health")]
    public int Health { get; set; }

    [JsonPropertyName("physicalAttack")]
    public int PhysicalAttack { get; set; }

    [JsonPropertyName("magicAttack")]
    public int MagicAttack { get; set; }

    [JsonPropertyName("armor")]
    public int Armor { get; set; }

    [JsonPropertyName("magicDefense")]
    public int MagicDefense { get; set; }

    [JsonPropertyName("magicPenetration")]
    public int MagicPenetration { get; set; }

    [JsonPropertyName("armorPenetration")]
    public int ArmorPenetration { get; set; }

    public HeroModel() { }

    public HeroModel(HeroEntity entity)
    {
        if (entity is null)
        {
            return;
        }
        this.Id = entity.Id;
        this.Name = entity.Name;
        this.Role = entity.Role;
        this.Intelligence = entity.Intelligence;
        this.Agility = entity.Agility;
        this.Strength = entity.Strength;
        this.Health = entity.Health;
        this.PhysicalAttack = entity.PhysicalAttack;
        this.MagicAttack = entity.MagicAttack;
        this.Armor = entity.Armor;
        this.MagicDefense = entity.MagicDefense;
        this.MagicPenetration = entity.MagicPenetration;
        this.ArmorPenetration = entity.ArmorPenetration;
    }

    public HeroEntity ToEntity()
    {
        return new HeroEntity
        {
            Id = Id,
            Name = Name,
            Role = Role,
            Intelligence = Intelligence,
            Agility = Agility,
            Strength = Strength,
            Health = Health,
            PhysicalAttack = PhysicalAttack,
            MagicAttack = MagicAttack,
            Armor = Armor,
            MagicDefense = MagicDefense,
            MagicPenetration = MagicPenetration,
            ArmorPenetration = ArmorPenetration,
        };
    }

    public void ToEntity(HeroEntity entity)
    {
        entity.Id = Id;
        entity.Name = Name;
        entity.Role = Role;
        entity.Intelligence = Intelligence;
        entity.Agility = Agility;
        entity.Strength = Strength;
        entity.Health = Health;
        entity.PhysicalAttack = PhysicalAttack;
        entity.MagicAttack = MagicAttack;
        entity.Armor = Armor;
        entity.MagicDefense = MagicDefense;
        entity.MagicPenetration = MagicPenetration;
        entity.ArmorPenetration = ArmorPenetration;
    }
}

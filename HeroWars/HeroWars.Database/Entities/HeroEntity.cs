using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HeroWars.Database.Enums;

namespace HeroWars.Database.Entities;

[Table("Hero")]
public class HeroEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public HeroRole Role { get; set; }

    [Required]
    public int Intelligence { get; set; }

    [Required]
    public int Agility { get; set; }

    [Required]
    public int Strength { get; set; }

    [Required]
    public int Health { get; set; }

    [Required]
    public int PhysicalAttack { get; set; }

    [Required]
    public int MagicAttack { get; set; }

    [Required]
    public int Armor { get; set; }

    [Required]
    public int MagicDefense { get; set; }

    [Required]
    public int MagicPenetration { get; set; }

    [Required]
    public int ArmorPenetration { get; set; }
}

namespace Solution.Database.Entities;

[Table("Type")]
[Index(nameof(Name), IsUnique = true)]
public class CarTypeEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(64)]
    [Required]
    public string Name { get; set; }

    public virtual ICollection<CarEntity> Cars { get; set; }
}
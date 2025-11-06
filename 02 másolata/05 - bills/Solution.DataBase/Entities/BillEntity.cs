namespace Solution.Database.Entities;

[Table("Bill")]
[Index(nameof(BillNumber), IsUnique = true)]
public class BillEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string PublicId { get; set; }

    [Required]
    public string BillNumber { get; set; }

    [Required]
    public DateTime DateIssued { get; set; }

    public virtual ICollection<BillItemEntity> BillItems { get; set; }

}

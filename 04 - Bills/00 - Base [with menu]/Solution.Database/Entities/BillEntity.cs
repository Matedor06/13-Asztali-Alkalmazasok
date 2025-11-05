namespace Solution.Database.Entities;

[Table("Bill")]
[Index(nameof(BillNumber), IsUnique = true)]
public class BillEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [Required]
    public string BillNumber { get; set; }

    [Required]
    public DateTime DateIssued { get; set; }

    [ForeignKey("BillItem")]
    public int BillItemId { get; set; }
    public virtual BillItemEntity BillItem { get; set; }
}

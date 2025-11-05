namespace Solution.Database.Entities;

[Table("BillItem")]
public class BillItemEntity
{   [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }


    [Required]
    public int UnitPrice { get; set; }

    [Required]
    public int Quantity { get; set; }

    public virtual ICollection<BillEntity> Bills { get; set; }

}


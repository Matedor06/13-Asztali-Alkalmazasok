namespace Solution.DataBase;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<BillEntity> Bills { get; set; }

	public DbSet<BillItemEntity> BillItems { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}
}

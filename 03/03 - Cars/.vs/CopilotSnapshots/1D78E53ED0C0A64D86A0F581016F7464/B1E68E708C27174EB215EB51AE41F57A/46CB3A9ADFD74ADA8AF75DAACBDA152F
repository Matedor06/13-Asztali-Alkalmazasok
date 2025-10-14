namespace Solution.DataBase;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<ManufacturerEntity> Manufacturers { get; set; }

	public DbSet<CarEntity> Cars { get; set; }

	public DbSet<CarTypeEntity> Types { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}
}

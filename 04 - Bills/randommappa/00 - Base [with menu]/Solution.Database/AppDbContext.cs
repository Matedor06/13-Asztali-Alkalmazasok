namespace Solution.DataBase;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<BillEntity> Bills { get; set; }
	public DbSet<BillItemEntity> BillItems { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Seed Bills
		modelBuilder.Entity<BillEntity>().HasData(
			new BillEntity
			{
				Id = 1,
				BillNumber = "BILL-2025-001",
				DateIssued = new DateTime(2025, 1, 15)
			},
			new BillEntity
			{
				Id = 2,
				BillNumber = "BILL-2025-002",
				DateIssued = new DateTime(2025, 2, 20)
			},
			new BillEntity
			{
				Id = 3,
				BillNumber = "BILL-2025-003",
				DateIssued = new DateTime(2025, 3, 10)
			}
		);

		// Seed BillItems
		modelBuilder.Entity<BillItemEntity>().HasData(
			// Items for Bill 1
			new BillItemEntity
			{
				Id = 1,
				Name = "Laptop",
				UnitPrice = 150000,
				Quantity = 2,
				BillId = 1
			},
			new BillItemEntity
			{
				Id = 2,
				Name = "Mouse",
				UnitPrice = 5000,
				Quantity = 3,
				BillId = 1
			},
			// Items for Bill 2
			new BillItemEntity
			{
				Id = 3,
				Name = "Monitor",
				UnitPrice = 80000,
				Quantity = 1,
				BillId = 2
			},
			new BillItemEntity
			{
				Id = 4,
				Name = "Keyboard",
				UnitPrice = 15000,
				Quantity = 2,
				BillId = 2
			},
			// Items for Bill 3
			new BillItemEntity
			{
				Id = 5,
				Name = "Headset",
				UnitPrice = 25000,
				Quantity = 1,
				BillId = 3
			},
			new BillItemEntity
			{
				Id = 6,
				Name = "USB Cable",
				UnitPrice = 2000,
				Quantity = 5,
				BillId = 3
			}
		);
	}
}

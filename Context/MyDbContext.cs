using EFCore3.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore3.Context
{
    public class MyDbContext : DbContext
    {
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{ }

        public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
    }

	public class MyDbContextReadOnly : DbContext
	{
		public MyDbContextReadOnly(DbContextOptions<MyDbContextReadOnly> options) : base(options)
		{ }

		public DbSet<ConsultaTodo> ConsultaTodo { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ConsultaTodo>().HasNoKey();
		}
	}
}
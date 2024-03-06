using Microsoft.EntityFrameworkCore;

namespace Practice888.Models
{
    public class MasterDB:DbContext
    {
        public MasterDB() { }

        public MasterDB(DbContextOptions<MasterDB> options):base(options) { }

       public DbSet<SaleMaster> SaleMasters { get; set;}
        public DbSet<Category>  Categories { get; set; }
        public DbSet<SaleDetail>  SaleDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PDBB8;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleMaster>()
                .HasOne(x=>x.Category)
                .WithMany(x=>x.SaleMasters)
                .HasForeignKey(x=>x.CID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CID = 1,
                CatName = "BTV"
            });
        }
    }
}

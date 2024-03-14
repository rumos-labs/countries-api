using Microsoft.EntityFrameworkCore;

namespace WebApplication19;

public class CountriesDbContext : DbContext
{
    public CountriesDbContext(DbContextOptions<CountriesDbContext> options) : base(options) { }
    
    public DbSet<CountryDto> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CountryDto>().HasKey(c => c.CommonName);
    }
}

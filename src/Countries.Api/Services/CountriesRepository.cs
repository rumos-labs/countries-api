using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace WebApplication19.Services
{
    public class CountriesRepository
    {
        private readonly IDbContextFactory<CountriesDbContext> contextFactory;

        public CountriesRepository(IDbContextFactory<CountriesDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            using var context = contextFactory.CreateDbContext();

            return await context.Countries.ToListAsync();
        }

        public async Task RefreshCountriesAsync(IEnumerable<CountryDto> countries)
        {
            using var context = contextFactory.CreateDbContext();

            // DELETE FROM Countries
            await context.Countries.ExecuteDeleteAsync();

            await context.Countries.AddRangeAsync(countries);

            await context.SaveChangesAsync();
        }
    }
}

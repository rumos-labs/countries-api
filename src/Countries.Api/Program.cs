using Microsoft.EntityFrameworkCore;
using WebApplication19;
using WebApplication19.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<RestCountriesService>();
builder.Services.AddSingleton<CountriesRepository>();
builder.Services.AddDbContextFactory<CountriesDbContext>(options =>
{
    options.UseSqlite("Data Source=countries.db");
});
builder.Services.AddHostedService<CountriesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/countries-db", async (CountriesRepository repository) =>
{
    return await repository.GetCountriesAsync();
})
.WithName("GetCountriesFromDb")
.WithOpenApi();

app.MapGet("/countries-live", async (RestCountriesService service) =>
{
    var countries = await service.GetCountriesAsync();
    var result = new List<CountryDto>();

    if (countries is null)
        return result;

    foreach (var country in countries)
    {
        if (country is null)
            continue;

        var countryDto = country.ToCountryDto();

        result.Add(countryDto);
    }

    return result;
})
.WithName("GetCountriesLive")
.WithOpenApi();

app.MapGet("/countries-raw", async (RestCountriesService service) =>
{
    return await service.GetCountriesRawAsync();
})
.WithName("GetCountriesRaw")
.WithOpenApi();

app.Run();

namespace WebApplication19.Services;

public class CountriesService : BackgroundService
{
    private readonly CountriesRepository repository;
    private readonly RestCountriesService service;

    public CountriesService(CountriesRepository repository, RestCountriesService service)
    {
        this.repository = repository;
        this.service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var countries = await service.GetCountriesAsync();
        var dtos = new List<CountryDto>();

        if (countries is null) return;

        foreach (var country in countries)
        {
            if (country is null)
                continue;

            var countryDto = country.ToCountryDto();

            dtos.Add(countryDto);
        }

        await repository.RefreshCountriesAsync(dtos);
    }
}
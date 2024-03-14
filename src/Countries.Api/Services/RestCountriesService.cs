namespace WebApplication19.Services;

public class RestCountriesService
{
    private readonly string endpoint = "https://restcountries.com/v3.1/all";
    private readonly IHttpClientFactory httpClientFactory;

    public RestCountriesService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Country>> GetCountriesAsync()
    {
        using var httpClient = httpClientFactory.CreateClient();

        var response = await httpClient.GetFromJsonAsync<Country[]>(endpoint);

        return response ?? Enumerable.Empty<Country>();
    }

    public async Task<object> GetCountriesRawAsync()
    {
        using var httpClient = httpClientFactory.CreateClient();

        var response = await httpClient.GetFromJsonAsync<object>(endpoint);

        return response;
    }
}

public class Country
{
    public Name Name { get; init; } = new();

    public string? Region { get; init; }

    public string? Subregion { get; init; }

    public Dictionary<string, string> Flags { get; init; } = new();

    public CountryDto ToCountryDto()
    {
        this.Flags.TryGetValue("png", out var flagUrl);

        return new CountryDto
        {
            CommonName = this.Name?.Common ?? string.Empty,
            OfficialName = this.Name?.Official ?? string.Empty,
            Region = this.Region ?? string.Empty,
            Subregion = this.Subregion ?? string.Empty,
            FlagUrl = flagUrl ?? string.Empty
        };
    }
}

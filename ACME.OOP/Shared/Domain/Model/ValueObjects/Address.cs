namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

public record Address
{
    public string Street { get; init; }
    public string Number { get; init; }
    public string City { get; init; }
    public string? StateOrRegion { get; init; }
    public string PostalCode { get; init; }
    public string Country { get; init; }

    public Address(
        string street, 
        string number, 
        string city, 
        string? stateOrRegion, 
        string postalCode, 
        string country)
    {
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException($"'{nameof(street)}' cannot be null or whitespace", nameof(street));
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException($"'{nameof(number)}' cannot be null or whitespace", nameof(number));
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException($"'{nameof(city)}' cannot be null or whitespace", nameof(city));
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException($"'{nameof(postalCode)}' cannot be null or whitespace", nameof(postalCode));
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException($"'{nameof(country)}' cannot be null or whitespace", nameof(country));
        Street = street;
        Number = number;
        City = city;
        StateOrRegion = stateOrRegion;
        PostalCode = postalCode;
        Country = country;
    }
}
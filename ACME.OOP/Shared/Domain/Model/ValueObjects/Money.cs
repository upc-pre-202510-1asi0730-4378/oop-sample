namespace ACME.OOP.Shared.Domain.Model.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) 
            || currency.Length != 3)
            throw new ArgumentException($"'{nameof(currency)}' must be a valid 3-letter code.", nameof(currency));
        Amount = amount;
        Currency = currency;
    }
    
    
}
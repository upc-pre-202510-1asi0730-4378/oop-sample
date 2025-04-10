using ACME.OOP.Procurement.Domain.Model.ValueObjects;
using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.Procurement.Domain.Model.Aggregates;

/// <summary>
/// This class represents a purchase order aggregate root for the purchase order aggregate in the procurement-bounded context.
/// </summary>
/// <param name="orderNumber">The unique identifier for the purchase order. Must not be null or empty.</param>
/// <param name="supplierId">The unique identifier for the supplier. Must not be null.</param>
/// <param name="orderDate">The date the order was placed. Must be a valid date.</param>
/// <param name="currency">The currency in which the order is placed. Must be a valid 3-letter currency code.</param>
public class PurchaseOrder(string orderNumber, SupplierId supplierId, DateTime orderDate, string currency)
{
    private readonly List<PurchaseOrderItem> _items = new();
    public string OrderNumber { get; } = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
    public SupplierId SupplierId { get; } = supplierId ?? throw new ArgumentNullException(nameof(supplierId));
    public DateTime OrderDate { get; } = orderDate;
    public string Currency { get; } = string.IsNullOrWhiteSpace(currency) || currency.Length != 3
        ? throw new ArgumentException("Currency must be a valid 3-letter code.", nameof(currency)) : currency;
    
    public IReadOnlyList<PurchaseOrderItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Adds an item to the purchase order.
    /// </summary>
    /// <param name="productId">The unique identifier for the product. Must not be null.</param>
    /// <param name="quantity">The quantity of the product. Must be greater than zero.</param>
    /// <param name="unitPriceAmount">The unit price of the product. Must be non-negative.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when quantity is less than or equal to zero or unit price is negative.</exception>
    public void AddItem(ProductId productId, int quantity, decimal unitPriceAmount)
    {
        ArgumentNullException.ThrowIfNull(productId);
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (unitPriceAmount < 0) throw new ArgumentOutOfRangeException(nameof(unitPriceAmount), "UnitPrice cannot be negative.");
        
        var unitPrice = new Money(unitPriceAmount, Currency);
        var item = new PurchaseOrderItem(productId, quantity, unitPrice);
        _items.Add(item);
    }

    /// <summary>
    /// Calculates the total amount of the purchase order
    /// </summary>
    /// <returns>A <see cref="Money"/> object representing the total amount of the purchase order.</returns>
    public Money CalculateTotal()
    {
        var total = _items.Sum(item => item.CalculateSubtotal().Amount);
        return new Money(total, Currency);
    }
}
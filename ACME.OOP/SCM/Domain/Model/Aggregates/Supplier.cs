using ACME.OOP.SCM.Domain.Model.ValueObjects;
using ACME.OOP.Shared.Domain.Model.ValueObjects;

namespace ACME.OOP.SCM.Domain.Model.Aggregates;

public class Supplier
{
    public SupplierId SupplierId { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }

    public Supplier(string identifier, string name, Address address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        SupplierId = new SupplierId(identifier);
        Name = name;
        Address = address;
    }
}
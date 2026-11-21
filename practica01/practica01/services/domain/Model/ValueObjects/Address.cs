namespace practica01.Services.Domain.Model.ValueObjects;

/// <summary>Represents an owned rental address.</summary>
public class Address
{
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;
    private Address() { }

    /// <summary>Creates an address with components of at most 40 characters.</summary>
    public Address(string street, string city, string postalCode)
    {
        Street = Validate(street, nameof(Street));
        City = Validate(city, nameof(City));
        PostalCode = Validate(postalCode, nameof(PostalCode));
    }

    private static string Validate(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Trim().Length > 40)
            throw new ArgumentException($"{name} is required and must not exceed 40 characters.");
        return value.Trim();
    }
}

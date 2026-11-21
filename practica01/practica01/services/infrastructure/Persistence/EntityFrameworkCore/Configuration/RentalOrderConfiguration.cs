using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using practica01.Services.Domain.Model.Aggregates;
namespace practica01.Services.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>Maps rental orders, their owned address and uniqueness constraint.</summary>
public class RentalOrderConfiguration : IEntityTypeConfiguration<RentalOrder>
{
    public void Configure(EntityTypeBuilder<RentalOrder> builder)
    {
        builder.ToTable("rental_orders");
        builder.HasKey(order => order.RentalOrderId);
        builder.Property(order => order.RentalOrderId).ValueGeneratedOnAdd();
        builder.Property(order => order.Customer).HasMaxLength(90).IsRequired();
        builder.Property(order => order.Plate).HasMaxLength(12).IsRequired();
        builder.Property(order => order.VehiclesId).HasConversion<int>().IsRequired();
        builder.Property(order => order.RequestedAt).IsRequired();
        builder.Property(order => order.Amount).IsRequired();
        builder.Ignore(order => order.DomainEvents);
        builder.HasIndex(order => new { order.Customer, order.VehiclesId }).IsUnique();
        builder.HasIndex(order => order.Plate);
        builder.OwnsOne(order => order.Address, address =>
        {
            address.Property(value => value.Street).HasColumnName("address_street").HasMaxLength(40).IsRequired();
            address.Property(value => value.City).HasColumnName("address_city").HasMaxLength(40).IsRequired();
            address.Property(value => value.PostalCode).HasColumnName("address_postal_code").HasMaxLength(40).IsRequired();
        });
        builder.Navigation(order => order.Address).IsRequired();
    }
}

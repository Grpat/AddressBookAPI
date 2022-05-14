using AddressBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Contact>(b =>
        {
            b.HasData(new Contact
            {
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Id = 1,
                FirstName = "Patryk",
                LastName = "Gruszczyk",
                PhoneNumber = "690563138",
                
            });
            b.OwnsOne(e => e.Address).HasData(new
            {
                ContactId=1,
                Country="Poland",
                City="Jaworze",
                Street="Cisowa",
                ZipCode="43-384"
            });
        });
 
    }
    public DbSet<Contact> Contacts { get; set; }
}
    

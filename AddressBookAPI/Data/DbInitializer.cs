using AddressBookAPI.Models;

namespace AddressBookAPI.Data;

public class DbInitializer:IDbInitializer
{
    private readonly DataContext _context;

    public DbInitializer(DataContext context)
    {
        _context = context;
    }
    public void Initialize()
    {
        var contact = new Contact
        {
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Id = 1,
            FirstName = "Patryk",
            LastName = "Gruszczyk",
            PhoneNumber = "690563138",
            Address = new Address
            {
                Street ="Cisowa",
                City = "Jaworze",
                Country = "Poland",
                ZipCode = "43-384"
            }
        };
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }
}
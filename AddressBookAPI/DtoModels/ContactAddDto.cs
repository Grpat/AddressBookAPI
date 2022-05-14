using System.ComponentModel.DataAnnotations;

namespace AddressBookAPI.DtoModels;

public class ContactAddDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public AddressDto Address { get; set; }
}
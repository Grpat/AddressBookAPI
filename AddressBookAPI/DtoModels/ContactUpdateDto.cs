using System.ComponentModel.DataAnnotations;
using AddressBookAPI.Models;

namespace AddressBookAPI.DtoModels;

public class ContactUpdateDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public AddressDto Address { get; set; }
    
}
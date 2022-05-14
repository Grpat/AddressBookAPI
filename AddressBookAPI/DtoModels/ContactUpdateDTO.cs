using System.ComponentModel.DataAnnotations;
using AddressBookAPI.Models;

namespace AddressBookAPI.DtoModels;

public class ContactUpdateDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public AddressDTO Address { get; set; }
    
}
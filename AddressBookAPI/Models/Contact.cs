using System.ComponentModel.DataAnnotations;

namespace AddressBookAPI.Models;

public class Contact:BaseEntity
{
    
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    public Address Address { get; set; }
    
  
    
}
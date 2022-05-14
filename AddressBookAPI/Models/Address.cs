using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAPI.Models;
[Owned]
public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    [DataType(DataType.PostalCode)]
    public string? ZipCode { get; set; }
}
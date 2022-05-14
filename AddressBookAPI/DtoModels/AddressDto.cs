using System.ComponentModel.DataAnnotations;

namespace AddressBookAPI.DtoModels;

public class AddressDto
{
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    [DataType(DataType.PostalCode)]
    public string? ZipCode { get; set; }
}
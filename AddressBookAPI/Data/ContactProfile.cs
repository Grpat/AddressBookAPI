using AddressBookAPI.DtoModels;
using AddressBookAPI.Models;
using AutoMapper;

namespace AddressBookAPI.Data;

public class ContactProfile:Profile
{
    public ContactProfile()
    {
        CreateMap<ContactUpdateDTO, Contact>();
        CreateMap<ContactUpdateDTO,Address >();
        CreateMap<Address,ContactUpdateDTO >();
        CreateMap<Contact,ContactUpdateDTO>();
        CreateMap<Address,AddressDTO >();
        CreateMap<AddressDTO, Address>();
    }
}
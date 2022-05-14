using AddressBookAPI.DtoModels;
using AddressBookAPI.Models;
using AutoMapper;

namespace AddressBookAPI.Data;

public class ContactProfile:Profile
{
    public ContactProfile()
    {
        CreateMap<ContactUpdateDto, Contact>();
        CreateMap<ContactUpdateDto,Address >();
        CreateMap<Address,ContactUpdateDto >();
        CreateMap<Contact,ContactUpdateDto>();
        
        
        CreateMap<Address,AddressDto >();
        CreateMap<AddressDto, Address>();
        
        
        CreateMap<ContactAddDto, Contact>();
        CreateMap<ContactAddDto,Address >();
        CreateMap<Address,ContactAddDto >();
        CreateMap<Contact,ContactAddDto>();
    }
}
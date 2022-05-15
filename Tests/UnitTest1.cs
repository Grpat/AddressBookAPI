using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBookAPI.Data;
using AddressBookAPI.DtoModels;
using AddressBookAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Tests;

public class UnitTest1:IDisposable
{
    private readonly DataContext _context;
    
    public UnitTest1()
    {
        var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new DataContext(options);
        _context.Database.EnsureCreated();
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
        _context.Contacts.AddAsync(contact);
        _context.SaveChangesAsync();
    }
    [Fact]
    public async Task GetContacts_ReturnsCorrectType()
    {
       

        var controller = new ContactController(_context,null);
        var response = await controller.GetContacts();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        Assert.IsAssignableFrom<IEnumerable<Contact>>(objectResult.Value);
    }
    [Fact]
    public async Task GetContact_ReturnsContact_ValidID()
    {
        var controller =new ContactController(_context,null);
        var response = await controller.GetContact(1);
        
        Assert.IsType<OkObjectResult>(response.Result);
    }
    
    [Fact]
    public async Task GetContact_ReturnsContact_InvalidID()
    {
        var controller =new ContactController(_context,null);
        var response = await controller.GetContact(5);
        Assert.IsType<NotFoundResult>(response.Result);
        
    }
    
    [Fact]
    public async Task AddContact_ReturnsBadRequest_WhenPhoneNumberIsAlreadyRegistered()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ContactProfile>());
        var mapper = config.CreateMapper();
        var controller =new ContactController(_context,mapper);
        var contactAddDto = new ContactAddDto
        {
            FirstName ="P",
            LastName = "G",
            PhoneNumber = "690563138",
            Address = new AddressDto
            {
                Street = "Cisowa",
                City = "75",
                Country = "Poland",
                ZipCode = "43-384"
            }
        };
        var response = await controller.AddContact(contactAddDto);
        
            
        Assert.IsType<BadRequestObjectResult>(response.Result);
        
    }
    
    public void Dispose()
    {
        _context.Database.EnsureCreated();
        _context.Dispose();
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBookAPI.Data;
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
    public async void GetContacts_ReturnsCorrectType()
    {
        var controller = new ContactController(_context,null);
        var result = await controller.GetContacts();

        var objectResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsAssignableFrom<IEnumerable<Contact>>(objectResult.Value);
    }
    [Fact]
    public async void GetContact_ReturnsContact_ValidID()
    {
        var controller =new ContactController(_context,null);
        var result = await controller.GetContact(1);
        
        Assert.IsType<OkObjectResult>(result.Result);
    }
    
    [Fact]
    public async void GetContact_ReturnsContact_InvalidID()
    {
        var controller =new ContactController(_context,null);
        var result = await controller.GetContact(5);
        Assert.IsType<NotFoundResult>(result.Result);
        
    }

    public void Dispose()
    {
        _context.Database.EnsureCreated();
        _context.Dispose();
    }
}
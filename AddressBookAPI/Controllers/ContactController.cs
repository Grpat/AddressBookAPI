using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBookAPI.DtoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddressBookAPI.Models;
using AutoMapper;

namespace AddressBookAPI.Data
{
   
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        

        // GET: api/Contact
        [HttpGet]
        [Route("api/GetAllContacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
          /*if (await _context.Contacts.CountAsync() == 0) return NoContent();*/
          
          return Ok(await _context.Contacts.ToListAsync());
         
        }

        // GET: api/Contact/5
        [HttpGet]
        [Route("api/GetContactById/{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null) return NotFound();
            
            return contact;
        }
        
        //GET: api/LastAddedContact 
        [HttpGet]
        [Route("GetLastAddedContact")]
        public async Task<ActionResult<Contact>> GetLastAddedContact()
        {
            var contact = await _context.Contacts.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();

            if (contact == null) return NotFound();
            
            return contact;
        }
        
        //GET: api/Contact/city
        [HttpGet]
        [Route("GetContactsByCity/{city}")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContactsByCity(string city)
        {
            var contact =  _context.Contacts.Where(x => x.Address.City == city);

            if (await contact.CountAsync()==0) return NoContent();
            
            return await contact.ToListAsync();
        }

        // PUT: api/Contact/5
        
        [HttpPut]
        [Route("api/EditContactById/{id}")]
        public async Task<IActionResult> EditContactById(int id, ContactUpdateDto contactUpdateDto)
        {
            if (id !=  contactUpdateDto.Id) return BadRequest();
            
            var contact = _mapper.Map<Contact>( contactUpdateDto);
            contact.UpdatedDate=DateTime.Now;
            _context.Entry(contact).State = EntityState.Modified;
            _context.Entry(contact).Property(x => x.CreatedDate).IsModified = false;
            
            if(IsPhoneRegisteredExceptUpdatedUser(contactUpdateDto.PhoneNumber,id))
                return BadRequest("Phone is Already Registered to Another User");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ContactExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        
       

        // POST: api/Contact
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api/AddContact")]
        public async Task<ActionResult<Contact>> AddContact(ContactAddDto contactAddDto)
        {
            if (IsPhoneAlreadyRegistered(contactAddDto.PhoneNumber)) return BadRequest("Phone is Already Registered");
            var contact = _mapper.Map<Contact>(contactAddDto);
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AddContact", new { id = contact.Id }, contactAddDto);
        }

        // DELETE: api/Contact/5
        [HttpDelete]
        [Route("api/DeleteContactById/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
           
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool IsPhoneAlreadyRegistered(string phoneNumber)
        {
            return (_context.Contacts?.Any(x => x.PhoneNumber == phoneNumber)).GetValueOrDefault();
        }
        
        private bool IsPhoneRegisteredExceptUpdatedUser(string phoneNumber,int id)
        {
            return (_context.Contacts?.Where(a=>a.Id!=id).Any(x => x.PhoneNumber == phoneNumber)).GetValueOrDefault();
        }
        
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Entities.Models;
using CodeChallenge.Biz.Contract.Interface;
using CodeChallenge.Biz.Service;

namespace CodeChallenge.Controllers
{
    [Produces("application/json")]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {
        private IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Contact> GetContact()
        {
            return this._contactService.GetAllContacts();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _contactService.GetContactAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public IActionResult PutContact([FromRoute] int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            try
            {
                this._contactService.SaveOrUpdate(contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this._contactService.ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public IActionResult PostContact([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this._contactService.SaveOrUpdate(contact);

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _contactService.GetContactAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            this._contactService.Delete(id);

            return Ok(contact);
        }

        [HttpGet("Search/{query}")]
        public IEnumerable<Contact> SearchContact([FromRoute] string query)
        {
            return this._contactService.SearchContactByPhoneOrEmail(query);            
        }

        [HttpGet("SearchByCode/{code}")]
        public IEnumerable<Contact> SearchContactByCode([FromRoute] string code)
        {
            return this._contactService.SearchContactByCityCode(code);
        }
    }
}
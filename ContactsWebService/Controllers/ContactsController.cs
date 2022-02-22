using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using ContactsLibrary;
using Csla;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        [HttpGet]
        public ContactList GetContacts()
        {
            return DataPortal.Fetch<ContactList>();
        }


        [HttpGet("{id}")]
        public ContactEdit Get(int id)
        {
            return DataPortal.Fetch<ContactEdit>(id);
        }


        // POST api/<ContactsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

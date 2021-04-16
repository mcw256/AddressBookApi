using AddressBookApi.Models;
using AddressBookApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepo _addressRepo;

        public AddressController(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _addressRepo.GetLastAddress());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<AddressController>/street
        [HttpGet("{street}")]
        public async Task<IActionResult> Get(string street)
        {
            try
            {
                return Ok(await _addressRepo.GetAddressesByStreet(street));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Address address)
        {
            try
            {
                await _addressRepo.AddNewAddress(address);
                return CreatedAtAction(nameof(Get), address);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<AddressController>/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Address address)
        {
            try
            {
                return Ok(await _addressRepo.UpdateAddressById(id, address));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<AddressController>/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _addressRepo.DeleteAddressById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

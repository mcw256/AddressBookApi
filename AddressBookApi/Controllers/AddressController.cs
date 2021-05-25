using AddressBookApi.Models;
using AddressBookApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 0, [FromQuery] string city = null, [FromQuery] string street = null)
        {
            try
            {
                return Ok(await _addressRepo.GetAddresses(page, city, street));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return Ok(await _addressRepo.GetAddressById(id));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }


        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] Address address)
        {
            try
            {
                await _addressRepo.AddNewAddress(address);
                return Ok();
            }
            
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Address address)
        {
            try
            {
                await _addressRepo.UpdateAddressById(id, address);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _addressRepo.DeleteAddressById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }


    }
}

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
                // TODO
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                // TODO
            }
            catch (Exception e)
            {
                // TODO
            }
        }


        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] Address address)
        {
            try
            {
                // TODO
            }
            catch (Exception e)
            {
                // TODO
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                // TODO
            }
            catch (Exception e)
            {
                // TODO
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // TODO
            }
            catch (Exception e)
            {
                // TODO
            }
        }


    }
}

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

        /// <summary>
        /// Gets last address
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetLast()
        {
            try
            {
                return Ok(await _addressRepo.GetLastAddress());
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }

        /// <summary>
        /// Gets list of all addresses
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Address>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _addressRepo.GetAllAddresses());
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }


        /// <summary>
        /// Gets address by Id (I'm aware this endpoint is rather ugly but you wanted to have straight up /{city} endpoint and I had to solve it somehow)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")] // Ok, I know it isn't great endpoint for id, but the /{city} endpoint collides with it (and as I understood the city needs stays in its from), so i cannot make simple /{id}
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetById(int id)
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

        /// <summary>
        /// Gets addresses by city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Address>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetByCity(string city)
        {
            try
            {
                return Ok(await _addressRepo.GetAddressesByCity(city));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }

        /// <summary>
        /// Adds new address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Add([FromBody] Address address)
        {
            try
            {
                await _addressRepo.AddNewAddress(address);
                return CreatedAtAction(nameof(GetById), new { id = address.Id }, address);
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }

        /// <summary>
        /// Updates existing address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Update(int id, [FromBody] Address address)
        {
            try
            {
                return Ok(await _addressRepo.UpdateAddressById(id, address));
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }

        /// <summary>
        /// Deletes address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Delete(int id)
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

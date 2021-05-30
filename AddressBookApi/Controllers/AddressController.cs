using AddressBookApi.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBookApi.Queries;
using AddressBookApi.Requests;
using AddressBookApi.Commands;
using AddressBookApi.DAL.Repositories;

namespace AddressBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddressesPage([FromQuery] GetAddressesPageRequest getAddressesPageRequest)
        {
            try
            {
                // request data validation should be here
                // ...
                // maping should be done differently
                var query = new GetAddressesPageQuery(getAddressesPageRequest.PageNo, getAddressesPageRequest.City, getAddressesPageRequest.Street);
                var result = await _mediator.Send(query);
                return Ok(result);
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
                // id validation here
                // ...

                var query = new GetAddressByIdQuery(id);
                var result = await _mediator.Send(query);
                return (result != null) ? (IActionResult)Ok(result) : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddAddressRequest addAddressRequest)
        {
            try
            {
                //request validation should be here
                // ...

                var command = new AddAddressCommand(addAddressRequest.Name, addAddressRequest.City, addAddressRequest.Street);
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(this.GetById), new { id = result.Id });
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAddressRequest updateAddressRequest)
        {
            try
            {
                //request validation should be here
                // ...

                var command = new UpdateAddressCommand(id, updateAddressRequest.Name, updateAddressRequest.City, updateAddressRequest.Street);
                await _mediator.Send(command);
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
                // id validation should be here
                // ...
                
                var command = new DeleteAddressCommand(id);
                var result = await _mediator.Send(command);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse() { ShortInfo = e.Message, AdditionalInfo = e.StackTrace });
            }
        }


    }
}

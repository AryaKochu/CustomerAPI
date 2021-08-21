using CustomerAPI.Controllers.Base;
using CustomerAPI.DB;
using CustomerAPI.Models;
using CustomerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : BaseController
    {
        private ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet("/")]
        [SwaggerOperation("Get all saved customer details")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IList<Customer>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomers()
        {
            var correlationId = Guid.NewGuid().ToString();
            return Ok(await _service.GetCustomers(correlationId));
        }

        [HttpPost("add")]
        [SwaggerOperation("Add a new customer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IList<Customer>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerRequest customer)
        {
            var correlationId = Guid.NewGuid().ToString();
            if (!ModelState.IsValid)
            {
                return BadRequest((CommonError)ModelState);
            }
            return Ok(await _service.AddCustomer(customer, correlationId));
        }


        [HttpPost("update")]
        [SwaggerOperation("Update an existing customer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest customer)
        {
            var correlationId = Guid.NewGuid().ToString();
            if (!ModelState.IsValid)
            {
                return BadRequest((CommonError)ModelState);
            }
            return Ok(_service.UpdateCustomer(customer, correlationId));
        }

        [HttpGet("search")]
        [SwaggerOperation("Search for a customer with first name or last name")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        //200
        [ProducesResponseType(typeof(IList<Customer>), (int)HttpStatusCode.OK)]
        //400
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> SearchCustomers(string name)
        {
            var correlationId = Guid.NewGuid().ToString();
            return Ok(await _service.SearchCustomer(name, correlationId));
        }
    }
}

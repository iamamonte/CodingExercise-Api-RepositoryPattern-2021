
using GroceryStore.Domain.Core;
using GroceryStore.Domain.Interface.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIContracts = GroceryStore.API.V1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ApiControllerBase
    {
        private readonly IGroceryStoreManager _groceryStoreManager;
        // TODO: Configure logging
        public CustomersController(IGroceryStoreManager groceryStoreManager, ILogger<APIContracts.Customer> logger)
        {
            _groceryStoreManager = groceryStoreManager;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<APIContracts.Customer>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {

            var response = await _groceryStoreManager.FindCustomers();
            return Ok(response.Result?
                .Select(
                    customer => new APIContracts.Customer { Id = customer.Id, Name = customer.Name }));
            
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(APIContracts.Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIContracts.Customer), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _groceryStoreManager.FindCustomers(x => x.Id == id);
            if (!response.Succeeded)
            {
                return ErrorResult;
            }
            
            if (!response.Result.Any())
            {
                return NotFound();
            }

            var customer = response.Result.Single();

            return Ok(new APIContracts.Customer { Id = customer.Id, Name = customer.Name });
        }

        // POST api/<CustomersController>
        [HttpPost]
        [ProducesResponseType(typeof(APIContracts.Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] APIContracts.Customer customer)
        {
            if (customer == null)
            {
                return BadRequest(new { @ValidationError = "Customer must not be null." });
            }
            var newCustomer = new Customer { Name = customer.Name };
            var response = await _groceryStoreManager.CreateOrUpdateCustomers(new Customer[] { newCustomer });
            if (!response.Succeeded)
            {
                return BadRequest(response.ErrorMessages);
            }
            var createdCustomer = response.Result.Single();
            return Ok(new APIContracts.Customer { Name = createdCustomer.Name, Id = createdCustomer.Id });
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(APIContracts.Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] APIContracts.Customer customer)
        {
            var newCustomer = new Customer { Name = customer.Name, Id = id };
            var response = await _groceryStoreManager.CreateOrUpdateCustomers(new Customer[] { newCustomer });
            if (!response.Result.Any()) 
            {
                return NotFound();
            }
            var result = response.Result;
            var affectedCustomer = result.Single();
            return Ok(new APIContracts.Customer { Name = affectedCustomer.Name, Id = affectedCustomer.Id });
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _groceryStoreManager.DeleteCustomers(new Customer[] { new Customer { Id = id } });
            return Ok();
        }

        [Route("[action]")]
        public IActionResult Throw() 
        {
            throw new NotImplementedException("This is on purpose.");
        }
    }
}


using GroceryStore.DAL;
using GroceryStore.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using APIContracts = GroceryStore.API;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        public CustomersController(IRepository<Customer> customerRepository) 
        {
            _customerRepository = customerRepository;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<APIContracts.Customer>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {

            var customers = _customerRepository.List().Select(customer => new APIContracts.Customer { Id = customer.Id, Name = customer.Name });
            return Ok(customers);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(APIContracts.Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIContracts.Customer), StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var customer = _customerRepository.FindById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(new APIContracts.Customer { Id = customer.Id, Name = customer.Name });
        }

        // POST api/<CustomersController>
        [HttpPost]
        [ProducesResponseType(typeof(APIContracts.Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] APIContracts.Customer customer)
        {
            if (customer == null)
            {
                return BadRequest(new { @ValidationError = "Customer must not be null." });
            }
            var newCustomer = new Customer { Name = customer.Name };
            _customerRepository.Add(newCustomer);
            return Ok(new APIContracts.Customer { Name = newCustomer.Name, Id = newCustomer.Id });
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(APIContracts.Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] APIContracts.Customer customer)
        {
            var existingCustomer = _customerRepository.FindById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.Name = customer.Name;
            _customerRepository.Update(existingCustomer);
            return Ok(new APIContracts.Customer { Name = existingCustomer.Name, Id = existingCustomer.Id });
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.FindById(id);
            if (customer != null) 
            {
                _customerRepository.Delete(customer);
            }
            
            return Ok();
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/customers
        public IHttpActionResult GetCustomers(string query=null)
        {

            var customerQuery = _context.Customers
                .Include(c => c.MembershipType);

            if(!String.IsNullOrEmpty(query))
            {
                customerQuery = customerQuery.Where(c => c.Name.Contains(query));
            }
            var customerDto = customerQuery.ToList()
                .Select(Mapper.Map<Customer, CustomerDto>)
                .ToList();

            return Ok(customerDto);


        }

        // GET /api/customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCutomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto,Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri+"/"+customer.Id),customerDto) ;
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerDb==null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                Mapper.Map<CustomerDto, Customer>(customerDto, customerDb);
                _context.SaveChanges();
            }
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                _context.Customers.Remove(customerDb);
                _context.SaveChanges();
            }
        }

    }
}

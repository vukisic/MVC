using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    //Customer API
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            //DataBase connection
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/Customers
        public IHttpActionResult GetCustomers(string query=null)
        {
            //Get Customers form DB , select all customers with query in customer.name!
            var customersQuery = _context.Customers.Include(c => c.MembershipType);
            if(!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            }
            var customerDtos = customersQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        //GET /api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            //Get single customer with specific ID
            var customer=_context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            else
                return Ok(Mapper.Map<Customer,CustomerDto>(customer)); 
        }

        //POST /api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            //Create Customer

            //Validation
            if (!ModelState.IsValid)
                return BadRequest();

            //Mappin, mainly usin Dtos
            var customer = Mapper.Map<CustomerDto,Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri+"/"+customer.Id),customerDto);
        }

        //PUT /api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id,CustomerDto customerDto)
        {
            //Update existing customer

            //Validation
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
                return NotFound();

            //Map and populate customerinDB
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDB);

            _context.SaveChanges();
            return Ok();
        }

        //DELETE /api/Customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            //Delete customer
            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
                NotFound();

            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();
            return Ok();
        }


    }
}

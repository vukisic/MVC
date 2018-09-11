using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{   
    //Rental API
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;
        public NewRentalsController()
        {
            //DataBase Connection
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalsDto newRentalsDto)
        {
            //GET Customer by Id
            var customer = _context.Customers.Single(c => c.Id == newRentalsDto.CustomerId);

            //Get List of Movies by MovieIds
            var movies = _context.Movies.Where(m => newRentalsDto.MovieIds.Contains(m.Id)).ToList();

            //Loop
            foreach (var item in movies)
            {
                if (item.NumberAvailable == 0)
                {
                    return BadRequest("Movie is Not Available!");
                }

                item.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = item,
                    DateRented = DateTime.Now
                };
                //Add to DataBase
                _context.Rentals.Add(rental);
            }

            //Save changes to DataBase
            _context.SaveChanges();
            return Ok();


        }
    }
}

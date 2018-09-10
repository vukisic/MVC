using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly_MVC.Models;
using Vidly_MVC.ViewModels;

namespace Vidly_MVC.Controllers
{
    public class MoviesController : Controller
    {
        public ViewResult Index()
        {
            var movies = GetMovies();
            return View(movies);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie{Id=1,Name="Shrek"},
                new Movie{Id=2,Name="Wall-e"}
            };
        }
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Shrek" };
            var customers = new List<Customer>
            {
                new Customer{Name="Customer 1"},
                new Customer{Name="Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                ListOfCustomers = customers
            };

            return View(viewModel);
            //return RedirectToAction("Index", "Home", new { page = 1, SortBy = "name" });
        }

        
    }
}
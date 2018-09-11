using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [Authorize(Roles =RoleName.CanManage)]
        public ViewResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManage)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManage)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()

                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id==0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.NumberInStock = movie.NumberInStock;
                movieInDB.GenreId = movie.GenreId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManage))
                return View("List");
            else
                return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManage)]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        // GET: Movies
        [Authorize(Roles = RoleName.CanManage)]
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
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
    //Movie API
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            //DataBase connection
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/Movies
        public IHttpActionResult GetMovies(string query=null)
        {
            //Get List of Movies with query in movie.Name
            var moviesQuery = _context.Movies.Include(c => c.Genre).Where(m => m.NumberAvailable>0);
            if (!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(c => c.Name.Contains(query));
            }
            var movieDtos = moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieDtos);
        }

        //GET /api/Movies/1
        [Authorize(Roles = RoleName.CanManage)]
        public IHttpActionResult GetMovie(int id)
        {
            //Get single movie with specific ID
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound();
            else
                return Ok(Mapper.Map<Movie,MovieDto>(movie));
        }

        //POST /api/Movies
        [Authorize(Roles = RoleName.CanManage)]
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            //Create Movie

            //Validation
            if (!ModelState.IsValid)
                return BadRequest();

            //Mapping , mainly using Dtos
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri+"/"+movie.Id),movieDto);
        }

        //PUT /api/Movies/1
        [Authorize(Roles = RoleName.CanManage)]
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            //Update specific Movie

            //Validation
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDB == null)
                return NotFound();

            //Mapping
            Mapper.Map<MovieDto, Movie>(movieDto, movieInDB);

            _context.SaveChanges();
            return Ok();
        }

        //DELETE /api/Movies/1
        [Authorize(Roles = RoleName.CanManage)]
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            //Delete single movie with specific ID
            var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDB == null)
                return NotFound();

            _context.Movies.Remove(movieInDB);
            _context.SaveChanges();
            return Ok();
        }
    }
}

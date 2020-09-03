using Microsoft.AspNetCore.Mvc;
using ActorMovieApplication.DAO;
using ActorMovieApplication.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
// handle create and read functionality of movies and actors
namespace ActorMovieApplication.Controllers
{
    public class ActorMovie : Controller
    {
        // ref to database
        private readonly ActorDbContext _context;
        public ActorMovie(ActorDbContext context)
        {
            _context = context;
        }
        // add actor to database
        [HttpPost]
        public IActionResult AddActor([Bind("actorName")] ActorModel newActor)
        {
            _context.actors.Add(newActor);
            _context.SaveChanges();
            return Content("Successful");
        }
        // add movie to database
        [HttpPost]
        public IActionResult AddMovie([Bind("title", "desc", "rating")] MovieModel newMovie, int actorID)
        {
            // find actor by id
            ActorModel matchingActor = _context.actors.Include(actor => actor.movies).FirstOrDefault(actor => actor.id == actorID);
            matchingActor.movies.Add(newMovie);
            _context.movies.Add(newMovie);
            _context.SaveChanges();
            // retrun view one
            return View("ViewOne", matchingActor);
        }
        // view actor by ID
        [HttpGet]
        public IActionResult ViewOne(int actorID)
        {
            ActorModel matchingActor = _context.actors.Include(actor => actor.movies).FirstOrDefault(actor => actor.id == actorID);
            // pass found actor to default view
            return View(matchingActor);
        }
    }
}
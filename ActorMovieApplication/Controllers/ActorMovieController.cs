using Microsoft.AspNetCore.Mvc;
using ActorMovieApplication.DAO;
using ActorMovieApplication.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            // if data passed in body of request is valid
            if(ModelState.IsValid)
            {
                // add actor to actor db set and save changes
                _context.actors.Add(newActor);
                _context.SaveChanges();
                return View("ViewAll", _context);
            } else 
            // if data passed in body of request is not valid
            {
                // build string to display error from model state query method and pass as view data
                string displayError = "";
                List<string> errors = GetErrorListFromModelState(ModelState);
                errors.ForEach(error => displayError += error);
                ViewData["displayError"] = displayError;
                return View("Error");
            }
        }
        // add movie to database
        [HttpPost]
        public IActionResult AddMovie([Bind("title", "desc", "rating")] MovieModel newMovie, int actorID)
        {
            // find actor by id
            ActorModel matchingActor = _context.actors.Include(actor => actor.movies).FirstOrDefault(actor => actor.id == actorID);
            // if matching actor is found
            if(matchingActor != null)
            {
                if(ModelState.IsValid)
                {
                matchingActor.movies.Add(newMovie);
                _context.movies.Add(newMovie);
                _context.SaveChanges();
                // retrun view one
                return View("ViewOne", matchingActor);
                } else 
                // if data passed in body of request is not valid
                {
                // build string to display error from model state query method and pass as view data
                    string displayError = "";
                    List<string> errors = GetErrorListFromModelState(ModelState);
                    errors.ForEach(error => displayError += error);
                    ViewData["displayError"] = displayError;
                    return View("Error");
                }
            } else
            // if matching actor is not found 
            {
                // send message as view data
                ViewData["displayError"] = "No Actor Found";
                return View("Error");
            }

        }
        // view actor by ID
        [HttpGet]
        public IActionResult ViewOne(int actorID)
        {
            ActorModel matchingActor = _context.actors.Include(actor => actor.movies).FirstOrDefault(actor => actor.id == actorID);
            // if matching actor is found
            if(matchingActor != null)
            {
                // pass found actor to default view
                return View(matchingActor);
            } else
            // if matching actor is not found
            {
                // send message as view data
                ViewData["displayError"] = "No Actor Found";
                return View("Error");                
            }
            
        }
        // view all actors
        [HttpGet]
        public IActionResult ViewAll(int actorID)
        {
            // pass actors to default view
            return View(_context);
        }        
        // method to capture model state validation errors
        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            IEnumerable<string> query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            List<string> errorList = query.ToList();
            return errorList;
        }
    }
}
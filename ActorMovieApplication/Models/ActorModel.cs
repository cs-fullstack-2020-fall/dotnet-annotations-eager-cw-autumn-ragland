using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// database model
namespace ActorMovieApplication.Models
{
    public class ActorModel 
    {
        [Key]
        public int id{get;set;}
        [Required]
        public string actorName{get;set;}
        public List<MovieModel> movies{get;set;}
    }
}
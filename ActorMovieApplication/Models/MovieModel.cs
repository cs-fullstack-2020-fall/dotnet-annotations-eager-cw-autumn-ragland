using System.ComponentModel.DataAnnotations;
// database model linked ot actors
namespace ActorMovieApplication.Models
{
    public class MovieModel
    {
        [Key]
        public int id{get;set;}
        [Required]
        public string title{get;set;}
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string desc{get;set;}
        [Range(1,5)]
        public int rating{get;set;}
    }
}
using Microsoft.EntityFrameworkCore;
using ActorMovieApplication.Models;
namespace ActorMovieApplication.DAO
{
    public class ActorDbContext : DbContext
    {
        public ActorDbContext(DbContextOptions<ActorDbContext> options) : base(options)
        {

        }
        public DbSet<ActorModel> actors {get;set;}
        public DbSet<MovieModel> movies {get;set;}
    }
}
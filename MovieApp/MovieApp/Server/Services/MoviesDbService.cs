using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Data;
using MovieApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Server.Services
{
    public interface IMoviesRepository
    {

    }

    public interface IMoviesDbService
    {
        Task<List<Movie>> GetMovies();
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> GetMovie(int movieId);

        Task<Movie> UpdateMovie(int movieId, Movie movie);

        Task<Movie> DeleteMovie(int movieId);
    }

    public class MoviesDbService : IMoviesDbService
    {
        private ApplicationDbContext _context;

        public MoviesDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            return movie;
        }

        public Task<List<Movie>> GetMovies()
        {
            return _context.Movies.OrderBy(m => m.Title).ToListAsync();
        }

        public async Task<Movie> UpdateMovie(int movieId, Movie movie)
        {
            var movieToUpdate = await _context.Movies.FindAsync(movieId);
            movieToUpdate.Title = movie.Title;
            movieToUpdate.Summary = movie.Summary;
            movieToUpdate.InTheaters = movie.InTheaters;
            movieToUpdate.Trailer = movie.Trailer;
            movieToUpdate.ReleaseDate = movie.ReleaseDate;
            movieToUpdate.Poster = movie.Poster;

            _context.Update(movieToUpdate);
            await _context.SaveChangesAsync();
            return movieToUpdate;
        }

        public async Task<Movie> DeleteMovie(int movieId)
        {
            var movieToDelete = await _context.Movies.FindAsync(movieId);
            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();
            return movieToDelete;
        }

    }
}

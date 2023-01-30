using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApp.Server.Services;
using System.Threading.Tasks;
using MovieApp.Shared.Models;


namespace MovieApp.Server.Controllers
{
    [Authorize]
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesDbService _dbService;

        public MoviesController(ILogger<MoviesController> logger, IMoviesDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            Console.WriteLine("Title: " + movie.Title);
            return Ok(await _dbService.AddMovie(movie));
        }

        [HttpGet("{movieId:int}")]
        public async Task<IActionResult> GetMovie(int movieId)
        {
            return Ok(await _dbService.GetMovie(movieId));
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _dbService.GetMovies());
        }

        [HttpPut("{movieId:int}")]
        public async Task<IActionResult> UpdateMovie(int movieId, [Bind("Title", "Summary", "InTheaters", "Trailer", "ReleaseDate", "Poster")] Movie movie)
        {
            return Ok(await _dbService.UpdateMovie(movieId, movie));
        }

        [HttpDelete("{movieId:int}")]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            return Ok(await _dbService.DeleteMovie(movieId));
        }

    }
}

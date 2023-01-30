using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MovieApp.Shared.Models;

namespace MovieApp.Client.Repository
{

    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies(HttpClient client);
        Task<Movie> GetMovie(HttpClient client, int movieId);
        Task<Movie> CreateMovie(HttpClient client, Movie movie);
        Task<Movie> UpdateMovie(HttpClient client, int movieId, Movie movie);
        Task<Movie> DeleteMovie(HttpClient client, int movieId);
    }

    public class MovieRepository : IMovieRepository
    {
        private static readonly string url = "https://localhost:5001/api/movies/";

        public async Task<List<Movie>> GetMovies(HttpClient client)
        {
            var responseHTTP = await client.GetAsync(url);

            if (!responseHTTP.IsSuccessStatusCode) return null;
            var responseString = await responseHTTP.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Movie>>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Movie> GetMovie(HttpClient client, int movieId)
        {
            var responseHTTP = await client.GetAsync(url + movieId);

            if (!responseHTTP.IsSuccessStatusCode) return null;
            var responseString = await responseHTTP.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Movie>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Movie> CreateMovie(HttpClient client, Movie movie)
        {
            var payload = JsonSerializer.Serialize(movie);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var responseHTTP = await client.PostAsync(url, content);

            if (!responseHTTP.IsSuccessStatusCode) return null;
            var responseString = await responseHTTP.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Movie>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Movie> UpdateMovie(HttpClient client, int movieId, Movie movie)
        {
            var payload = JsonSerializer.Serialize(movie);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var responseHTTP = await client.PutAsync(url + movieId, content);
            if (!responseHTTP.IsSuccessStatusCode) return null;
            var responseString = await responseHTTP.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Movie>(responseString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Movie> DeleteMovie(HttpClient client, int movieId)
        {
            var responseHTTP = await client.DeleteAsync(url + movieId);

            if (!responseHTTP.IsSuccessStatusCode) return null;
            var responseString = await responseHTTP.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Movie>
            (responseString, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }
            );

        }
    }
}
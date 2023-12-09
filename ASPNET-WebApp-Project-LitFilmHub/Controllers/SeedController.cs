using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using CsvHelper.Configuration;
using ClassLibrary_LitFilmHub;
using System.Diagnostics.Metrics;
using System.Globalization;
using ASPNET_WebApp_Project_LitFilmHub.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_WebApp_Project_LitFilmHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly LiteratureAndFilmDbContext _db;
        private readonly string _pathName;

        public SeedController(LiteratureAndFilmDbContext db, string pathName)
        {
            _db = db;
            _pathName = pathName;
        }

        [HttpGet("Books")]
        public async Task<IActionResult> ImportBooksAsync()
        {
            // create a lookup dictionary containing all the books already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Book> booksByTitle = _db.Books
                .AsNoTracking().ToDictionary(x => x.Title, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            IEnumerable<booksCsv>? records = csv.GetRecords<booksCsv>();
            foreach (booksCsv record in records)
            {
                if (booksByTitle.ContainsKey(record.Title))
                {
                    continue;
                }

                Book book = new()
                {
                    Title = record.Title,
                    Author = record.Author,
                    Genre = record.Genre,
                    PublicationYear = record.PublicationYear,
                    Isbn = record.Isbn,
                    Summary = record.Summary,
                    CoverImageUrl = record.CoverImageUrl,
                };
                await _db.Books.AddAsync(book);
                booksByTitle.Add(record.Title, book);
            }

            await _db.SaveChangesAsync();

            return new JsonResult(booksByTitle.Count);
        }//end ImportBookAsync

        [HttpGet("Films")]
        public async Task<IActionResult> ImportFilmsAsync()
        {
            // create a lookup dictionary containing all the films already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Film> filmsByTitle = _db.Films
                .AsNoTracking().ToDictionary(x => x.Title, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            IEnumerable<filmsCsv>? records = csv.GetRecords<filmsCsv>();
            foreach (filmsCsv record in records)
            {
                if (filmsByTitle.ContainsKey(record.Title))
                {
                    continue;
                }

                Film film = new()
                {
                    Title = record.Title,
                    Director = record.Director,
                    Genre = record.Genre,
                    ReleaseYear = record.ReleaseYear,
                    Rating = record.Rating,
                    Synopsis = record.Synopsis,
                    PosterImageUrl = record.PosterImageUrl,
                };
                await _db.Films.AddAsync(film);
                filmsByTitle.Add(record.Title, film);
            }

            await _db.SaveChangesAsync();

            return new JsonResult(filmsByTitle.Count);
        }//end ImportFilmAsync

    }//end SeedController
}

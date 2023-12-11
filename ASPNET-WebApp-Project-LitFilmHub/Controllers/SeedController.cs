using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CsvHelper;
using CsvHelper.Configuration;
using ClassLibrary_LitFilmHub;
using System.Diagnostics.Metrics;
using System.Globalization;
using ASPNET_WebApp_Project_LitFilmHub.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASPNET_WebApp_Project_LitFilmHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly LiteratureAndFilmDbContext _db;
        private UserManager<LiteratureAndFilmUser> _userManager;
        //private readonly string _pathName;
        private readonly string _booksPathName;
        private readonly string _filmsPathName;
        private readonly string _membersPathName;
        private readonly string _discussionsPathName;

        public SeedController(LiteratureAndFilmDbContext db, IWebHostEnvironment environment, UserManager<LiteratureAndFilmUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            //_pathName = Path.Combine(environment.ContentRootPath, "Data/worldcities.csv");
            _booksPathName = Path.Combine(environment.ContentRootPath, "Data/books.csv");
            _filmsPathName = Path.Combine(environment.ContentRootPath, "Data/films.csv");
            _membersPathName = Path.Combine(environment.ContentRootPath, "Data/members.csv");
            _discussionsPathName = Path.Combine(environment.ContentRootPath, "Data/discussions.csv");

        }

        [HttpGet("ResetIdentity")]
        public async Task<IActionResult> ResetIdentityAsync()
        {
            var tables = new[] { "Books", "Films", "Members", "Discussions" }; 
            foreach (var table in tables)
            {
                var resetIdentitySql = $"DBCC CHECKIDENT ('{table}', RESEED, 0);";
                await _db.Database.ExecuteSqlRawAsync(resetIdentitySql);
            }

            return Ok("Identity columns reset.");
        }//end ResetIdentityAsync



        [HttpPost("Users")]
        public async Task<IActionResult> ImportUsersAsync()
        {
            List<LiteratureAndFilmUser> usersList = new();

            (string name, string email) = ("user", "user@email.com");
            LiteratureAndFilmUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await _userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user";
            }
            _ = await _userManager.CreateAsync(user, "P@ssw0rd!")
                   ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await _db.SaveChangesAsync();
            return Ok();
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

            using StreamReader reader = new(_booksPathName);
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
                    Isbn = record.ISBN,
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

            using StreamReader reader = new(_filmsPathName);
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


        [HttpGet("Members")]
        public async Task<IActionResult> ImportMembersAsync()
        {
            // Assuming you have a CSV file for members
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_membersPathName);
            using CsvReader csv = new(reader, config);

            IEnumerable<MembersCsv>? records = csv.GetRecords<MembersCsv>();
            foreach (MembersCsv record in records)
            {
                Member member = new()
                {
                    // Assuming MemberID is not in the CSV since it's typically auto-generated by the database
                    FirstName = record.FirstName,
                    MiddleInitial = record.MiddleInitial,
                    LastName = record.LastName,
                    City = record.City,
                    Country = record.Country,
                    Email = record.Email,
                    Username = record.Username,
                    Password = record.Password // Remember to hash the password
                };

                await _db.Members.AddAsync(member);
            }

            await _db.SaveChangesAsync();

            // Return the count of imported members
            return new JsonResult("Members imported successfully");
        }//end ImportMembersAsync




        [HttpGet("Discussions")]
        public async Task<IActionResult> ImportDiscussionsAsync()
        {
            // Assuming you have a CSV file for discussions
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_discussionsPathName); // Update with the path to your CSV file
            using CsvReader csv = new(reader, config);

            IEnumerable<DiscussionsCsv>? records = csv.GetRecords<DiscussionsCsv>(); // Replace DiscussionsCsv with your CSV model
            foreach (DiscussionsCsv record in records)
            {
                // Directly use MemberID from the record assuming it's correctly associated
                Discussion discussion = new()
                {
                    MemberID = record.MemberID,
                    Content = record.Content
                    // Add additional fields here if your CSV contains more information
                };

                await _db.Discussions.AddAsync(discussion);
            }

            await _db.SaveChangesAsync();

            return new JsonResult("Discussions imported successfully");
        }



    }//end SeedController
}

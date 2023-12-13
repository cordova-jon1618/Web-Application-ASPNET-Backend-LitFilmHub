using Microsoft.AspNetCore.Authorization;
using ClassLibrary_LitFilmHub;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET_WebApp_Project_LitFilmHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly LiteratureAndFilmDbContext _db;

        public SearchController(LiteratureAndFilmDbContext db)
        {
            _db = db;
        }


        // GET: api/book-detail
        [HttpGet("book-detail")]
        public IEnumerable<Book> GetBookDetail()
        {
            return _db.Books.ToList();
        }

        // GET: api/film-detail
        [HttpGet("film-detail")]
        public IEnumerable<Film> GetFilmDetail()
        {
            return _db.Films.ToList();
        }


        // GET: api/<SearchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

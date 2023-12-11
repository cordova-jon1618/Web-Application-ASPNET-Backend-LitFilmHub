using Microsoft.AspNetCore.Authorization;
using ClassLibrary_LitFilmHub;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET_WebApp_Project_LitFilmHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LiteratureAndFilmDbContext _db;

        public BookController(LiteratureAndFilmDbContext db)
        {
            _db = db;
        }

        // GET: api/<BookController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Book> Get()
        {
            return _db.Books.ToList();
        }

        // GET: api/book-detail
        [HttpGet("book-detail")]
        public IEnumerable<Book> GetBookDetail()
        {
            return _db.Books.ToList();
        }



        // GET: api/<BookController>
        /*        [HttpGet]
                public IEnumerable<string> Get()
                {
                    return new string[] { "value1", "value2" };
                }*/

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

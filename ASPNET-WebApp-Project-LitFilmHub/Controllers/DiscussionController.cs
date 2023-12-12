using ClassLibrary_LitFilmHub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNET_WebApp_Project_LitFilmHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {

        private readonly LiteratureAndFilmDbContext _db;

        public DiscussionController(LiteratureAndFilmDbContext db)
        {
            _db = db;
        }

        // GET: api/<DiscussionController>
        [HttpGet]
        //[Authorize]
        public IEnumerable<Discussion> Get()
        {
            return _db.Discussions.ToList();
        }

        // GET api/<DiscussionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DiscussionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DiscussionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DiscussionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using AspHangFire.Data;
using AspHangFire.Models;
using AspHangFire.Service;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace AspHangFire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public PersonController(ApplicationDbContext context, IBackgroundJobClient backgroundJobClient)
        {
            _context = context;
            _backgroundJobClient = backgroundJobClient; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Nice");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Person person )
        {
          
                _backgroundJobClient.Enqueue<IPeopleRepository>(repository =>
                repository.CreatePerson(person));
                return Ok("Person Created,Response");
        }

        [HttpPost("schedule")]
        public IActionResult Schedule(string person)
        {
            _backgroundJobClient.Schedule(() => Console.WriteLine("The person name is "+ person),
                TimeSpan.FromMinutes(5));
            return Ok();
        }

       
    }
}

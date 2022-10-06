using AspHangFire.Data;
using AspHangFire.Models;
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
            _backgroundJobClient.Enqueue(()=>Console.WriteLine(person.Name));
            if (ModelState.IsValid)
            {
                _backgroundJobClient.Enqueue(() => CreatePerson(person));
                return Ok("Person Created,Response");
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task CreatePerson(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            Console.WriteLine("Person Created");
            await Task.Delay(5000);
            
        }
    }
}

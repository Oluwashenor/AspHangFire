using AspHangFire.Data;
using AspHangFire.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspHangFire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Nice");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Person person )
        {
            if (ModelState.IsValid)
            {
                await _context.People.AddAsync(person);
                if(await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Person Created,Response");
                }
                
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}

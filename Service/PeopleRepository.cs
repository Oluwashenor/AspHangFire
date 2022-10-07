using AspHangFire.Data;
using AspHangFire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspHangFire.Service
{

    public interface IPeopleRepository
    {
        Task CreatePerson(Person person);
    }
    public class PeopleRepository :IPeopleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PeopleRepository> _logger;

        public PeopleRepository(ApplicationDbContext context, ILogger<PeopleRepository> logger)
        {
            _context = context; 
            _logger = logger;   
        }
        public async Task CreatePerson(Person person)
        {
            _logger.LogInformation($"Adding person {person.Name}");
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            
            await Task.Delay(5000);
            _logger.LogInformation($"Added person {person.Name}");

        }
    }

}

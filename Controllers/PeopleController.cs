using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netapi.Database;
using netapi.Models;

namespace netapi.Controllers;

[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly DataContext context;

    public PeopleController(DataContext context)
    {
        this.context = context;
    }

    [HttpGet(Name = "GetPeople")]
    public async Task<IActionResult> Get()
    {
        List<Person> people = await this.context.People
            .Include(m => m.Address).ToListAsync<Person>();
        return this.Ok(people);
    }

    [HttpPost(Name = "PostPeople")]
    public async Task<IActionResult> Post(Person person)
    {
        this.context.People.Add(person);
        await this.context.SaveChangesAsync();
        return this.Ok(person);
    }

    [HttpPut("{id}", Name = "PutPeople")]
    public async Task<IActionResult> Put(int id, Person person)
    {
        Person? _person = await this.context.People.FirstOrDefaultAsync(person => person.Id == id);
        if (_person == null)
        {
            return this.NotFound($"Person ({id}) not found");
        }
        person.Id = id;
        this.context.People.Update(person);
        await this.context.SaveChangesAsync();
        return this.Ok(person);
    }
    [HttpDelete("{id}", Name = "DeletePeople")]
    public async Task<IActionResult> Delete(int id)
    {

        Person? person = await this.context.People.FirstOrDefaultAsync(person => person.Id == id);
        if (person == null)
        {
            return this.NotFound($"Person ({id}) not found");
        }
        this.context.People.Remove(person);
        await this.context.SaveChangesAsync();
        return this.Ok(person);
    }
}
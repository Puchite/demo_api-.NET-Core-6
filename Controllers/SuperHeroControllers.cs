using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.controller 
{
    [ApiController]
    [Route("api/[controller]")]

    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
           _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            // var heroesByID = heroes.Find(hero => hero.Id == id );
            var heroesByID = await _context.SuperHeroes.FindAsync(id);
            
            if(heroesByID is null) 
            {
                return  BadRequest("Hero not found");
            }

            return Ok(heroesByID);
            
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Get(SuperHero request)
        {
            // var updateHeroesByID = heroes.Find(hero => hero.Id == request.Id );
            var updateHeroesByID = await _context.SuperHeroes.FindAsync(request.Id);
            
            if(updateHeroesByID is null) 
            {
                return  BadRequest("Hero not found");
            }

            updateHeroesByID.Name = request.Name;
            updateHeroesByID.FirstName = request.FirstName;
            updateHeroesByID.LastName = request.LastName;
            updateHeroesByID.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            // var deleteHeroesByID = heroes.Find(hero => hero.Id == id );
            var deleteHeroesByID = await _context.SuperHeroes.FindAsync(id);

            if(deleteHeroesByID is null) 
            {
                return  BadRequest("Hero not found");
            }

            _context.SuperHeroes.Remove(deleteHeroesByID);
            await _context.SaveChangesAsync();


            return NoContent();

        }

    }

}
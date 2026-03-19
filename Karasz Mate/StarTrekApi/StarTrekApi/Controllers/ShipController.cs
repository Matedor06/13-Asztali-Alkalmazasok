using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarTrekDatabase;
using StarTrekDatabase.Models;
using System.ComponentModel.DataAnnotations;

namespace StarTrekApi.Controllers
{
    [ApiController]
    public class ShipController(AppDbContext dbContext) : ControllerBase
    {
        [HttpGet]
        [Route("/api/ships")]
        public async Task<ActionResult<List<ShipModel>>> GetAllAsync()
        {
            var result = await dbContext
                .Ships.AsNoTracking()
                .Select(x => new ShipModel(x))
                .ToListAsync();
            if (result.Count == 0)
            {
                return NotFound("No ships found.");
            }
            return result;
        }


        [HttpGet]
        [Route("/api/ship/{id}")]
        public async Task<ActionResult<ShipModel>> GetByIdAsync([FromRoute][Required] int id)
        {
            var result = await dbContext.Ships.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound($"Ship with id '{id}' not found.");
            }
            return new ShipModel(result);
        }





        [HttpPost]
        [Route("api/ship")]
        public async Task<ActionResult<ShipModel>> CreateAsync([FromBody][Required] ShipModel model)
        {
            bool exists = await dbContext.Ships.AnyAsync(x => x.Name == model.Name);

            if (exists)
            {
                return Conflict($"Ship with name '{model.Name}' already exists.");
            }

            var ship = model.ToEntity();

            await dbContext.Ships.AddAsync(ship);
            await dbContext.SaveChangesAsync();

            return new ShipModel(ship);
        }




        [HttpPut]
        [Route("api/ship/{id}")]
        public async Task<ActionResult<ShipModel>> UpdateAsync(
        [FromBody][Required] ShipModel model,
        [FromRoute][Required] int id
)
        {
            var ship = await dbContext.Ships.FirstOrDefaultAsync(x => x.Id == id);
            if (ship == null)
            {
                return NotFound("Ship not found!");
            }
            ship.Name = model.Name;
            ship.Class = model.Class;
            ship.RaceFaction = model.RaceFaction;
            ship.Length = model.Length;
            ship.Crew = model.Crew;
            ship.MaxWarp = model.MaxWarp;
            ship.Armament = model.Armament;
            ship.ShieldType = model.ShieldType;
            ship.HullMaterial = model.HullMaterial;
            ship.Role = model.Role;


            dbContext.Ships.Attach(ship);

            await dbContext.SaveChangesAsync();

            return new ShipModel(ship);
        }



        [HttpDelete]
        [Route("api/ship/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute][Required] int id)
        {
            var result = await dbContext
                .Ships.AsNoTracking()
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
            if (result != 1)
            {
                return Conflict("Error during delete");
            }
            return Ok("Ship deleted");
        }













    }
}

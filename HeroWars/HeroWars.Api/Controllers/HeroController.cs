using System.ComponentModel.DataAnnotations;
using HeroWars.Database;
using HeroWars.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HeroWars.Api.Controllers;

[ApiController]
public class HeroController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [Route("/api/heroes")]
    public async Task<ActionResult<List<HeroModel>>> GetAllAsync()
    {
        var result = await dbContext
            .Heroes.AsNoTracking()
            .Select(x => new HeroModel(x))
            .ToListAsync();
        if (result.Count == 0)
        {
            return NotFound("No heroes found.");
        }
        return result;
    }

    [HttpGet]
    [Route("/api/hero/{id}")]
    public async Task<ActionResult<HeroModel>> GetByIdAsync([FromRoute] [Required] int id)
    {
        var result = await dbContext.Heroes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
        {
            return NotFound($"Hero with id '{id}' not found.");
        }
        return new HeroModel(result);
    }

    [HttpPost]
    [Route("api/hero")]
    public async Task<ActionResult<HeroModel>> CreateAsync([FromBody] [Required] HeroModel model)
    {
        bool exists = await dbContext.Heroes.AnyAsync(x => x.Name == model.Name);

        if (exists)
        {
            return Conflict($"Hero with name '{model.Name}' already exists.");
        }

        var hero = model.ToEntity();

        await dbContext.Heroes.AddAsync(hero);
        await dbContext.SaveChangesAsync();

        return new HeroModel(hero);
    }

    [HttpPut]
    [Route("api/hero/{id}")]
    public async Task<ActionResult<HeroModel>> UpdateAsync(
        [FromBody] [Required] HeroModel model,
        [FromRoute] [Required] int id
    )
    {
        var hero = await dbContext.Heroes.FirstOrDefaultAsync(x => x.Id == id);
        if (hero == null)
        {
            return NotFound("Hero not found!");
        }

        hero.Name = model.Name;
        hero.Intelligence = model.Intelligence;
        hero.Agility = model.Agility;
        hero.Strength = model.Strength;
        hero.Health = model.Health;
        hero.PhysicalAttack = model.PhysicalAttack;
        hero.MagicAttack = model.MagicAttack;
        hero.Armor = model.Armor;
        hero.MagicDefense = model.MagicDefense;
        hero.MagicPenetration = model.MagicPenetration;
        hero.ArmorPenetration = model.ArmorPenetration;

        dbContext.Heroes.Attach(hero);

        await dbContext.SaveChangesAsync();

        return new HeroModel(hero);
    }

    [HttpDelete]
    [Route("api/hero/{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] [Required] int id)
    {
        var result = await dbContext
            .Heroes.AsNoTracking()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        if (result != 1)
        {
            return Conflict("Error during delete");
        }
        return Ok("Hero deleted");
    }
}

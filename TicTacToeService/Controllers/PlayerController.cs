using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TicTacToeService.Repositories;

namespace TicTacToeService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : BaseController
{
    [HttpGet]
    public IEnumerable<Player> Get()
    {
        return db.Players.GetAll();
    }

    [HttpGet]
    [Route("Get")]
    public Player GetById(string id)
    {
        return db.Players.SingleOrDefault(c => c.Id == id);
    }

    [HttpPut]
    public IActionResult Put(string id, Player entity)
    {

        Player existingEntity = db.Players.SingleOrDefault(c => c.Id == id);
        if (existingEntity == null)
            return NotFound();


        var filter = Builders<Player>.Filter.Eq(x => x.Id, id);
        db.Players.Complete(filter, entity);
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(Player entity)
    {
        db.Players.Add(entity);
        return Ok(entity);
    }

    [HttpDelete]
    public IActionResult Delete(string id)
    {
        Player existingEntity = db.Players.SingleOrDefault(c => c.Id == id);
        if (existingEntity == null)
            return NotFound();

        var filter = Builders<Player>.Filter.Eq(x => x.Id, id);
        db.Players.Remove(filter);
        return Ok();
    }
}

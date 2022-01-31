using DAL.Models;
using DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TicTacToeService.Repositories;

namespace TicTacToeService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : BaseController
{
    [HttpGet]
    public IEnumerable<Game> Get()
    {
        return db.Games.GetAll();
    }

    [HttpGet]
    [Route("Get")]
    public Game GetById(string id)
    {
        return db.Games.SingleOrDefault(c => c.Id == id);
    }

    [HttpPut]
    public IActionResult Put(string id, Game entity)
    {

        Game existingEntity = db.Games.SingleOrDefault(c => c.Id == id);
        if (existingEntity == null)
            return NotFound();


        var filter = Builders<Game>.Filter.Eq(x => x.Id, id);
        db.Games.Complete(filter, entity);
        return Ok(entity);
    }

    [HttpPost]
    public IActionResult Post(Game entity)
    {
        db.Games.Add(entity);
        return Ok(entity);
    }

    [HttpDelete]
    public IActionResult Delete(string id)
    {
        Game existingEntity = db.Games.SingleOrDefault(c => c.Id == id);
        if (existingEntity == null)
            return NotFound();

        var filter = Builders<Game>.Filter.Eq(x => x.Id, id);
        db.Games.Remove(filter);
        return Ok();
    }

    [HttpPost]
    [Route("MakeMove")]
    public IActionResult MakeMove(PlayerMove playerMove)
    {
        try
        {
            Game activeGame = db.Games.SingleOrDefault(c => c.Id == playerMove.GameID);
            List<PlayerHistory> playerHistory = playerMove.ActivePlayer == 'X' ? activeGame.PlayerXHistory : activeGame.PlayerOHistory;
            playerHistory.Add(playerMove.Move);

            var filter = Builders<Game>.Filter.Eq(x => x.Id, activeGame.Id);
            db.Games.Complete(filter, activeGame);

            GameRules rules = new GameRules(db);
            Game completedGame = rules.CheckGame(playerMove.GameID, playerMove.ActivePlayer);
            return Ok(completedGame);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.StackTrace);
        }
    }
}

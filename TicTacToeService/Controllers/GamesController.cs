using DAL.Models;
using DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TicTacToeService.Repositories;

namespace TicTacToeService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : BaseController
{
    private static event EventHandler<string> PublishEvent;

    [HttpGet]
    [Route("Subscribe/{id}")]
    public async Task Subscribe(string id, CancellationToken cancellationToken)
    {
        //Setup SSE connection
        var response = Response;
        response.Headers.Add("Content-Type", "text/event-stream");

        await response.WriteAsync($"data: {"Connected!"}\r\r");
        response.Body.Flush();

        //Receive Publish Data from own channel
        EventHandler<string> onMessage = async (sender, data) =>
        {
            //Send to all SSE subscribers
            data = data.Replace("\r\n", "");
            await response.WriteAsync($"data: {data}\r\r");
            response.Body.Flush();
        };

        PublishEvent += onMessage;

        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
        }

        PublishEvent -= onMessage;
    }

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
        if (entity.GameMode == "Online Player")
        {
            PublishEvent?.Invoke(this, "reload");
        }
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

            if (completedGame.GameMode == "Online Player")
            {
                PublishEvent?.Invoke(this, "reload");
            }

            //PC move
            if (completedGame.GameMode == "AI" && completedGame.Completed != true)
            {
                rules.AIMove(playerMove.GameID, playerMove.ActivePlayer);
                Game completedGameAI = rules.CheckGame(playerMove.GameID, playerMove.ActivePlayer == 'X' ? 'O' : 'X');
                return Ok(completedGameAI);
            }
            return Ok(completedGame);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.StackTrace);
        }
    }

    [HttpGet]
    [Route("GetPlayerRoom")]
    public Game GetPlayerRoom(string id)
    {
        return db.Games.Find(c => c.GameMode == "Online Player").LastOrDefault();
    }
}

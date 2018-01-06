using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyGameList.Data;
using MyGameList.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGameList.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly GameContext _context;
        private readonly ILogger<GamesController> _logger;

        public GamesController(ILogger<GamesController> logger, GameContext context) {
            _logger = logger;
            _context = context;
        }

        // GET: api/games
        [HttpGet]
        public IEnumerable<Game> GetGames() {
            _logger.LogDebug("[Games] GetAll called");

            return _context.Games.ToList();
        }

        // GET api/games/5
        [HttpGet("{id}", Name = "GetGame")]
        public IActionResult GetGameById(long id) {
            _logger.LogDebug($"[Games] Get {id} called");

            Game game = _context.Games.Find(id);
            if (game == null) {
                return NotFound();
            }

            return new ObjectResult(game);
        }

        // POST api/games
        [HttpPost]
        public IActionResult CreateGame([FromBody] Game game) {
            _logger.LogDebug($"[Games] Post {PrettyPrint(game)} called");

            if (game == null) {
                return BadRequest();
            }

            _context.Games.Add(game);
            _context.SaveChanges();

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }

        // PUT api/games/5
        [HttpPut("{id}")]
        public IActionResult UpdateGame(long id, [FromBody] Game game) {
            _logger.LogDebug($"[Games] Put id: {id} game: {PrettyPrint(game)} called");

            if (game == null) {
                return BadRequest();
            }
            game.Id = id;

            Game gameToUpdate = _context.Games.Find(id);
            if (gameToUpdate == null) {
                return NotFound();
            }

            gameToUpdate.Title = game.Title;
            gameToUpdate.Description = game.Description;

            _context.Games.Update(gameToUpdate);
            _context.SaveChanges();

            return new NoContentResult();
        }

        // DELETE api/games/5
        [HttpDelete("{id}")]
        public IActionResult DeleteGame(long id) {
            _logger.LogDebug($"[Games] Delete {id} called");

            Game gameToDelete = _context.Games.Find(id);
            if (gameToDelete == null) {
                return NotFound();
            }

            _context.Games.Remove(gameToDelete);
            _context.SaveChanges();

            return new NoContentResult();
        }

        private string PrettyPrint(Object o) {
            return JsonConvert.SerializeObject(o, Formatting.Indented);
        }
    }
}

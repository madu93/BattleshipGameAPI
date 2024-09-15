using BattleshipGameAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private static GameBoard board = new GameBoard();

        [HttpGet("reset")]
        public IActionResult ResetGame()
        {
            board = new GameBoard();
            return Ok(new { Message = "Game reset!" });
        }

        [HttpPost("attack")]
        public IActionResult Attack([FromBody] AttackRequest request)
        {
            int x = request.X;
            int y = request.Y;

            bool isHit = board.Attack(x, y, out Ship hitShip);
            if (board.IsAllShipsSunk)
            {
                return Ok(new { Message = $"All ships sunk! You won!" });
            }

            if (isHit)
            {
                string status = hitShip.IsSunk ? $"{hitShip.Name} sunk!" : "Hit!";
                return Ok(new { Message = status });
            }

            return Ok(new { Message = "Miss!" });
        }
    }



}

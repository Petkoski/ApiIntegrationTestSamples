using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces.Gateways.Repositories;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        // GET api/players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get()
        {
            return await _playerRepository.ListAll();
        }

        // GET api/players/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            return await _playerRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> Post()
        {
            return await _playerRepository.Add(new Player("Jovan", "Petkoski", 184, 91, new DateTime(1995, 1, 20)) { Created = DateTime.UtcNow });
        }

        // DELETE api/players/{id}
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            Player player = _playerRepository.GetTById(id);
            await _playerRepository.Delete(player);
        }
    }
}
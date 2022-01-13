using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proline.CentralEngine.DBApi.Contexts;
using Proline.CentralEngine.DBApi.Models.Central;

namespace Proline.Component.UserManagment.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerIndentitiesController : ControllerBase
    {
        private readonly ProlineCentralContext _context;

        public PlayerIndentitiesController(ProlineCentralContext context)
        {
            _context = context;
        }

        // GET: api/PlayerIndentities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerIndentity>>> GetPlayerIdentity()
        {
            return await _context.PlayerIdentity.ToListAsync();
        }

        // GET: api/PlayerIndentities/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<PlayerIndentity>> GetPlayerIndentity(long id)
        {
            var playerIndentity = await _context.PlayerIdentity.FindAsync(id);

            if (playerIndentity == null)
            {
                return NotFound();
            }

            return playerIndentity;
        }

        // GET: api/PlayerIndentities/5
        [HttpGet("{identifier}")]
        public async Task<ActionResult<PlayerIndentity>> GetPlayerIndentity(string identifier)
        {
            var playerIndentity = _context.PlayerIdentity.First(e=>e.Identifier.Equals(identifier));

            if (playerIndentity == null)
            {
                return NotFound();
            }

            return playerIndentity;
        }

        // PUT: api/PlayerIndentities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerIndentity(long id, PlayerIndentity playerIndentity)
        {
            if (id != playerIndentity.IdentityId)
            {
                return BadRequest();
            }

            _context.Entry(playerIndentity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerIndentityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PlayerIndentities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlayerIndentity>> PostPlayerIndentity(PlayerIndentity playerIndentity)
        {
            _context.PlayerIdentity.Add(playerIndentity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerIndentity", new { id = playerIndentity.IdentityId }, playerIndentity);
        }

        // DELETE: api/PlayerIndentities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerIndentity>> DeletePlayerIndentity(long id)
        {
            var playerIndentity = await _context.PlayerIdentity.FindAsync(id);
            if (playerIndentity == null)
            {
                return NotFound();
            }

            _context.PlayerIdentity.Remove(playerIndentity);
            await _context.SaveChangesAsync();

            return playerIndentity;
        }

        private bool PlayerIndentityExists(long id)
        {
            return _context.PlayerIdentity.Any(e => e.IdentityId == id);
        }
    }
}

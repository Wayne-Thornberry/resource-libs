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
    public class LinkedIndentitiesController : ControllerBase
    {
        private readonly ProlineCentralContext _context;

        public LinkedIndentitiesController(ProlineCentralContext context)
        {
            _context = context;
        }

        // GET: api/PlayerIndentities
        [HttpGet]
        [Route("GetAllIdentities")]
        public async Task<ActionResult<IEnumerable<LinkedIndentity>>> GetAllIdentities()
        {
            return await _context.LinkedIdentity.ToListAsync();
        }

        [HttpGet]
        [Route("GetAllMatchingIdentities")]
        public async Task<ActionResult<IEnumerable<LinkedIndentity>>> GetAllMatchingIdentities(List<string> identities)
        {
            return await _context.LinkedIdentity.Where(e=> identities.Contains(e.Identifier)).ToListAsync();
        }

        // GET: api/PlayerIndentities/5
        [HttpGet]
        [Route("GetAllPlayerIdentities")]
        public async Task<ActionResult<IEnumerable<LinkedIndentity>>> GetAllPlayerIndentities(long playerId)
        {
            var playerIndentity = await _context.LinkedIdentity.Where(e=>e.PlayerId == playerId).ToListAsync();

            if (playerIndentity == null)
            {
                return NotFound();
            }

            return playerIndentity;
        }

        [HttpGet]
        [Route("GetAllUserIdentities")]
        public async Task<ActionResult<IEnumerable<LinkedIndentity>>> GetAllUserIdentities(long userId)
        {
            var playerIndentity = await _context.LinkedIdentity.Where(e => e.UserId == userId).ToListAsync();

            if (playerIndentity == null)
            {
                return NotFound();
            }

            return playerIndentity;
        }

        [HttpGet]
        [Route("GetIdentity")]
        public async Task<ActionResult<LinkedIndentity>> GetIdentity(long id)
        {
            var playerIndentity = await _context.LinkedIdentity.FindAsync(id);

            if (playerIndentity == null)
            {
                return NotFound();
            }

            return playerIndentity;
        }

        [HttpGet]
        [Route("GetMatchingIdentity")]
        public async Task<ActionResult<LinkedIndentity>> GetMatchingIdentity(string identifier)
        {
            var playerIndentity = await _context.LinkedIdentity.FirstOrDefaultAsync(e=>e.Identifier.Equals(identifier));

            if (playerIndentity == null)
            {
                return NotFound();
            }

            return playerIndentity;
        }


        // POST: api/PlayerIndentities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("PostIdentity")]
        public async Task<ActionResult<LinkedIndentity>> PostPlayerIndentity(LinkedIndentity playerIndentity)
        {
            _context.LinkedIdentity.Add(playerIndentity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerIndentity", new { id = playerIndentity.IdentityId }, playerIndentity);
        }

        // POST: api/PlayerIndentities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("PostIdentities")]
        public async Task<ActionResult<LinkedIndentity>> PostPlayerIndentity(List<LinkedIndentity> playerIndentities)
        {
            _context.LinkedIdentity.AddRange(playerIndentities);
            await _context.SaveChangesAsync();

            var x = new List<object>();
            foreach (var item in playerIndentities)
            {
                x.Add(new { id = item.IdentityId });
            }

            return CreatedAtAction("GetPlayerIndentity", x, playerIndentities);
        }

        // PUT: api/PlayerIndentities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("PutIdentity")]
        public async Task<IActionResult> PutPlayerIndentity(long id, LinkedIndentity playerIndentity)
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


        // DELETE: api/PlayerIndentities/5
        [HttpDelete]
        [Route("DeleteIdentity")]
        public async Task<ActionResult<LinkedIndentity>> DeletePlayerIndentity(long id)
        {
            var playerIndentity = await _context.LinkedIdentity.FindAsync(id);
            if (playerIndentity == null)
            {
                return NotFound();
            }

            _context.LinkedIdentity.Remove(playerIndentity);
            await _context.SaveChangesAsync();

            return playerIndentity;
        }

        private bool PlayerIndentityExists(long id)
        {
            return _context.LinkedIdentity.Any(e => e.IdentityId == id);
        }
    }
}

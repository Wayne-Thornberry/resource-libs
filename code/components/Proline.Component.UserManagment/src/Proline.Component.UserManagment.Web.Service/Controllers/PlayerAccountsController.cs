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
    public class PlayerAccountsController : ControllerBase
    {
        private readonly ProlineCentralContext _context;

        public PlayerAccountsController(ProlineCentralContext context)
        {
            _context = context;
        }

        // GET: api/PlayerAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerAccount>>> GetPlayerAccounts()
        {
            return await _context.PlayerAccounts.ToListAsync();
        }

        // GET: api/PlayerAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerAccount>> GetPlayerAccount(long id)
        {
            var playerAccount = await _context.PlayerAccounts.FindAsync(id);

            if (playerAccount == null)
            {
                return NotFound();
            }

            return playerAccount;
        }

        // PUT: api/PlayerAccounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerAccount(long id, PlayerAccount playerAccount)
        {
            if (id != playerAccount.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(playerAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerAccountExists(id))
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

        // POST: api/PlayerAccounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlayerAccount>> PostPlayerAccount(PlayerAccount playerAccount)
        {
            _context.PlayerAccounts.Add(playerAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerAccount", new { id = playerAccount.PlayerId }, playerAccount);
        }

        // DELETE: api/PlayerAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerAccount>> DeletePlayerAccount(long id)
        {
            var playerAccount = await _context.PlayerAccounts.FindAsync(id);
            if (playerAccount == null)
            {
                return NotFound();
            }

            _context.PlayerAccounts.Remove(playerAccount);
            await _context.SaveChangesAsync();

            return playerAccount;
        }

        private bool PlayerAccountExists(long id)
        {
            return _context.PlayerAccounts.Any(e => e.PlayerId == id);
        }
    }
}

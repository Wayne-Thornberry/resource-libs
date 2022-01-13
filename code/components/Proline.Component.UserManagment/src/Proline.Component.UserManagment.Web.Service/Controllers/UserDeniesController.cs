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
    public class UserDeniesController : ControllerBase
    {
        private readonly ProlineCentralContext _context;

        public UserDeniesController(ProlineCentralContext context)
        {
            _context = context;
        }

        // GET: api/UserDenies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDeny>>> GetUserDeny()
        {
            return await _context.UserDeny.ToListAsync();
        }

        // GET: api/UserDenies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDeny>> GetUserDeny(long id)
        {
            var userDeny = await _context.UserDeny.FindAsync(id);

            if (userDeny == null)
            {
                return NotFound();
            }

            return userDeny;
        }

        // GET: api/UserDenies/5
        [HttpGet("{userId:long}")]
        public async Task<ActionResult<IEnumerable<UserDeny>>> GetUserDenies(long userId)
        {
            var userDeny =  _context.UserDeny.Where(e=>e.UserId == userId).ToList();

            if (userDeny == null)
            {
                return NotFound();
            }

            return userDeny;
        }

        // PUT: api/UserDenies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDeny(long id, UserDeny userDeny)
        {
            if (id != userDeny.DenyId)
            {
                return BadRequest();
            }

            _context.Entry(userDeny).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDenyExists(id))
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

        // POST: api/UserDenies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserDeny>> PostUserDeny(UserDeny userDeny)
        {
            _context.UserDeny.Add(userDeny);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDeny", new { id = userDeny.DenyId }, userDeny);
        }

        // DELETE: api/UserDenies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDeny>> DeleteUserDeny(long id)
        {
            var userDeny = await _context.UserDeny.FindAsync(id);
            if (userDeny == null)
            {
                return NotFound();
            }

            _context.UserDeny.Remove(userDeny);
            await _context.SaveChangesAsync();

            return userDeny;
        }

        private bool UserDenyExists(long id)
        {
            return _context.UserDeny.Any(e => e.DenyId == id);
        }
    }
}

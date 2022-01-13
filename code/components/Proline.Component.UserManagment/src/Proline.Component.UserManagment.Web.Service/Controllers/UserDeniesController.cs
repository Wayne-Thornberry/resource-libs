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
        [Route("GetDenials")]
        public async Task<ActionResult<IEnumerable<UserDenial>>> GetDenials()
        {
            return await _context.UserDenial.ToListAsync();
        }

        // GET: api/UserDenies/5
        [HttpGet]
        [Route("GetDenial")]
        public async Task<ActionResult<UserDenial>> GetDeny(long id)
        {
            var userDeny = await _context.UserDenial.FindAsync(id);

            if (userDeny == null)
            {
                return NotFound();
            }

            return userDeny;
        }

        // GET: api/UserDenies/5
        [HttpGet]
        [Route("GetUserDenials")]
        public async Task<ActionResult<IEnumerable<UserDenial>>> GetUserDenials(long userId)
        {
            var userDeny =  _context.UserDenial.Where(e=>e.UserId == userId).ToList();

            if (userDeny == null)
            {
                return NotFound();
            }

            return userDeny;
        }



        // POST: api/UserDenies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("PostDenial")]
        public async Task<ActionResult<UserDenial>> PostUserDeny(UserDenial userDeny)
        {
            _context.UserDenial.Add(userDeny);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDeny", new { id = userDeny.DenyId }, userDeny);
        }

        // PUT: api/UserDenies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("PutDenial")]
        public async Task<IActionResult> PutUserDeny(long id, UserDenial userDeny)
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

        // DELETE: api/UserDenies/5
        [HttpDelete]
        [Route("DeleteDenial")]
        public async Task<ActionResult<UserDenial>> DeleteUserDeny(long id)
        {
            var userDeny = await _context.UserDenial.FindAsync(id);
            if (userDeny == null)
            {
                return NotFound();
            }

            _context.UserDenial.Remove(userDeny);
            await _context.SaveChangesAsync();

            return userDeny;
        }

        private bool UserDenyExists(long id)
        {
            return _context.UserDenial.Any(e => e.DenyId == id);
        }
    }
}

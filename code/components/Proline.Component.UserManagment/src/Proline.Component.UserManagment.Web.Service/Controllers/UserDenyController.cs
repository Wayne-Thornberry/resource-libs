using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proline.CentralEngine.DBApi.Contexts;
using Proline.Online.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.Component.UserManagment.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserDenyController : ControllerBase
    {
        private ProlineCentralContext _context;

        public UserDenyController(ProlineCentralContext context)
        {
            _context = context;
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDenyOutParameter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(long id)
        {
            UserDenyOutParameter userDeny = null;
            if (id < 0)
                return BadRequest();
            try
            {
                var deny = _context.UserDeny.FirstOrDefault(e => e.UserId == id); 
                if (deny == null)
                    return Problem();
                userDeny = new UserDenyOutParameter()
                {
                    DenyId = deny.DenyId,
                    Reason = deny.Reason,
                    Untill = deny.ExpiresAt,
                };
            }
            catch (Exception)
            { 
                return Problem();
            }
            return Ok(userDeny);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(UserDenyInParameter inParameter)
        {
            if (inParameter == null)
                return BadRequest();

            try
            { 
                _context.UserDeny.Add(new CentralEngine.DBApi.Models.Central.UserDeny() {

                    UserId = inParameter.UserId,
                    Reason = inParameter.Reason,
                    BannedAt = DateTime.UtcNow,
                    ExpiresAt = inParameter.Untill, 
                });
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return Problem(); 
                throw;
            } 
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.Component.UserManagment.Web.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        //[HttpGet("{id:long}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerAccountOutParameter))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Get(long id)
        //{
        //    UserAccountOutParameter outParameter = null;
        //    if (id < 0)
        //        return BadRequest();

        //    try
        //    {
        //        var account = _context.Users.FirstOrDefault(e => e.UserId == id);
        //        if (account == null)
        //            return Problem();
        //        outParameter = new UserAccountOutParameter()
        //        {
        //            UserId = account.UserId,
        //            Username = account.Username,
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return Problem();
        //    }
        //    return Ok(outParameter);
        //}
    }
}

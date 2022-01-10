using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using Proline.Online.Data;
using Proline.CentralEngine.DBApi.Contexts; 
using Microsoft.AspNetCore.Mvc;
using Proline.CentralEngine.DBApi.Models.Central;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Proline.Component.Web.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private ProlineCentralContext _context;

        public UserController(ProlineCentralContext context)
        {
            _context = context;
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserAccountOutParameter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(long id)
        {
            UserAccountOutParameter outParameter = null;
            if (id < 0)
                return BadRequest();

            try
            {
                var account = _context.Users.FirstOrDefault(e => e.UserId == id);
                if(account == null)
                    return Problem(); 
                outParameter = new UserAccountOutParameter()
                {
                    UserId = account.UserId,
                    Username = account.Username,
                };
            }
            catch (Exception)
            {
                return Problem(); 
            }
            return Ok(outParameter); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(UserAccountInParameter inParameter)
        {
            string username = inParameter.Username;
            List<IdentifierInParameter> identifiers = inParameter.Identifiers; 
            if (string.IsNullOrEmpty(username) || identifiers == null)
                return BadRequest();

            try
            {
                var account = new UserAccount()
                {
                    Username = username,
                    GroupId = 0,
                    Priority = 0,
                    CreatedOn = DateTime.Now,
                };

                var player = new PlayerAccount()
                {
                    Name = username,
                    Priority = 0,
                    RegisteredAt = DateTime.Now,
                };
                _context.Users.Add(account);
                _context.PlayerRegistration.Add(player);
                _context.SaveChanges();

                foreach (var item in identifiers)
                {
                    _context.PlayerIdentity.Add(new PlayerIndentity()
                    {
                        Identifier = item.Identifier,
                        IdentityTypeId = (int) item.IdentitierType,
                        UserId = account.UserId,
                        PlayerId = player.PlayerId
                    });
                } 
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
            } 
            return Ok();
        }

    }
}
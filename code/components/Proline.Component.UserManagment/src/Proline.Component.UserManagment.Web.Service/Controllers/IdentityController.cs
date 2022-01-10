using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proline.CentralEngine.DBApi.Contexts;
using Proline.CentralEngine.DBApi.Models.Central;
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
    public class IdentityController : ControllerBase
    {
        private ProlineCentralContext _context;

        public IdentityController(ProlineCentralContext context)
        {
            _context = context;
        }


        [HttpGet("{identifier}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IdentityOutParameter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string identifier)
        {
            IdentityOutParameter id = null;
            if (string.IsNullOrEmpty(identifier))
                return BadRequest();
            try
            {
                var identity = _context.PlayerIdentity.FirstOrDefault(e => e.Identifier.Equals(identifier));
                if (identity == null)
                    return Problem();
                id = new IdentityOutParameter()
                {
                    Identifier = identity.Identifier,
                    IdentifierType = identity.IdentityTypeId,
                    PlayerId = identity.PlayerId,
                    UserId = identity.UserId
                };
            }
            catch (Exception)
            {
                return Problem();
            }
            return Ok(id);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IdentityOutParameter>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(List<IdentifierInParameter> identities)
        {
            List<IdentityOutParameter> id = new List<IdentityOutParameter>();
            if (identities == null)
                return BadRequest();
            try
            {
                foreach (var item in identities)
                {
                    var identity = _context.PlayerIdentity.FirstOrDefault(e => e.Identifier.Equals(item.Identifier));
                    if (identity == null)
                        return Problem();
                    id.Add(new IdentityOutParameter()
                    {
                        Identifier = identity.Identifier,
                        IdentifierType = identity.IdentityTypeId,
                        PlayerId = identity.PlayerId,
                        UserId = identity.UserId
                    });
                }
               
            }
            catch (Exception)
            {
                return Problem();
            }
            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(IdentifierInParameter inParameter)
        {
            if (inParameter == null)
                return BadRequest();
            try
            {
                _context.PlayerIdentity.Add(new PlayerIndentity()
                {
                    Identifier = inParameter.Identifier,
                    IdentityTypeId = inParameter.IdentitierType,
                    PlayerId = inParameter.PlayerId,
                    UserId = inParameter.UserId,
                });
                _context.SaveChanges();
            }
            catch (Exception e)
            { 
                return Problem();
                throw;
            }
            return Ok();
        }


    }
}

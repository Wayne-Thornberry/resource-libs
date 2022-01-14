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
    public class UserManagmentController : ControllerBase
    {
        private ProlineCentralContext _context;

        public UserManagmentController(ProlineCentralContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Attempts to register a new user and create a new Player using the provided identifiers, if the primary identifier matches an already existing user
        /// account, returns that account and assosiated player account instead. If the primary identifier does not match, but a secondary identifier does,
        /// creates a new player account and matches the primary identifier with that existing user id and new player account. If all identifiers dont match
        /// creates a new user account and a new player account and assosiates the inserted identifiers to that user id and player id
        /// </summary>
        /// <param name="inParameter"></param>
        /// <returns>UserAccountOutParameter</returns>
        [HttpPost]
        [Route("LoginPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginPlayerOutParameter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginPlayer(LoginPlayerInParameter inParameter)
        {

            if (inParameter == null || string.IsNullOrEmpty(inParameter.Identifier))
                return BadRequest();

            try
            { 
                var identity = _context.LinkedIdentity.FirstOrDefault(e => e.Identifier.Equals(inParameter.Identifier));
                if (identity == null)
                    return Ok(new EmptyResult());

                var denies = _context.UserDenial.Where(e => e.UserId == identity.UserId).OrderByDescending(e => e.ExpiresAt);
                var x = new LoginPlayerOutParameter()
                {
                    UserId = identity.UserId,
                    PlayerId = identity.PlayerId,
                    Deny = new UserDenyOutParameter(),
                };
                if(denies.Count() > 0)
                {
                    var deny = denies.First();
                    x.IsDenied = true;
                    x.Deny = new UserDenyOutParameter()
                    {
                        DenyId = deny.DenyId,
                        Reason = deny.Reason,
                        Untill = deny.ExpiresAt,
                    };
                } 
                return Ok(x);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
                throw;
            }
        }


        /// <summary>
        /// Attempts to register a new user and create a new Player using the provided identifiers, if the primary identifier matches an already existing user
        /// account, returns that account and assosiated player account instead. If the primary identifier does not match, but a secondary identifier does,
        /// creates a new player account and matches the primary identifier with that existing user id and new player account. If all identifiers dont match
        /// creates a new user account and a new player account and assosiates the inserted identifiers to that user id and player id
        /// </summary>
        /// <param name="inParameter"></param>
        /// <returns>UserAccountOutParameter</returns>
        [HttpPost]
        [Route("RegisterPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegistrationPlayerOutParameter))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterPlayer(RegistrationPlayerInParameter inParameter)
        {

            if (inParameter == null || inParameter.Identifiers == null || inParameter.Identifiers.Count == 0)
               return BadRequest();

            try
            {
                var identities = inParameter.Identifiers;
                var primaryIdentity = identities.First();
                var identity = _context.LinkedIdentity.FirstOrDefault(e => e.Identifier.Equals(primaryIdentity.Identifier));

                var playerAccount = new PlayerAccount()
                {
                    Name = inParameter.Username,
                    Priority = inParameter.Priority,
                    RegisteredAt = DateTime.UtcNow
                };

                var userAccount = new UserAccount()
                {
                    Username = inParameter.Username,
                    Priority = inParameter.Priority,
                    CreatedOn = DateTime.UtcNow,
                    GroupId = 0
                };

              
                if (identity == null)
                {
                    var x = _context.LinkedIdentity.Where(e => identities.Select(x => x.Identifier).Contains(e.Identifier)).ToArray();
                    if (x.Length > 0)
                    {
                        identity = x.First();
                        _context.PlayerAccounts.Add(playerAccount);
                        _context.SaveChanges();

                        _context.LinkedIdentity.Add(new LinkedIdentity()
                        {
                            Identifier = primaryIdentity.Identifier,
                            IdentityTypeId = primaryIdentity.IdentitierType,
                            PlayerId = playerAccount.PlayerId,
                            UserId = identity.UserId
                        });
                        _context.SaveChanges();

                        userAccount = _context.UserAccounts.First(e => e.UserId == identity.UserId);
                    }
                    else
                    {
                        _context.PlayerAccounts.Add(playerAccount);
                        _context.UserAccounts.Add(userAccount);
                        _context.SaveChanges();
                        var list = new List<LinkedIdentity>();

                        foreach (var item in identities)
                        {
                            list.Add(new LinkedIdentity()
                            {
                                Identifier = item.Identifier,
                                IdentityTypeId = item.IdentitierType,
                                PlayerId = playerAccount.PlayerId,
                                UserId = userAccount.UserId
                            });
                        }
                        _context.LinkedIdentity.AddRange(list);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    //userAccount = _context.UserAccounts.First(e => e.UserId == primaryIdentity.UserId);
                    //playerAccount = _context.PlayerAccounts.First(e => e.PlayerId == primaryIdentity.PlayerId);
                    return Ok(new EmptyResult());
                }

                var outParameters = new RegistrationPlayerOutParameter()
                {
                    Username = playerAccount.Name,
                    PlayerId = playerAccount.PlayerId,
                    Priority = playerAccount.Priority,
                    UserId = userAccount.UserId
                };
                return Ok(outParameters);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
                throw;
            }
        }
    }
}

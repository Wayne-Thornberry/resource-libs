using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proline.DBAccess.Data;
using Proline.DBAccess.MidLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Proline.DBAccess.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DBController : ControllerBase
    {
        // private ProlineCentralContext _context;

        PlacePlayerDataRequest _inParameters;

        public DBController()
        {
            // _context = context;
        }


        [HttpPost]
        [Route("SaveFile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlacePlayerDataResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveFile(PlacePlayerDataRequest inParameter)
        {

            if (inParameter == null || string.IsNullOrEmpty(inParameter.Data))
                return BadRequest();

            try
            {
                PlacePlayerDataResponse response = null;
                using (var api = new DBAccessApi())
                {
                    response = api.SaveFile(inParameter);
                }


                    _inParameters = inParameter;

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
                throw;
            }
        }

        [HttpPost]
        [Route("LoadFile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPlayerDataInParameters))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoadFile(GetPlayerDataInParameters inParameter)
        {

            if (inParameter == null || inParameter.Id == -1)
                return BadRequest();

            try
            {
                inParameter.Data = _inParameters.Data;

                return Ok(inParameter);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
                throw;
            }
        }
    }
}

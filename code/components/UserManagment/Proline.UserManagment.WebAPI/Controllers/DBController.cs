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

        public DBController()
        {

        }


        [HttpPost]
        [Route("InsertSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InsertSaveResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertSave(InsertSaveRequest inParameter)
        {

            if (inParameter == null || string.IsNullOrEmpty(inParameter.Data))
                return BadRequest();

            try
            {
                InsertSaveResponse response = null;
                using (var api = new DBAccessApi())
                {
                    response = api.SaveFile(inParameter);
                }  
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
                throw;
            }
        }

        [HttpPost]
        [Route("GetSave")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSaveResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSave(GetSaveRequest inParameter)
        {

            if (inParameter == null || string.IsNullOrEmpty(inParameter.Username))
                return BadRequest();

            try
            { 
                GetSaveResponse response = null;
                using (var api = new DBAccessApi())
                {
                    response = api.GetSave(inParameter);
                }  
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
                throw;
            }
        }

        [HttpPost]
        [Route("RegisterPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterPlayerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterPlayer(RegisterPlayerRequest inParameter)
        {

            if (inParameter == null || string.IsNullOrEmpty(inParameter.Name))
                return BadRequest();

            try
            {
                RegisterPlayerResponse response = null;
                using (var api = new DBAccessApi())
                {
                    response = api.RegisterPlayer(inParameter);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
                throw;
            }
        }

        [HttpPost]
        [Route("GetPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPlayerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlayer(GetPlayerRequest inParameter)
        {

            if (inParameter == null || string.IsNullOrEmpty(inParameter.Username))
                return BadRequest();

            try
            {
                GetPlayerResponse response = null;
                using (var api = new DBAccessApi())
                {
                    response = api.GetPlayer(inParameter);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.ToString());
                throw;
            }
        }
    }
}

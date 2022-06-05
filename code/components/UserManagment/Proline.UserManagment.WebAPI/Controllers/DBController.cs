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

            if (inParameter == null || inParameter.Id < -1)
                return BadRequest();

            try
            { 
                GetSaveResponse response = null;
                using (var api = new DBAccessApi())
                {
                    response = api.GetSave(inParameter);
                }  
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Proline.Online.Data;
using Proline.CentralEngine.DBApi.Contexts; 
using Proline.CentralEngine.MidLayer;

namespace Proline.CentralEngine.Web.Service.Controllers
{
    [RoutePrefix("User")]
    public class UserController : ApiController
    {

        [HttpPost]
        [Route("PutUser")]
        [ResponseType(typeof(ReturnCode))]
        public IHttpActionResult PutUser(UserInParameter username)
        { 
            var rc = Engine.RegisterUser(username.Username);
            if (rc != ReturnCode.Success)
                return InternalServerError();
            return Ok(rc);
        }

        [HttpPost]
        [Route("DoTest/{id:long}")]
        [ResponseType(typeof(PlayerDetailsOutParameter))]
        public IHttpActionResult GetUser(long id)
        {
            var rc = ReturnCode.Success; //Engine.GetUsers(out var users);
            Engine.GetPlayerDetails(id, out var x);
            if (rc != ReturnCode.Success)
                return InternalServerError();
            return Ok(x);
        }


        [HttpPost]
        [Route("DoTest2")]
        [ResponseType(typeof(PlayerDetailsOutParameter))]
        public IHttpActionResult GetUser2(RegisterInstanceInParameter inParameter)
        {
            var rc = ReturnCode.Success; //Engine.GetUsers(out var users);
            Engine.GetPlayerDetails(1, out var x);
            if (rc != ReturnCode.Success)
                return InternalServerError();
            return Ok(x);
        }
    }
}
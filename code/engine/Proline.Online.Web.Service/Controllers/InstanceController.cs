using Proline.Online.Data;
using Proline.CentralEngine.MidLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Proline.CentralEngine.Web.Service.Controllers
{
    [RoutePrefix("Instance")]
    public class InstanceController : ApiController
    {

        [HttpPost]
        [Route("RegisterInstance")]
        [ResponseType(typeof(PlayerDetailsOutParameter))]
        public IHttpActionResult RegisterInstance(RegisterInstanceInParameter inParameters)
        {
            var rc = Engine.RegisterInstance(inParameters);
            if (rc != ReturnCode.Success)
                return InternalServerError();
            return Ok(rc);
        }



    }
}
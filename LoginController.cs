using Lms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lms.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("AddLoginRecord")]
        public string AddLoginRecord([FromBody] LoginModel LoginModel)
        {
            return LoginModel.AddLoginRecord();
        }
    }
}

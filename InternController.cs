using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS2.Models;  // Ensure the namespace is correct

namespace LMS2.Controllers
{
    public class InternController : ApiController
    {
        [HttpPost]
        [Route("GetInternsByDesignation")]
        public IHttpActionResult GetInternsByDesignation([FromBody] InternModel internModel)
        {
            if (internModel == null)
            {
                // Return a 400 Bad Request if the request body is empty
                return BadRequest("Invalid request data.");
            }

            try
            {
                // Call the method to fetch interns by designation
                List<InternDetails> interns = internModel.GetInternsByDesignation();

                // Return the list of interns with a 200 OK status
                return Ok(interns);
            }
            catch (ArgumentException ex)
            {
                // Handle validation errors and return a 400 Bad Request
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions and return a 500 Internal Server Error
                return InternalServerError(ex);
            }
        }
    }
}

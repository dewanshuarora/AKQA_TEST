using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AKQA.Extensions;

namespace AKQA.Controllers
{
    public class NumberToWordConvertorController : ApiController
    {
        [Route("api/{value}/convert")] //USING ATTRIBUTE ROUTING FOR GET METHOD
        public string Get(string value)
        {
            return value.ConvertToWords();
        }

        public IHttpActionResult Post([FromBody]Request value) //USING DEFAULT ROUTING FOR POST METHOD, USING POST METHOD IN THE JAVASCRIPT CODE
        {
            return Ok(value.value.ConvertToWords());
        }

    }

    public class Request
    {
        public string value { get; set; }
    }
}
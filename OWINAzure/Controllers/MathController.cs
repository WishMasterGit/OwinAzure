using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using WebApi.OutputCache.V2.TimeAttributes;

namespace OWINAzure.Controllers
{

    public class MathController : ApiController
    {
        [HttpGet]
        [Route("add")]
        [CacheOutputUntilToday(00, 00)]
        public IHttpActionResult Add(double x, double y)
        {
            return Ok(x + y);
        }

        [HttpPost]
        [Route("substract")]
        public IHttpActionResult Substract([FromBody]Numbers numbers)
        {
            return Ok(numbers.x - numbers.y);
        }

        [HttpPost]
        [Route("divide")]
        public IHttpActionResult Divide(JObject numbers)
        {
            try
            {
                return Ok((double)numbers["x"] / (double)numbers["y"]);
            }
            catch (Exception e)
            {
                return this.BadRequest("division by 0");
            }
        }

        [HttpPost]
        [Route("multiply")]
        public IHttpActionResult Multiply(JObject numbers)
        {
            var mult = (double) numbers["x"] - (double) numbers["y"];
            var result = new JObject
            {
                ["Result"] = mult
            };
            return Ok(result);
        }

    }
}
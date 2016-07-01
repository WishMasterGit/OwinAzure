using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using OWINAzure.Services;

namespace OWINAzure.Controllers
{
    
    public class PuzzleController : ApiController
    {
        protected readonly PuzzleService Service = new PuzzleService();

        [HttpGet]
        [Route("puzzle/getAll")]
        [ResponseType(typeof(List<PuzzleEntity>))]
        [Authorize(Roles = "admin")]
        public IHttpActionResult GetAll()
        {
            return Ok(Service.GetList());
        }

        [HttpGet]
        [Route("puzzle/get")]
        [ResponseType(typeof(PuzzleEntity))]
        public IHttpActionResult Get(string id)
        {
            var result = Service.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("puzzle/update")]
        [ResponseType(typeof(PuzzleEntity))]
        public IHttpActionResult Update([FromBody] PuzzleEntity entity)
        {
            var result = Service.Update(entity);
            return Ok(result);
        }

        [HttpPost]
        [Route("puzzle/create")]
        [ResponseType(typeof(PuzzleEntity))]
        public IHttpActionResult Create([FromBody] PuzzleEntity entity)
        {
            var result = Service.Create(entity);
            return Ok(result);
        }

        [HttpGet]
        [Route("puzzle/delete")]
        [ResponseType(typeof(PuzzleEntity))]
        public IHttpActionResult Delete(string id)
        {
            var entity = Service.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            var youtubeEntity = Service.Delete(entity);
            return Ok(youtubeEntity);
        }
    }
}
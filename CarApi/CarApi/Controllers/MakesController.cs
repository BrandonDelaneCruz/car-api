using CarApi.Data;
using CarApi.Data.Entities;
using CarApi.Requests;
using CarApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarApi.Controllers
{
    [ApiController]
    public class MakesController : ControllerBase
    {
        private readonly DataContext _context;

        public MakesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("api/makes")]
        public ActionResult<List<MakeResponse>> GetAll()
        {
            List<MakeResponse> makesToReturn = _context.Makes
                .Select(x => new MakeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList();

            return Ok(makesToReturn);
        }

        [HttpGet("api/makes/{makeId}")]
        public ActionResult<MakeResponse> Get([FromRoute] int makeId)
        {
            Make? makeFromDatabase = _context.Makes
                .FirstOrDefault(x => x.Id == makeId);

            if (makeFromDatabase == null)
            {
                return NotFound();
            }

            MakeResponse makeToReturn = new MakeResponse
            {
                Id = makeFromDatabase.Id,
                Name = makeFromDatabase.Name,
            };

            return Ok(makeToReturn);
        }

        [HttpPost("api/makes")]
        public ActionResult<MakeResponse> Create([FromBody] MakeCreateRequest request)
        {
            Make makeToCreate = new Make
            {
                Name = request.Name,
            };

            _context.Makes.Add(makeToCreate);
            _context.SaveChanges();

            MakeResponse makeToReturn = new MakeResponse
            {
                Id = makeToCreate.Id,
                Name = makeToCreate.Name
            };

            return Created($"api/makes/{makeToReturn.Id}", makeToReturn);
        }

        [HttpPut("api/makes/{makeId}")]
        public ActionResult<MakeResponse> Update(
            [FromRoute] int makeId,
            [FromBody] MakeUpdateRequest request)
        {
            Make? makeFromDatabase = _context.Makes
                .FirstOrDefault(x => x.Id == makeId);

            if (makeFromDatabase == null)
            {
                return NotFound();
            }

            makeFromDatabase.Name = request.Name;

            _context.SaveChanges();

            MakeResponse makeToReturn = new MakeResponse
            {
                Id = makeFromDatabase.Id,
                Name = makeFromDatabase.Name,
            };

            return Ok(makeToReturn);
        }

        [HttpDelete("api/makes/{makeId}")]
        public ActionResult Delete(int makeId)
        {
            Make? makeToDelete = _context.Makes
                .FirstOrDefault(x => x.Id == makeId);

            if (makeToDelete == null)
            {
                return NotFound();
            }

            _context.Makes.Remove(makeToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}

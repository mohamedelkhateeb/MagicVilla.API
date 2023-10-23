using FirstApp.API.Data;
using FirstApp.API.Models;
using FirstApp.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.API.Controllers
{
    [Route("api/VillaApi")]
    //[ApiController]
    public class VillaApiController : ControllerBase
    {
        //1-Get All
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult <IEnumerable<VillaDTO>> GetVillas()
        {
            //For Status code
            return Ok(VillaStore.villaDTOs);
        }
        //2-Get By Id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id")]
        public ActionResult <VillaDTO> GetVilla(int id )
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = VillaStore.villaDTOs.FirstOrDefault(v => v.Id == id);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        //3-Create (post) Item
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("AddVilla")]
        public ActionResult <VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            //Custom Validation
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}


            //Check if The name is exist
            if(VillaStore.villaDTOs.FirstOrDefault(x => x.Name.ToLower() == 
            villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa Name Already Exists!");
                return BadRequest(ModelState);
            }


            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            if(villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDTO.Id = VillaStore.villaDTOs.OrderByDescending(i => i.Id).FirstOrDefault().Id + 1;
            VillaStore.villaDTOs.Add(villaDTO);
            return Ok(villaDTO);
        }
        //Delete Item
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("id")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = VillaStore.villaDTOs.FirstOrDefault(v => v.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            VillaStore.villaDTOs.Remove(result);

            return NoContent();
        }
        //3-Create (post) Item
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("UpdateVilla")]
        public IActionResult Updateilla(int id , [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest(villaDTO);
            }

            var result = VillaStore.villaDTOs.FirstOrDefault(v => v.Id == id);
            result.Name = villaDTO.Name;
            return NoContent();
        }
    }
}

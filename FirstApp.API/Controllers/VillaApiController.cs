using FirstApp.API.Data;
using FirstApp.API.Models;
using FirstApp.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApp.API.Controllers
{
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.villaDTOs;
        }
        [HttpGet("id")]
        public VillaDTO GetVilla(int id )
        {
            return VillaStore.villaDTOs.FirstOrDefault(v => v.Id == id);
        }
    }
}

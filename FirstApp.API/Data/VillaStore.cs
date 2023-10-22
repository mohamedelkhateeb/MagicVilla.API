using FirstApp.API.Models.DTO;

namespace FirstApp.API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaDTOs = new List<VillaDTO>
            {
                new VillaDTO {Id = 1 , Name = "Pool View"},
                new VillaDTO {Id = 2 , Name = "Beach View"},
            };
    }
}

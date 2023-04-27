using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_api.Models;

namespace prueba_tecnica_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CiudadPaisController : Controller
    {
      
        public CiudadPaisController()
        {
           
        }

        [HttpGet("{busqueda}")]
        public async Task<ActionResult<IList<GeocoderModel>>> BuscarCiudades(string busqueda)
        {
            CiudadesPaises _ciudadesPaises = new CiudadesPaises();
            IList<GeocoderModel> results = await _ciudadesPaises.BuscarCiudades(busqueda);

            return Ok(results);
        }
        [HttpGet("array/{busqueda}")]
        public async Task<ActionResult<string[]>> BuscarCiudadesArreglo(string busqueda)
        {
            CiudadesPaises _ciudadesPaises = new CiudadesPaises();
            IList<GeocoderModel> results = await _ciudadesPaises.BuscarCiudades(busqueda);

            return Ok(results.Select(s=> s.Formatted).ToArray());
        }
    }
}

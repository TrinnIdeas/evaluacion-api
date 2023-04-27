using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_api.Models;
using System.Net.Mail;

namespace prueba_tecnica_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : Controller
    {
      

        private readonly IConfiguration _config;

        public FormController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm(FormModel formData)
        {
            

            try
            {
                Correo oCorreo = new Correo();
                // Envía el correo electrónico
                await oCorreo.Enviar(formData, _config);

                return Ok();
            }
            catch (SmtpException)
            {
                return StatusCode(500);
            }
        }
    }
}

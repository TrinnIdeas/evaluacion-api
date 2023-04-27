using prueba_tecnica_api.Models;
using System.Net.Mail;

namespace prueba_tecnica_api
{
    public class Correo
    {
        string plantilla = @"
  <div class='recuadro' style='background: linear-gradient(to right, #2FD303, #35EE03); height: 100px; position: relative; border-radius: 10px;'>
    <p class='texto' style='color: white; font-size: 40px; position: absolute; top: 20%; left: 50%; transform: translate(-50%, -50%);'>Green Leaves</p>
  </div>
  <p>Estimado ##nombre## </p><br>
  Hemos recibido sus datos y nos pondremos en contacto con usted en la brevedad posible. Enviaremos un correo con información a su cuenta:<b> ##email##</b>
  <br><br>
  <p style='text-align: right;'><b>Atte.</b></p>
  <p style='text-align: right; color:green'><b>Green Leaves</b></p>
  <p style='text-align: right;'><b>##ciudad## a ##fecha##</b></p>

  <style>
    .recuadro::after {
      content: '';
      position: absolute;
      right: 0;
      top: 0;
      bottom: 0;
      width: 100px;
      background-image: url('https://images.vexels.com/media/users/3/207136/isolated/lists/dc6980a67acd5e2d4a13bc446e9e3378-gran-icono-de-hoja-verde.png');
      background-size: contain;
      background-repeat: no-repeat;
    }
  </style>";


        public async Task<string> Enviar(FormModel formData, IConfiguration _config)
        {
            try
            {
               
                // Construye el mensaje de correo electrónico
                var body = $"Nombre: {formData.Nombre}\n" +
                           $"Email: {formData.Email}\n" +
                           $"Teléfono: {formData.Telefono}\n" +
                           $"Fecha: {formData.Fecha}\n" +
                           $"Ciudad y Estado: {formData.CiudadEstado}";

                var message = new MailMessage();
                message.To.Add(formData.Email);
                message.Subject = "Formulario de contacto";
                plantilla = plantilla.Replace("##nombre##", formData.Nombre);
                plantilla = plantilla.Replace("##email##", formData.Email);
                plantilla = plantilla.Replace("##ciudad##", formData.CiudadEstado);
                plantilla = plantilla.Replace("##fecha##", formData.Fecha.ToString("dd-MMM-yyyy"));

                message.Body = plantilla;
                message.From = new MailAddress(_config["EmailSettings:SmtpUsername"]) ;
                message.IsBodyHtml = true;

                // Configura la conexión de correo electrónico
                var smtpClient = new SmtpClient();
                smtpClient.Host = _config["EmailSettings:SmtpServer"];
                smtpClient.Port = int.Parse(_config["EmailSettings:SmtpPort"]);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(
                    _config["EmailSettings:SmtpUsername"],
                    _config["EmailSettings:SmtpPassword"]);
                

                try
                {
                    // Envía el correo electrónico
                    await smtpClient.SendMailAsync(message);
                    
                }
                catch (SmtpException)
                {
                    return "Ocurrió un error inesperado, revisar la bitacora de errores";
                }

                return "";
            }
            catch (Exception)
            {

                return "Ocurrió un error inesperado, revisar la bitacora de errores";

            }

        }
    }
}

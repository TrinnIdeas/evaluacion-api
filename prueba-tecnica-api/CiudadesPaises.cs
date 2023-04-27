using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using prueba_tecnica_api.Models;

namespace prueba_tecnica_api
{
    public class CiudadesPaises
    {
        
        public async Task<IList<GeocoderModel>> BuscarCiudades(string busqueda)
        {
           
            using (var client = new HttpClient())
            {
                string url = $"https://api.opencagedata.com/geocode/v1/json?q={busqueda}&limit=10&key={apiKey}&pretty=1";
                HttpResponseMessage response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                JObject result = JObject.Parse(json);
                IList<GeocoderModel> results = JsonConvert.DeserializeObject<IList<GeocoderModel>>(result["results"].ToString());             



                results = results.Zip(Enumerable.Range(0, results.Count), (r, i) => { r.id = i; return r; }).ToList();

                return results;
            }
        }

        public async Task<List<string>> BuscarCiudadesMexico(string busqueda)
        {
            List<string> resultCiudades =new List<string>();
            // Configura la clave de API y la URL base de OpenCageData
            string apiKey = "3f6a309bff1a4f8ebe4ed55f0ee51c02";
            string baseUrl = "https://api.opencagedata.com/geocode/v1/json?q=";

            // Obtiene una lista de todos los estados de México (puedes usar tu propia lista)
            string[] estados = new string[] { "Aguascalientes", "California", "California Sur", "Campeche", "Chiapas", "Chihuahua", "Coahuila", "Colima", "Durango", "Estado de México", "Guanajuato", "Guerrero", "Hidalgo", "Jalisco", "Michoacan", "Morelos", "Nayarit", "Nuevo Leon", "Oaxaca", "Puebla", "Queretaro", "Quintana Roo", "San Luis Potosi", "Sinaloa", "Sonora", "Tabasco", "Tamaulipas", "Tlaxcala", "Veracruz", "Yucatán", "Zacatecas" };

            // Crea un cliente HttpClient
            using var httpClient = new HttpClient();

            // Para cada estado, haz una solicitud a la API de OpenCageData para obtener las ciudades
            foreach (var estado in estados)
            {
                var url = $"{baseUrl}{estado}&key={apiKey}&no_annotations=1&pretty=1";
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(jsonString);

                    // Extrae las ciudades de la respuesta JSON
                    var results = json["results"].Children();
                    foreach (var result in results)
                    {
                        var components = result["components"];
                        if (components["city"] != null)
                        {
                            var city = components["city"].ToString();
                         

                            resultCiudades.Add(string.Format("{0},{1}",city,estado));
                        }
                    }
                }
            }

            return resultCiudades;
        }
    }

}

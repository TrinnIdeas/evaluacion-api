namespace prueba_tecnica_api.Models
{
    public class GeocoderModel
    {
        string _Formatted { get; set; }
        public int id { get; set; }
        public string Formatted { get { return _Formatted.Replace("México", "Mexico"); } set { _Formatted = value; } }
        public string City { get; set; }
        public string Town { get; set; }

        public string CityCountry { get { return String.Format("{0},{1},{2}",City ,Town, Country); } }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeocoderModel(string formatted, string city, string town, string country, double latitude, double longitude)
        {
            Formatted = formatted;
            City = city;
            Town = town;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

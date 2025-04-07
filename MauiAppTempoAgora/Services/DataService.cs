using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    internal class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo? t = null;

            string chave = "fe2fae345f38121fe0f237ca261c0471";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    var rascunho = JObject.Parse(json);

                    var sunriseUnix = (double)rascunho["sys"]["sunrise"];
                    var sunsetUnix = (double)rascunho["sys"]["sunset"];

                    DateTimeOffset sunrise = DateTimeOffset.FromUnixTimeSeconds((long)sunriseUnix).ToLocalTime();
                    DateTimeOffset sunset = DateTimeOffset.FromUnixTimeSeconds((long)sunsetUnix).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString("dd/MM/yyyy HH:mm:ss"),
                        sunset = sunset.ToString("dd/MM/yyyy HH:mm:ss"),
                    };
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new HttpRequestException("Erro de conexão ou serviço indisponível.");
                }
            }

            return t;
        }
    }
}

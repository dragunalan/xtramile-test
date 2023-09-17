using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using XtramileWeather.Model;
using Microsoft.Extensions.Logging;
using XtramileWeather.Controllers;
using System.Threading.Tasks;
using XtramileWeather.Helper;

namespace XtramileWeather.Services
{
    public class XtramileWeatherService : IXtramileWeatherService
    {
        private readonly ILogger<XtramileWeatherService> _logger;

        public XtramileWeatherService(ILogger<XtramileWeatherService> logger)
        {
            _logger = logger;
        }

        public async Task<WeatherResponse> GetWeather(string city)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={city}&APPID=a2cf056a22ed3a7437cf16b4641aa38c"),
                    };

                    using (var response = await httpClient.SendAsync(request))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(apiResponse);

                        weatherResponse.Time = DateTime.UtcNow.AddSeconds(weatherResponse.Timezone);
                        weatherResponse.TemperatureConverter();
                        return weatherResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new WeatherResponse() { ErrorMessage = ex.Message };
            }
        }
    }
}

// a RESTFul service which reports weather information for all cities or a specified city, or reports cities for a weather warning status
// and supports weather updates for specified cities
// uses attribute based routing
// supplies Swagger metadata /swagger/docs/v1 
// and /swagger to see swagger UI test page if specified in swaggerconfig.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Weather.Models;

namespace Weather.Controllers
{
    [RoutePrefix("weather")]
    public class WeatherController : ApiController
    {
        /*
        * GET /weather/all                  get weather information for all cities       RetrieveAllWeatherInformation()
        * GET /weather/city/Dublin          get weather information for Dublin           GetWeatherInformationForCity(city)
        * GET /weather/cities/warning/true  get cities which have a weather warning      GetCityNameForWarningStatus(true)
        * PUT /weather/dublin               update weather information for city          PutUpdateWeatherInformation(String city, WeatherInformation weatherInfo)
         */

        private static List<WeatherInformation> weather = new List<WeatherInformation>()
        {
            new WeatherInformation { City = "Dublin", Temperature = 14, WindSpeed = 15.7, Conditions = "Cloudy", WeatherWarning = false },
            new WeatherInformation { City = "Barcelona", Temperature = 29, WindSpeed = 5, Conditions = "Sunny", WeatherWarning = false },
            new WeatherInformation { City = "New York", Temperature = 3, WindSpeed = 30, Conditions = "Rain", WeatherWarning = true },
            new WeatherInformation { City = "London", Temperature = 13, WindSpeed = 18, Conditions = "Fog", WeatherWarning = false }
        };


        // GET /weather/all
        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllWeatherInformation()
        {
            lock (weather)
            {
                return Ok(weather.OrderBy(w => w.City).ToList());
            }     
        }


        // GET /weather/city/dublin
        [Route("city/{city:alpha}")]
        public IHttpActionResult GetWeatherInformationForCity(String city)
        {
            lock (weather)
            {
                var cityWeather = weather.FirstOrDefault(w => w.City.ToUpper() == city.ToUpper());
                if (cityWeather == null)
                {
                    return NotFound();
                }
                return Ok(cityWeather);
            }
        }


        // GET /weather/warning/true
        [Route("warning/{warning:bool}")]
        [HttpGet]
        public IHttpActionResult GetCityNamesWithWeatherWarning(bool warning)
        {
            lock(weather)
            {
                var cities = weather.Where(w => w.WeatherWarning == warning).Select(w => w.City).ToList();
                return Ok(cities);
            }
        }


        // PUT /weather/Dublin
        [Route("{city:alpha}")]
        [HttpPut]
        public IHttpActionResult PutUpdateWeatherInformation(String city, WeatherInformation weatherInfo)
        {
            if (ModelState.IsValid)
            {
                if(city == weatherInfo.City)
                {
                    lock(weather)
                    {
                        int index = weather.FindIndex(w => w.City.ToUpper() == weatherInfo.City.ToUpper());
                        if (index == -1)
                        {
                            return NotFound();
                        }
                        else
                        {
                            weather.RemoveAt(index);
                            weather.Add(weatherInfo);
                            return Ok();
                        }
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

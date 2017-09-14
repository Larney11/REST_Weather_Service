using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weather.Models
{
    public class WeatherInformation
    {
        public String City { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public String Conditions { get; set; }
        public bool WeatherWarning { get; set; }
    }
}
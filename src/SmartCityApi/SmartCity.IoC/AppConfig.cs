using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.IoC
{
    public class AppConfig
    {
        private IConfiguration _configuration;

        public AppConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string _smartCityConnString { get; set; }
        public string SmartCityConnString
        {
            get
            {
                if (string.IsNullOrEmpty(_smartCityConnString))
                    _smartCityConnString = _configuration.GetConnectionString("SmartCityContext");
                return _smartCityConnString;
            }
        }
        
    }
    
}

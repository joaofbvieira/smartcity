using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Domain
{
    public class ParametrosEmail
    {
        public String PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public String UsernameEmail { get; set; }

        public String UsernamePassword { get; set; }

        public String FromEmail { get; set; }

    }
}

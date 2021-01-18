using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AppSettingsOptions
    {
        public string connectionString { get; set; }
        public string thumbKey { get; set; }
        public string storageName { get; set; }
        public string VSDCAddress { get; set; }
    }
}

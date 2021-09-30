using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.WebAPI.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; } // "Pass@word1"; dotnet user-secrets init   user secrets set MongoDbSettings:Password Pass@word1

        public string ConnectionString 
        {
            get
            {
                return $"mongodb://{Host}:{Port}";  // return $"mongodb://{User}:{Password}@{Host}:{Port}";
            } 
        }



    }
}

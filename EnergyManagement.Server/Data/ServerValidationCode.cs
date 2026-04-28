using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyManagement.Server.Data
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public sealed class ServerValidationCodeAttribute:Attribute
    {
        public string Path { get; }
        public ServerValidationCodeAttribute(string path)
        {
            Path = path;
        }
        
    }
}
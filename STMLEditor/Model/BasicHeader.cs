using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace STMLEditor.Model
{
    public class BasicHeader
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }

        [JsonConstructor]
        public BasicHeader(string name = "New STML Element", string id = "", string description = "")
        { 
            Name = name;
            ID = id == "" ? Guid.NewGuid().ToString() : id;
            Description = description;
        }
    }
}

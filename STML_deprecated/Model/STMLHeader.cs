using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace STML.Model
{
    public sealed class STMLHeader
    {
        public string Name { get; set; }
        public string ID { get; }
        public string Comments { get; set; }

        public STMLHeader(string name = "New STML Element", string id = "", string comments = "")
        { 
            Name = name;
            ID = id is null or "" ? Guid.NewGuid().ToString() : id;
            Comments = comments;
        }
    }
}

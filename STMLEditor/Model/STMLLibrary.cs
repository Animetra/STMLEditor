using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace STMLEditor.Model
{
    internal class STMLLibrary : STMLElement
    {
        [JsonConstructor]
        public STMLLibrary()
        {
            Header = new BasicHeader("New Library");
        }

        public override void AddChild(out STMLElement child)
        {
            child = new STMLDocument();
            Children.Add(child);
        }
    }
}

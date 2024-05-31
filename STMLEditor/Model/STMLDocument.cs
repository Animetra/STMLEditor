using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace STMLEditor.Model
{
    public class STMLDocument : STMLElement
    {
        [JsonConstructor]
        public STMLDocument() 
        {
            Header = new BasicHeader("New Document");
        }

        public override void AddChild(out STMLElement child)
        {
            child = new STMLSection();
            Children.Add(child);
        }
    }
}

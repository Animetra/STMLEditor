using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace STML.Model
{
    public class STMLDocument : STMLElement
    {
        public STMLDocument(STMLLibrary parent) : base(parent)
        {
            Header = new STMLHeader("New Document");
        }

        public override STMLElement? AddChild()
        {
            throw new InvalidOperationException($"Use AddDictionary or AddScript");
        }
        public STMLElement AddDictionary()
        {
            STMLDictionary child = new STMLDictionary(this);
            Children.Add(child);

            return child;
        }
        public STMLElement AddScript()
        {
            STMLScript child = new STMLScript(this);
            Children.Add(child);

            return child;
        }
    }
}

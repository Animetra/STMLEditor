using System;

namespace STML.Model
{
    public class STMLDocument : STMLElement
    {
        public STMLDocument() : base(null)
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

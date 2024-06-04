using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STML.Model
{
    public class STMLScript : STMLSection
    {
        public STMLScript(STMLDocument parent) : base(parent)
        {
            Header = new STMLHeader("New Script");
        }

        public override STMLElement AddChild()
        {
            STMLExpression child = new STMLExpression(this);
            Children.Add(child);

            return child;
        }
    }
}

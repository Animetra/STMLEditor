using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STML.Model
{
    public class STMLDictionary : STMLSection
    {
        public STMLDictionary(STMLDocument parent) : base(parent)
        {
            Header = new STMLHeader("New Dictionary");
        }

        public override STMLElement AddChild()
        {
            STMLTerm child = new STMLTerm(this);
            Children.Add(child);

            return child;
        }
    }
}

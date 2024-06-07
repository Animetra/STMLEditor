using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STML.Model
{
    public abstract class STMLSection : STMLElement
    {
        public STMLSection(STMLDocument parent) : base(parent)
        {
            Header = new STMLHeader("New Section");
        }
    }
}

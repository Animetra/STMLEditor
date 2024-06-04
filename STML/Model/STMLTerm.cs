using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STML.Model
{
    public class STMLTerm : STMLText
    {
        public STMLTerm(STMLDictionary parent) : base(parent)
        {
            Header = new STMLHeader("New Term");
        }
    }
}

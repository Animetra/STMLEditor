using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STML.Model
{
    public class STMLExpression : STMLText
    {
        public string Narrator { get; set; } = "";
        public string Style { get; set; } = "";
        public STMLExpression(STMLSection parent) : base(parent)
        {
            Header = new STMLHeader("New Expression");
        }
    }
}

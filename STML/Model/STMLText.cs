using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STML.Model
{
    public abstract class STMLText : STMLElement
    {
        public string Content { get; set; } = "";
        public STMLText(STMLSection parent) : base(parent)
        {
            Header.Name = "New STMLText";
        }
    }
}

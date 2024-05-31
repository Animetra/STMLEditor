using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMLEditor.Model
{
    public abstract class STMLText : STMLElement
    {
        public string Narrator { get; set; }
        public string Style { get; set; }
        public string Content { get; set; }
    }
}

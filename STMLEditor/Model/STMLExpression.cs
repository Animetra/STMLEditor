using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMLEditor.Model
{
    public class STMLExpression : STMLText
    {
        public bool IsConversation { get; set; }
        public STMLExpression()
        {
            Header = new BasicHeader("New Expression");
        }
    }
}

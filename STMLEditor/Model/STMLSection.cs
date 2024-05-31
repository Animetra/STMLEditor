using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMLEditor.Model
{
    public class STMLSection : STMLElement
    {
        public bool IsConversation { get; set; }

        public STMLSection() 
        {
            Header = new BasicHeader("New Section");
        }

        public override void AddChild(out STMLElement child)
        {
            child = new STMLExpression();
            Children.Add(child);
        }
    }
}

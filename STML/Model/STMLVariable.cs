using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace STML.Model
{
    public class STMLVariable
    {
        private STMLLibrary _parent;
        public string Name { get; set; }
        public STMLTerm Term {  get; set; }

        public STMLVariable(STMLLibrary parent)
        {
            _parent = parent;
        }

        public void SetTerm(string documentID, string sectionID, string termID)
        {
            Term = (STMLTerm)_parent.Children.First(x => x.Header.ID == documentID).Children.First(x => x.Header.ID == sectionID).Children.First(x => x.Header.ID == termID);
        }
    }
}

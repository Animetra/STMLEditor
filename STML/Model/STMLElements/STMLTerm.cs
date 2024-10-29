using System;

namespace STML.Model
{
    public sealed class STMLTerm :
        STMLElement,
        IUsableInEditor
    {
        private string _referenceName;
        public string ReferenceName
        {
            get => _referenceName;
            set { _referenceName = AddReferenceToLibrary(value); }
        }

        public STMLString Content { get; set; } = new STMLString(null);

        public STMLTerm(STMLDictionary parent) : base(parent)
        {
            _referenceName = "";
            Header = new STMLHeader("New Term");
        }

        public override STMLElement? AddChild()
        {
            throw new InvalidOperationException("Terms can't have children");
        }

        private string AddReferenceToLibrary(string name)
        {
            return ParentLibrary.AddReference(name, this);
        }
    }
}
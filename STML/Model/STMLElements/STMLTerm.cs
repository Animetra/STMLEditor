using System;
using System.Collections.Generic;

namespace STML.Model
{
    public sealed class STMLTerm :
        STMLElement
    {
        private string _referenceName;
        public string ReferenceName
        {
            get => _referenceName;
            set { _referenceName = AddReferenceToProject(value); }
        }

        public STMLString ActiveLanguageContent => Content.GetContentOfActiveLanguage(ParentProject);
        public Dictionary<string, STMLString> Content { get; set; } = new Dictionary<string, STMLString>();

        public STMLTerm(STMLDictionary parent) : base(parent)
        {
            _referenceName = "";
            Header = new STMLHeader("New Term");
        }

        public override STMLElement? AddChild()
        {
            throw new InvalidOperationException("Terms can't have children");
        }

        private string AddReferenceToProject(string name)
        {
            return ParentProject.AddReference(name, this);
        }
    }
}
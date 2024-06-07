using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace STML.Model
{
    public class STMLString : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected string _plain;
        public virtual string Plain
        {
            get => _plain;
            set { _plain = value; }
        }

        public STMLString(string text = "")
        {
            _plain = text;
        }

        public static implicit operator string(STMLString input) { return input.Plain; }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

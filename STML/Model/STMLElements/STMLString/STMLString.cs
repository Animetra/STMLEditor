using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace STML.Model
{
    public class STMLString : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string? _plain;
        public string? Plain
        {
            get => _plain;
            set { _plain = value; OnPropertyChanged(); }
        }

        public STMLString(string? text)
        {
            _plain = text;
        }

        public static implicit operator string(STMLString input) { return input?.Plain ?? "{Error: STMLString has no content.}"; }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVMRecipe
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string forename = string.Empty;
        public string Forename
        {
            get => forename;
            set
            {
                if (forename != value)
                {
                    forename = value;
                    OnPropertyChanged();
                }
            }
        }
        private string surname = string.Empty;
        public string Surname
        {
            get => surname;
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ResetCommand { get; private set; }

        public MainViewModel()
        {
            ResetCommand = new Command(Reset);
        }

        public void Reset()
        {
            Forename = string.Empty;
            Surname = string.Empty;
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MVVMRecipe;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private string _forename = string.Empty;

    private Command? _resetCommand;

    private string _surname = string.Empty;

    public string Forename
    {
        get => _forename;
        set => SetField(ref _forename, value);
    }

    public string Surname
    {
        get => _surname;
        set => SetField(ref _surname, value);
    }

    public ICommand ResetCommand => _resetCommand ??= new Command(Reset);

    private void Reset()
    {
        Forename = string.Empty;
        Surname = string.Empty;
    }

    #region INotifyPropertyChanged implementation

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}
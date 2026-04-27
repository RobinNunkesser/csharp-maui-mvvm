using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVMRecipe.ViewModels;

public partial class TodoItem : ObservableObject
{
    public TodoItem(string title, bool isCompleted = false)
    {
        Title = title;
        IsCompleted = isCompleted;
    }

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private bool isCompleted;
}

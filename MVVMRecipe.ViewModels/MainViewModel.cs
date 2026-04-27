using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MVVMRecipe.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        Todos.CollectionChanged += OnTodosCollectionChanged;

        AddSampleTodo("Compiled Bindings prüfen", false);
        AddSampleTodo("RelayCommand einsetzen", true);
        AddSampleTodo("MSTest ergänzen", false);

        UpdateDerivedState();
    }

    [ObservableProperty]
    private ObservableCollection<TodoItem> todos = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddTodoCommand))]
    private string newTodoTitle = string.Empty;

    public int RemainingCount => Todos.Count(todo => !todo.IsCompleted);

    public int CompletedCount => Todos.Count(todo => todo.IsCompleted);

    [RelayCommand(CanExecute = nameof(CanAddTodo))]
    private void AddTodo()
    {
        var title = NewTodoTitle.Trim();
        if (title.Length == 0)
        {
            return;
        }

        Todos.Add(new TodoItem(title));
        NewTodoTitle = string.Empty;
        UpdateDerivedState();
    }

    [RelayCommand(CanExecute = nameof(CanClearCompleted))]
    private void ClearCompleted()
    {
        for (var index = Todos.Count - 1; index >= 0; index--)
        {
            if (Todos[index].IsCompleted)
            {
                Todos.RemoveAt(index);
            }
        }

        UpdateDerivedState();
    }

    private bool CanAddTodo()
    {
        return !string.IsNullOrWhiteSpace(NewTodoTitle);
    }

    private bool CanClearCompleted()
    {
        return Todos.Any(todo => todo.IsCompleted);
    }

    private void AddSampleTodo(string title, bool isCompleted)
    {
        Todos.Add(new TodoItem(title, isCompleted));
    }

    private void OnTodosCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (var item in e.OldItems.OfType<TodoItem>())
            {
                item.PropertyChanged -= OnTodoItemPropertyChanged;
            }
        }

        if (e.NewItems is not null)
        {
            foreach (var item in e.NewItems.OfType<TodoItem>())
            {
                item.PropertyChanged += OnTodoItemPropertyChanged;
            }
        }

        UpdateDerivedState();
    }

    private void OnTodoItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TodoItem.IsCompleted))
        {
            UpdateDerivedState();
        }
    }

    private void UpdateDerivedState()
    {
        OnPropertyChanged(nameof(RemainingCount));
        OnPropertyChanged(nameof(CompletedCount));
        ClearCompletedCommand.NotifyCanExecuteChanged();
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVVMRecipe.ViewModels;

namespace MVVMRecipe.Tests;

[TestClass]
public class MainViewModelTests
{
    [TestMethod]
    public void Constructor_SeedsThreeTodos()
    {
        var viewModel = new MainViewModel();

        Assert.AreEqual(3, viewModel.Todos.Count);
        Assert.AreEqual(2, viewModel.RemainingCount);
        Assert.AreEqual(1, viewModel.CompletedCount);
    }

    [TestMethod]
    public void AddTodoCommand_AddsTrimmedTodoAndClearsInput()
    {
        var viewModel = new MainViewModel
        {
            NewTodoTitle = "  Neue Aufgabe  "
        };

        viewModel.AddTodoCommand.Execute(null);

        Assert.AreEqual(4, viewModel.Todos.Count);
        Assert.AreEqual("Neue Aufgabe", viewModel.Todos[^1].Title);
        Assert.AreEqual(string.Empty, viewModel.NewTodoTitle);
        Assert.AreEqual(3, viewModel.RemainingCount);
    }

    [TestMethod]
    public void AddTodoCommand_CannotExecuteForWhitespace()
    {
        var viewModel = new MainViewModel
        {
            NewTodoTitle = "   "
        };

        Assert.IsFalse(viewModel.AddTodoCommand.CanExecute(null));
    }

    [TestMethod]
    public void ClearCompletedCommand_RemovesCompletedTodos()
    {
        var viewModel = new MainViewModel();

        viewModel.Todos[0].IsCompleted = true;
        viewModel.ClearCompletedCommand.Execute(null);

        Assert.AreEqual(1, viewModel.Todos.Count);
        Assert.AreEqual(1, viewModel.RemainingCount);
        Assert.AreEqual(0, viewModel.CompletedCount);
    }
}

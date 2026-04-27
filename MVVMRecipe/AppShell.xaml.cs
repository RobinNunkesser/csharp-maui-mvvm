namespace MVVMRecipe;

public partial class AppShell : Shell
{
    public AppShell(MainPage mainPage)
    {
        InitializeComponent();
        HomeShellContent.ContentTemplate = new DataTemplate(() => mainPage);
    }
}

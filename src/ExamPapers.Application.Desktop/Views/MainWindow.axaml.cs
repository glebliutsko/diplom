using Avalonia.Controls;
using ExamPapers.Application.Desktop.Views.MainMenuItems;
using ExamPapers.Application.Desktop.Views.MainWindowPages;
using Material.Icons;

namespace ExamPapers.Application.Desktop.Views;

public partial class MainWindow : Window
{
    private readonly IMainMenuItem[] _menuItems;

    public MainWindow()
    {
        InitializeComponent();

        _menuItems = GetItemsMenu().Result;
        MainMenuListBox.ItemsSource = _menuItems;
    }

    private async Task<IMainMenuItem[]> GetItemsMenu()
    {
        var currentUser = await ExamApiClientKeeper.Client.GetMe()
            .ConfigureAwait(false);

        IMainMenuItem[]? menuItems = null;
        switch (currentUser.Role)
        {
            case "Admin":
                menuItems = new IMainMenuItem[]
                {
                    new CustomMenuItem(MaterialIconKind.Users,
                        "Пользователи",
                        () => new UserManagementUserControl(this)),
                    new CustomMenuItem(MaterialIconKind.Group,
                        "Группы",
                        () => null)
                };
                break;
            case "Teacher":
                menuItems = new IMainMenuItem[]
                {
                    new CustomMenuItem(MaterialIconKind.Help,
                        "Банк вопросов",
                        () => new BankQuestionsUserControl(this))
                };
                break;
            case "Student":
                break;
        }

        return menuItems!;
    }

    private void MainMenuListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox eSender)
            return;

        var selectedItem = _menuItems[eSender.SelectedIndex];
        MainContentControl.Content = selectedItem.GetContent();
    }
}
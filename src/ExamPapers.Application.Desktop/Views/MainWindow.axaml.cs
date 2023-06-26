using Avalonia;
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
                        () => new BankQuestionsUserControl(this)),
                    new CustomMenuItem(MaterialIconKind.HelpBoxMultiple,
                        "Тесты",
                        () => new TestsUserControl(this)),
                    new CustomMenuItem(MaterialIconKind.CheckAll,
                        "Результаты",
                        () => new TestingSessionUserControl(this))
                };
                break;
            case "Student":
                menuItems = new IMainMenuItem[]
                {
                    new CustomMenuItem(MaterialIconKind.HelpBoxMultiple,
                        "Тестирование",
                        () => new StudentTestingUserControl(this))
                };
                break;
        }

        return menuItems!;
    }

    private void MainMenuListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListBox eSender)
            return;
        
        if (eSender.SelectedIndex == -1)
            return;

        var selectedItem = _menuItems[eSender.SelectedIndex];
        MainContentControl.Content = selectedItem.GetContent();
    }

    public void ShowContent(AvaloniaObject content)
    {
        MainMenuListBox.SelectedIndex = -1;
        MainContentControl.Content = content;
    }
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class NewGroupDialog : Window
{
    private readonly GroupResponse? _group;
    public GroupRequest? Result { get; private set; } = null;
    
    public NewGroupDialog(GroupResponse? group = null)
    {
        _group = group;
        
        InitializeComponent();

        if (_group != null)
        {
            Title = "Редактирование группы";
            HeaderTextBlock.Text = "Редактирование группы";
            GroupNameTextBox.Text = _group.Name;
        }
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void AcceptButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ErrorTextBlock.Text = "";
        
        var groupName = GroupNameTextBox.Text;
        if (groupName == null)
        {
            ErrorTextBlock.Text = "Заполните все поля";
            return;
        }

        Result = new GroupRequest
        {
            Name = groupName
        };
        Close();
    }
}
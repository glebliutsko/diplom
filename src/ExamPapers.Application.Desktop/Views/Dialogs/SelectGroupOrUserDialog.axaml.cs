using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ExamPapers.API.Entity;

namespace ExamPapers.Application.Desktop.Views.Dialogs;

public partial class SelectGroupOrUserDialog : Window
{
    public int? GroupId { get; private set; }
    public int? StudentId { get; private set; }
    
    public SelectGroupOrUserDialog()
    {
        InitializeComponent();

        StudentListBox.ItemsSource = ExamApiClientKeeper.Client.GetAllUsers().Result
            .Where(x => x.Role == "Student")
            .ToList();
        GroupListBox.ItemsSource = ExamApiClientKeeper.Client.GetAllGroups().Result;
    }

    private void SendButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ErrorTextBlock.Text = "";
        if (TypeDistributionComboBox.SelectedItem == null)
        {
            ErrorTextBlock.Text = "Заполните все поля";
            return;
        }

        if (TypeDistributionComboBox.SelectedIndex == 0)
        {
            if (StudentListBox.SelectedItem == null)
            {
                ErrorTextBlock.Text = "Заполните все поля";
                return;
            }

            StudentId = ((UserResponse)StudentListBox.SelectedItem).Id;
        }
        else if (TypeDistributionComboBox.SelectedIndex == 1)
        {
            if (GroupListBox.SelectedItem == null)
            {
                ErrorTextBlock.Text = "Заполните все поля";
                return;
            }

            GroupId = ((GroupResponse)GroupListBox.SelectedItem).Id;
        }
        Close();
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void TypeDistributionComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (TypeDistributionComboBox.SelectedItem == null)
            return;
        
        if (TypeDistributionComboBox.SelectedIndex == 0)
        {
            StudentListBox.IsVisible = true;
            GroupListBox.IsVisible = false;
        }
        else if (TypeDistributionComboBox.SelectedIndex == 1)
        {
            StudentListBox.IsVisible = false;
            GroupListBox.IsVisible = true;
        }
    }
}
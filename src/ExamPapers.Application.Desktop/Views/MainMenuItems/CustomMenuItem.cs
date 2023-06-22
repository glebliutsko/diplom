using System;
using Avalonia;
using Material.Icons;

namespace ExamPapers.Application.Desktop.Views.MainMenuItems;

public class CustomMenuItem : IMainMenuItem
{
    private readonly Func<AvaloniaObject> _factoryContent;

    public CustomMenuItem(MaterialIconKind icon, string title, Func<AvaloniaObject> factoryContent)
    {
        _factoryContent = factoryContent;
        Icon = icon;
        Title = title;
    }

    public MaterialIconKind Icon { get; set; }
    public string Title { get; set; }

    public AvaloniaObject GetContent()
    {
        return _factoryContent();
    }
}
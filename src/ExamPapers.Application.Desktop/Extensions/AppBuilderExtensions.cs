using System;
using Avalonia;

namespace ExamPapers.Application.Desktop.Extensions;

public static class AppBuilderExtensions
{
    public static AppBuilder SetX11WmClass(this AppBuilder appBuilder, string wmClass)
    {
        if (!OperatingSystem.IsLinux())
            return appBuilder;

        var x11Options = new X11PlatformOptions
        {
            WmClass = "ExamPapers"
        };
        return appBuilder.With(x11Options);
    }
}
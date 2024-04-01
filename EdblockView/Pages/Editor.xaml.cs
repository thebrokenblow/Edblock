using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using EdblockView.Abstractions;
using EdblockViewModel.PagesVM;

namespace EdblockView.Pages;

/// <summary>
/// Логика взаимодействия для Editor.xaml
/// </summary>
public partial class Editor : UserControl
{
    private EditorVM? edblockVM;
    public Editor()
    {
        InitializeComponent();
    }

    private void ChangedSizeWindow(object sender, SizeChangedEventArgs e)
    {
        var edblock = (Editor)sender;

        var widthWindow = (int)edblock.ActualWidth;
        var heightWindow = (int)edblock.ActualHeight;

        edblockVM ??= (EditorVM)DataContext;

        edblockVM?.CanvasSymbolsVM.ScalingCanvasSymbolsVM.CalculateSizeUnscaledCanvas(
            widthWindow,
            heightWindow);
    }
}
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Edblock.SymbolsUI.Factories;
using Microsoft.Extensions.DependencyInjection;
using EdblockViewModel.Symbols;
using EdblockViewModel.Components.ListSymbols;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для ListSymbols.xaml
/// </summary>
public partial class ListSymbols : UserControl
{
    private ListSymbolsVM? listSymbolsVM;

    public ListSymbols() =>
        InitializeComponent();

    private void AddSymbolView(object sender, MouseButtonEventArgs e)
    {
    }
}
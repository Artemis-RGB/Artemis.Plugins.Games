using Artemis.Plugins.Games.TruckSimulator.ViewModels;
using Avalonia.Markup.Xaml;
using ReactiveUI.Avalonia;

namespace Artemis.Plugins.Games.TruckSimulator.Views;

public partial class TruckSimulatorConfigurationView : ReactiveUserControl<TruckSimulatorConfigurationViewModel>
{
    public TruckSimulatorConfigurationView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
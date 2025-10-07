using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Threading;
using MonitorLights.Backend;
using MonitorLights.Backend.Helper;
using MonitorLights.UI.Windows.Models;

namespace MonitorLights.UI.Windows.Views;

public partial class MainWindow : Window
{
    private ComboBox _monitorComboBox;
    private Slider _opacitySlider;
    private MainWindowModel _model;
    
    private bool _initialized;
    
    private bool _displayActive;
    
    public MainWindow()
    {
        this._initialized = false;
        this._displayActive = false;
        
        InitializeComponent();

        this._monitorComboBox = this.FindControl<ComboBox>(nameof(CTRL_Monitor_Selector))!;
        this._opacitySlider = this.FindControl<Slider>(nameof(CTRL_Brightness_Slider))!;
        
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (this.DataContext is MainWindowModel model)
        {
            this._model = model;
        }

        this._monitorComboBox.SelectedIndex = MonitorHelper.GetMonitorIndex(this._model.SelectedMonitor);
        this._opacitySlider.Value = Core.Instance.Settings.Brightness;
        
        this._initialized = true;
    }

    private void CTRL_Monitor_Selector_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (!this._initialized)
            return;
        
        Core.Instance.Settings.SelectedMonitor = MonitorHelper.GetStringFromMonitor(this._model.SelectedMonitor);
    }

    private void CTRL_Brightness_Slider_OnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if (!this._initialized)
            return;
        
        Core.Instance.Settings.Brightness = e.NewValue;
    }

    private void CTRL_Display_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            DisplayWindow displayWindow = new DisplayWindow();
            displayWindow.ParentWindow = this;
            displayWindow.Show(this);
        });
    }
}
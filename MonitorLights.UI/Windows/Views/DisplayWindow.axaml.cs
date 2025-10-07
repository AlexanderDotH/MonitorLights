using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using MonitorDetails.Models;
using MonitorLights.Backend;
using MonitorLights.Backend.Extensions;
using MonitorLights.Backend.Helper;

namespace MonitorLights.UI.Windows.Views;

public partial class DisplayWindow : Window
{
    private Window _parent;
    
    private SolidColorBrush _backgroundBrush;

    public DisplayWindow()
    {
        this._backgroundBrush = new SolidColorBrush(Colors.White);   
        
        InitializeComponent();
        
        this.Loaded += OnLoaded;
    }
    
    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        DisplayDevice device = MonitorHelper.GetMonitorFromString(Core.Instance.Settings.SelectedMonitor);

        if (device is Monitor monitor)
        {
            this.Position = new PixelPoint(monitor.MonitorCoordinates.X, monitor.MonitorCoordinates.Y);
            this.Width = monitor.MonitorCoordinates.Width;
            this.Height = monitor.MonitorCoordinates.Height;
        }

        if (this.ParentWindow is MainWindow window)
        {
            this._parent = window;
            
            Slider slider = this._parent.FindControl<Slider>("CTRL_Brightness_Slider")!;
            slider.ValueChanged += SliderOnValueChanged;
        }
        
        AdjustBrightness(Core.Instance.Settings.Brightness);
    }

    private void SliderOnValueChanged(object? sender, RangeBaseValueChangedEventArgs e)
    {
        AdjustBrightness(e.NewValue);
    }

    private void AdjustBrightness(double brightness)
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            this.Background = this._backgroundBrush.AdjustBrightness(brightness);
        });
    }
    
    private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        this.Close();
    }
    
    public Window ParentWindow
    {
        get => _parent;
        set => _parent = value;
    }
}
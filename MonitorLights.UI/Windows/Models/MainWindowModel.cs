using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using MonitorDetails;
using MonitorDetails.Interfaces;
using MonitorDetails.Models;
using MonitorLights.Backend;
using MonitorLights.Backend.Helper;

namespace MonitorLights.UI.Windows.Models;

public class MainWindowModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private DisplayDevice _selectedMonitor;
    private ObservableCollection<DisplayDevice>  _monitors;
    
    public MainWindowModel()
    {
        this._monitors = MonitorHelper.GetMonitors();
        this.SelectedMonitor = MonitorHelper.GetMonitorFromString(Core.Instance.Settings.SelectedMonitor);
    }
    
    public DisplayDevice SelectedMonitor
    {
        get => _selectedMonitor;
        set => _selectedMonitor = value;
    }

    public ObservableCollection<DisplayDevice> Monitors
    {
        get
        {
            return this._monitors;
        }
    }
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
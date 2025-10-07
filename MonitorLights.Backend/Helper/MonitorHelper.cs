using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using MonitorDetails;
using MonitorDetails.Interfaces;
using MonitorDetails.Models;
using MonitorLights.Backend.Extensions;
using Monitor = MonitorDetails.Models.Monitor;

namespace MonitorLights.Backend.Helper;

public class MonitorHelper
{
    private static IReader _reader;

    static MonitorHelper()
    {
        _reader = new Reader();
    }
    
    public static ObservableCollection<DisplayDevice> GetMonitors()
    {
        ObservableCollection<DisplayDevice> monitorCollection = new ObservableCollection<DisplayDevice>();
        
        foreach (DisplayDevice monitorDetail in _reader.GetMonitorDetails())
            monitorCollection.Add(monitorDetail);

        return monitorCollection;
    }

    public static DisplayDevice GetMonitorFromString(string monitorName)
    {
        if (string.IsNullOrEmpty(monitorName))
            return _reader.GetMonitorDetails().FirstOrDefault()!;
        
        return _reader.GetMonitorDetails().First(m => m.Name.SequenceEqual(monitorName));
    }

    public static int GetMonitorIndex(DisplayDevice monitorDetail)
    {
        return GetMonitors().FindIndex(m => m.Id.SequenceEqual(monitorDetail.Id));
    }
    
    public static string GetStringFromMonitor(DisplayDevice monitor)
    {
        return monitor.Name;
    }
}
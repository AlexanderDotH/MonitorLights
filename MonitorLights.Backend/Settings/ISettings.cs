using Config.Net;

namespace MonitorLights.Backend.Settings;

public interface ISettings
{
    string SelectedMonitor { get; set; }
    
    [Option(DefaultValue = "100")]
    double Brightness { get; set; }
}
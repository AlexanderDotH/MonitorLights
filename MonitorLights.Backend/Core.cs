using Config.Net;
using Dumpify;
using MonitorDetails;
using MonitorDetails.Interfaces;
using MonitorLights.Backend.Settings;

namespace MonitorLights.Backend;

public class Core
{
    private static Core _instance;
    private ISettings _settings;
    
    public void Initialize()
    {
        ISettings settings = new ConfigurationBuilder<ISettings>()
            .UseJsonFile("Config.json")
            .Build();
        
        this._settings = settings;
    }

    public ISettings Settings
    {
        get => _settings;
    }

    public static Core Instance
    {
        get
        {
            if  (_instance == null)
                _instance = new Core();
            
            return _instance;
        }
    }
}
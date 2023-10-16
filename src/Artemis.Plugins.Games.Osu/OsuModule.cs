using System.Collections.Generic;
using Artemis.Core.Modules;
using Artemis.Plugins.Games.Osu.DataModels;
using Serilog;

namespace Artemis.Plugins.Games.Osu;

public class OsuModule : Module<OsuDataModel>
{
    private readonly ILogger _logger;
    
    public OsuModule(ILogger logger)
    {
        _logger = logger;    
    }
    
    public override List<IModuleActivationRequirement>? ActivationRequirements { get; } = new()
    {
        new ProcessActivationRequirement("osu!")
    };
    
    public override void Enable()
    {

    }

    public override void Disable()
    {
    }

    public override void Update(double deltaTime)
    {
        var reader = OsuMemoryDataProvider.StructuredOsuMemoryReader.Instance;
        if (reader.TryRead(reader.OsuMemoryAddresses.GeneralData))
        {
            DataModel.GeneralData.Apply(reader.OsuMemoryAddresses.GeneralData);
        }
    }
}
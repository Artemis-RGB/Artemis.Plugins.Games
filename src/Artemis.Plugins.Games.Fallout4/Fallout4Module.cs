using Artemis.Core;
using Artemis.Core.Modules;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Artemis.Plugins.Games.Fallout4
{
    [PluginFeature(AlwaysEnabled = true, Name = "Fallout 4")]
    public class Fallout4Module : Module<FalloutMapDataModel>
    {
        private readonly ILogger _logger;
        private Fallout4Reader reader;

        public override List<IModuleActivationRequirement> ActivationRequirements { get; } = new() { new ProcessActivationRequirement("Fallout4") };

        public Fallout4Module(ILogger logger)
        {
            _logger = logger;

            UpdateDuringActivationOverride = false;
        }

        public override void Enable()
        {
        }

        public override void Disable()
        {
        }

        public override void Update(double deltaTime)
        {
        }

        public override void ModuleActivated(bool isOverride)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    reader = new();
                    reader.DataReceived += OnReaderDataReceived;
                    reader.FieldRemoved += OnReaderFieldRemoved;
                    return;
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Failed to initialize Fallout 4 reader, retrying...");
                    Thread.Sleep(2000);
                }
            }

            throw new ArtemisPluginException("Failed to initialize Fallout 4 module. Make sure the Pip-boy option is enabled.");
        }

        private void OnReaderFieldRemoved(object sender, uint e)
        {
            _logger.Information("Removed field {e}", e);
        }

        public override void ModuleDeactivated(bool isOverride)
        {
            reader.DataReceived -= OnReaderDataReceived;
            reader.FieldRemoved -= OnReaderFieldRemoved;
            reader?.Dispose();
        }

        private void OnReaderDataReceived(object sender, EventArgs e)
        {
            Profiler.StartMeasurement("Fill DataModel");
            DataModel.Fill(reader.Database);
            Profiler.StopMeasurement("Fill DataModel");
        }
    }
}
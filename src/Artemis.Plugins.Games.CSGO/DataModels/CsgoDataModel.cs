using Artemis.Core.Modules;
using System.Collections.Generic;
using System.ComponentModel;
using Artemis.Plugins.Games.CSGO.GameDataModels;

namespace Artemis.Plugins.Games.CSGO.DataModels;

public class CsgoDataModel : DataModel
{
    public RootGameData? Data { get; set; }
}
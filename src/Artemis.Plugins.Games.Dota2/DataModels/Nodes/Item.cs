using Newtonsoft.Json;

namespace Artemis.Plugins.Games.Dota2.DataModels.Nodes
{
    public enum ItemUsability
    {
        Empty,
        Passive,
        Usable,
        OnCooldown,
    }

    /// <summary>
    /// Class representing item information
    /// </summary>
    [JsonObject]
    public class Item
    {
        /// <summary>
        /// Item name
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// The name of the rune cotnained inside this item.
        /// <note type="note">Possible rune names: empty, arcane, bounty, double_damage, haste, illusion, invisibility, regen</note>
        /// </summary>
        public string ContainsRune { get; set; } = "";

        /// <summary>
        /// A boolean representing whether this item can be casted
        /// </summary>
        public bool CanCast { get; set; }

        /// <summary>
        /// Item's cooldown
        /// </summary>
        public int Cooldown { get; set; }

        /// <summary>
        /// A boolean representing whether this item is passive
        /// </summary>
        public bool Passive { get; set; }

        /// <summary>
        /// The amount of charges on this item
        /// </summary>
        public int Charges { get; set; }

        public ItemUsability Usability
        {
            get
            {
                if (Name.Equals(""))
                {
                    return ItemUsability.Empty;
                }
                if (Passive)
                {
                    return ItemUsability.Passive;
                }
                return Cooldown > 0 ? ItemUsability.OnCooldown : ItemUsability.Usable;
            }
        }
    }
}

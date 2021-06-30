using System.ComponentModel;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace MechArmor
{
    public class MechArmorServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(false), Label("Use testing recipes"), ReloadRequired]
        public bool UseTestingRecipes;
    }
}

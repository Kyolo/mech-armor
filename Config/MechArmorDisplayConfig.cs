using System;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MechArmor.Config
{
    public class MechArmorDisplayConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(1000), Range(0, 4096)]
        public int DefaultArmorStatusPositionX;
        [DefaultValue(0), Range(0, 4096)]
        public int DefaultArmorStatusPositionY;

        [DefaultValue(false)]
        public bool BulletAttractorGenerateDust;

    }
}

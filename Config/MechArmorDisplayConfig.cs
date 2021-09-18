using System;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MechArmor.Config
{
    public class MechArmorDisplayConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Default X position of the state indicator."), Tooltip("From left to right and in pixels, starting at 0."), DefaultValue(0)]
        public int DefaultArmorStatusPositionX;
        [Label("Default Y position of the state indicator."), Tooltip("From top to bottom and in pixels, starting at 0."), DefaultValue(0)]
        public int DefaultArmorStatusPositionY;

        [Label("Bullet attractor particle effect"),Tooltip("If the Bullet Attractor Effect should generate dust to display the range arround the player"), DefaultValue(false)]
        public bool BulletAttractorGenerateDust;

    }
}

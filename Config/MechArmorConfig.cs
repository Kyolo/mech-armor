using System.Collections.Generic;
using System.ComponentModel;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace MechArmor
{
    public class MechArmorServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(false), ReloadRequired]
        public bool UseTestingRecipes;

        [ReloadRequired]
        public Dictionary<ProjectileDefinition, bool> ProjectileBlacklist;

        [ReloadRequired]
        public Dictionary<int, bool> AiWhitelist;

        [DefaultValue(false)]
        public bool BoostLunarMultishot;


        public bool CanAffectProjectile(Projectile p)
        {
            ProjectileDefinition pdef = new ProjectileDefinition(p.type);
            return AiWhitelist.ContainsKey(p.aiStyle) && AiWhitelist[p.aiStyle]// the ai is whitelisted
                && !(ProjectileBlacklist.ContainsKey(pdef) && ProjectileBlacklist[pdef]);// This specific projectile is not blacklisted
        }

        public MechArmorServerConfig()
        {
            ProjectileBlacklist = new Dictionary<ProjectileDefinition, bool>();

            AiWhitelist = new Dictionary<int, bool>
            {
                { 1, true },// Basic Arrows
                { 2, true },// Thrown weapons
                { 3, true },// Boomerangs
                { 8, true },// Ball of fire
                { 10, true },// Sand ball
                { 11, true },
                { 12, true },
                { 14, true },// Spiky ball
                { 16, true },// Enemy bomb
                { 18, true },// Demon sickle
                { 23, true },// Flames
                { 25, true },// Boulder
                { 26, true },// Turtles
                { 27, true },// Unholy trident
                { 28, true },// Frost/Ice Bolt
                { 29, true },// Magic weapons bolt
                { 33, true },// Flare
                { 34, true },// Rockets
                { 36, true },// Bees
                { 37, true },// Spear
                { 45, true },// Rain
                { 48, true },// Shadow beam
                { 50, true },// Inferno
                { 51, true },// Lost Soul
                { 56, true },// Flaming scythe
                { 58, true },// Present
                { 64, false },// Sharknado
                { 65, true },// Sharknado bolt
                { 79, true },// Martian deathray
                { 80, true },// Martian rocket
                { 85, true },// Moon leech
                { 86, true },// Ice Mist
                { 102, true },// Flow Invader
                { 103, true },// Starmark
                { 105, true },// Spore
                { 106, true },// Spore 2
                { 107, true },// Desert Spirit's Curse
                { 122, true },// Released Energy

            };
        }
    }
}

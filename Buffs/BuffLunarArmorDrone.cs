using System;
using Terraria;
using Terraria.ModLoader;

using MechArmor.Projectiles;

namespace MechArmor.Buffs
{
    public class BuffLunarArmorDrone : ModBuff
    {

        // This buff is only a visual indicator for now
        // Maybe change it later or even remove it

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Drones");
            Description.SetDefault("You are assisted by lunar drones.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<LunarArmorDrone>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}

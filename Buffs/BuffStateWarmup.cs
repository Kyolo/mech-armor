using System;
using Terraria;
using Terraria.ModLoader;

namespace MechArmor.Buffs
{
    public class BuffStateWarmup : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("State change warmup");
            Description.SetDefault("Your armor need to finish changing state");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MechArmorPlayer>().IsArmorOnWarmup = true;
        }
    }
}

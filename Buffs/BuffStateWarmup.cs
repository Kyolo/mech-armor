using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Buffs
{
    public class BuffStateWarmup : ModBuff
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("State change warmup");
*/
/* 
Removed because of localization update
            Description.SetDefault("Your armor need to finish changing state");
*/
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MechArmorPlayer>().IsArmorOnWarmup = true;
        }
    }
}

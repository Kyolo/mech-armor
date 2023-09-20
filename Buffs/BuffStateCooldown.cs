using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Buffs
{
    public class BuffStateCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("State change cooldown");
*/
/* 
Removed because of localization update
            Description.SetDefault("Your armor can't change state");
*/
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MechArmorPlayer>().IsArmorOnCooldown = true;
        }
    }
}

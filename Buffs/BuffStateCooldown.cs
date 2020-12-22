using System;
using Terraria;
using Terraria.ModLoader;

namespace MechArmor.Buffs
{
    public class BuffStateCooldown : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("State change cooldown");
            Description.SetDefault("Your armor can't change state");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<MechArmorPlayer>().ArmorCooldown = true;
        }
    }
}

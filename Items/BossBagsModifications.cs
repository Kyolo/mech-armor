using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items
{
    public class BossBagsModifications : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag" && arg == ItemID.PlanteraBossBag)
            {
                if (Main.rand.Next(3) < 1)
                {
                    player.QuickSpawnItem(ModContent.ItemType<StrangeWood>(), Main.rand.Next(5, 10));
                }
            }
        }
    }
}

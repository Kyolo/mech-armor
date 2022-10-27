using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace MechArmor.Items
{
    public class BossBagsModifications : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot) {
		    if(item.type == ItemID.PlanteraBossBag) {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StrangeWood>(), 3, 5, 10));
			}
		}
        
    }
}

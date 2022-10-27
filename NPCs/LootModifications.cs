using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

using MechArmor.Items;

namespace MechArmor.NPCs
{
    public class LootModifications : GlobalNPC
    {

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(npc.type == NPCID.Plantera && !Main.expertMode && !Main.masterMode) {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StrangeWood>(), 3, 3, 8));
            }
        }
    }
}

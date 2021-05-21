using System;

using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

using MechArmor.Items;

namespace MechArmor.NPCs
{
    public class LootModifications : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if(!Main.expertMode && npc.type == NPCID.Plantera)
            {
                if(Main.rand.Next(10) < 5)
                {
                    Item.NewItem(npc.Center, ModContent.ItemType<StrangeWood>(), Main.rand.Next(5, 11));
                }
            }
        }
    }
}

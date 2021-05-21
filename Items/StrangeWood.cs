using System;

using Terraria.ModLoader;
using Terraria.ID;

namespace MechArmor.Items
{
    public class StrangeWood : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A strange wood taken from the corpse of Plantera.\nUseless as a building material.");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;

            item.maxStack = 999;
            item.value = 400;//4 silver a piece
            item.rare = ItemRarityID.Lime;
        }
    }
}

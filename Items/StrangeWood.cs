using System;

using Terraria.ModLoader;
using Terraria.ID;

namespace MechArmor.Items
{
    public class StrangeWood : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            Tooltip.SetDefault("A strange wood taken from the corpse of Plantera.\nUseless as a building material.");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;

            Item.maxStack = 999;
            Item.value = 400;//4 silver a piece
            Item.rare = ItemRarityID.Lime;
        }
    }
}

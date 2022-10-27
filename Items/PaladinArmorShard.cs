using System;

using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items
{
    public class PaladinArmorShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Made out of melted Paladin equipement");
        }

        public override void SetDefaults()
        {
            Item.width = 19;
            Item.height = 22;

            Item.maxStack = 999;
            Item.value = 400;//4 silver a piece
            Item.rare = ItemRarityID.Lime;
        }

        public override void AddRecipes()
        {
            CreateRecipe(12)
            .AddTile(TileID.AdamantiteForge)
            .AddIngredient(ItemID.PaladinsShield, 1)
            .Register();

            CreateRecipe(6)
            .AddTile(TileID.AdamantiteForge)
            .AddIngredient(ItemID.PaladinsHammer, 1)
            .Register();
        }
    }
}

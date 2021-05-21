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
            item.width = 19;
            item.height = 22;

            item.maxStack = 999;
            item.value = 400;//4 silver a piece
            item.rare = ItemRarityID.Lime;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.AddIngredient(ItemID.PaladinsShield, 1);
            recipe.SetResult(this, 12);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.AddIngredient(ItemID.PaladinsHammer, 1);
            recipe.SetResult(this, 6);
            recipe.AddRecipe();
        }
    }
}

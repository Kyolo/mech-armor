using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Body)]
    class LightPlateProtectedArmorBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A breastplate with light armor plates");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 6;
        }

        // Set bonus in PlateProtectedArmorHelmet.cs

        public override void AddRecipes()
        {
            // Check if we need to use the testing recipes
            if (ModContent.GetInstance<MechArmorServerConfig>().UseTestingRecipes)
            {
                ModRecipe recipeW = new ModRecipe(mod);
                recipeW.AddRecipeGroup("Wood", 20);
                recipeW.AddTile(TileID.WorkBenches);

                recipeW.SetResult(this);
                recipeW.AddRecipe();
            }
            
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ModContent.ItemType<PlateProtectedArmorLeggings>());
            recipe.AddRecipeGroup("MechArmor:Bars:Evil", 20);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

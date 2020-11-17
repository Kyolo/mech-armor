using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    class AdvancedPlateProtectedArmorLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A legging with alloyed hellstone and evilmetal armor plates");
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
                ModRecipe wRecipe = new ModRecipe(mod);
                wRecipe.AddRecipeGroup("Wood", 10);
                wRecipe.AddTile(TileID.WorkBenches);
                wRecipe.SetResult(this);
                wRecipe.AddRecipe();
            }

            //Upgrade from heavy
            ModRecipe recipeUpgradeHeavy = new ModRecipe(mod);
            recipeUpgradeHeavy.AddTile(TileID.Anvils);
            recipeUpgradeHeavy.AddIngredient(ModContent.ItemType<HeavyPlateProtectedArmorLeggings>());
            recipeUpgradeHeavy.AddRecipeGroup("MechArmor:Bars:Evil", 10);

            recipeUpgradeHeavy.SetResult(this);
            recipeUpgradeHeavy.AddRecipe();

            //Upgrade from light
            ModRecipe recipeUpgradeLight = new ModRecipe(mod);
            recipeUpgradeLight.AddTile(TileID.Anvils);
            recipeUpgradeLight.AddIngredient(ModContent.ItemType<LightPlateProtectedArmorLeggings>());
            recipeUpgradeLight.AddIngredient(ItemID.HellstoneBar, 10);

            recipeUpgradeLight.SetResult(this);
            recipeUpgradeLight.AddRecipe();

            //Upgrade from basic
            ModRecipe recipeUpgradeBasic = new ModRecipe(mod);
            recipeUpgradeBasic.AddTile(TileID.Anvils);
            recipeUpgradeBasic.AddIngredient(ModContent.ItemType<PlateProtectedArmorLeggings>());
            recipeUpgradeBasic.AddIngredient(ItemID.HellstoneBar, 10);
            recipeUpgradeBasic.AddRecipeGroup("MechArmor:Bars:Evil", 10);

            recipeUpgradeBasic.SetResult(this);
            recipeUpgradeBasic.AddRecipe();

        }
    }
}

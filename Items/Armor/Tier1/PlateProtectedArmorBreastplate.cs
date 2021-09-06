using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Body)]
    class PlateProtectedArmorBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 5;
        }

        // Set bonus in PlateProtectedArmorHelmet.cs

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            // Check if we need to use the testing recipes
            if(ModContent.GetInstance<MechArmorServerConfig>().UseTestingRecipes)
            {
                recipe.AddRecipeGroup("Wood", 20);
                recipe.AddTile(TileID.WorkBenches);
            }
            else
            {
                recipe.AddTile(TileID.Anvils);
                recipe.AddIngredient(ItemID.DartTrap, 4);
                recipe.AddRecipeGroup("MechArmor:Armor:Chest:T2");
            }
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

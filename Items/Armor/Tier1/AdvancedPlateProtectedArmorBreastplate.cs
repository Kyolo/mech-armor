using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Body)]
    class AdvancedPlateProtectedArmorBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 70;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 7;
        }

        // Set bonus in PlateProtectedArmorHelmet.cs

        public override void AddRecipes()
        {
            
            //Upgrade from heavy
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<HeavyPlateProtectedArmorBreastplate>())
            .AddRecipeGroup("MechArmor:Bars:Evil", 20)
            .AddIngredient(ItemID.Wire, 20)
            .Register();

            //Upgrade from light
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<LightPlateProtectedArmorBreastplate>())
            .AddIngredient(ItemID.HellstoneBar, 20)
            .AddIngredient(ItemID.Wire, 20)

            .Register();

            //Upgrade from basic
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<PlateProtectedArmorBreastplate>())
            .AddIngredient(ItemID.HellstoneBar, 20)
            .AddRecipeGroup("MechArmor:Bars:Evil", 20)
            .AddIngredient(ItemID.Wire, 20)
            .Register();
            
        }
    }
}

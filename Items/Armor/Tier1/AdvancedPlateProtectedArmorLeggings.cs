using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Legs)]
    class AdvancedPlateProtectedArmorLeggings : ModItem
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
            Item.defense = 6;
            
        }


        public override void AddRecipes()
        {
            //Upgrade from heavy
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<HeavyPlateProtectedArmorLeggings>())
            .AddRecipeGroup("MechArmor:Bars:Evil", 10)
            .AddIngredient(ItemID.Wire, 10)
            .Register();

            //Upgrade from light
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<LightPlateProtectedArmorLeggings>())
            .AddIngredient(ItemID.HellstoneBar, 10)
            .AddIngredient(ItemID.Wire, 10)
            .Register();

            //Upgrade from basic
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<PlateProtectedArmorLeggings>())
            .AddIngredient(ItemID.HellstoneBar, 10)
            .AddRecipeGroup("MechArmor:Bars:Evil", 10)
            .AddIngredient(ItemID.Wire, 10)
            .Register();

        }
    }
}

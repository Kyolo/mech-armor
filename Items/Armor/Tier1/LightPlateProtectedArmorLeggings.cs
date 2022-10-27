using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Legs)]
    class LightPlateProtectedArmorLeggings : ModItem
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
            Item.defense = 5;
            
        }


        public override void AddRecipes()
        {

            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<PlateProtectedArmorLeggings>())
            .AddRecipeGroup("MechArmor:Bars:Evil", 10)
            .Register();
        }
    }
}

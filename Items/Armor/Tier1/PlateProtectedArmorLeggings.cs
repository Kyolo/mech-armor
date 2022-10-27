using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Legs)]
    class PlateProtectedArmorLeggings : ModItem
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
            Item.defense = 4;
            
        }

        // Set bonus in PlateProtectedArmorHelmet.cs

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.DartTrap, 2)
            .AddRecipeGroup("MechArmor:Armor:Pants:T2")
            .Register();
        }
    }
}

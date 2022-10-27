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
            Item.width = 18;
            Item.height = 18;
            Item.value = 70;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 5;
        }

        // Set bonus in PlateProtectedArmorHelmet.cs

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.DartTrap, 4)
            .AddRecipeGroup("MechArmor:Armor:Chest:T2")
            .Register();
        }
    }
}

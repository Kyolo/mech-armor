using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier2
{
    [AutoloadEquip(EquipType.Legs)]
    class TierAdaLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("Hard Metal Leggings");
*/
/* 
Removed because of localization update
            Tooltip.SetDefault("5% Increased Movement Speed");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 11;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddRecipeGroup("MechArmor:Bars:HMT1", 15)
            .AddRecipeGroup("MechArmor:Bars:HMT2", 15)
            .AddRecipeGroup("MechArmor:Bars:HMT3", 15)
            .Register();
        }
    }
}

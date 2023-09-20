using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier3
{
    [AutoloadEquip(EquipType.Legs)]
    class HolyTierLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("Holy Mechanized Leggings");
*/
/* 
Removed because of localization update
            Tooltip.SetDefault("10% increased movement speed");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 11;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.HallowedBar, 15)
            .AddIngredient(ItemID.SoulofFright, 10)
            .Register();
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier3
{
    [AutoloadEquip(EquipType.Body)]
    class HolyTierBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("Holy Mechanized Breastplate");
*/
/* 
Removed because of localization update
            Tooltip.SetDefault("10% increased critical strike chance");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 4, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 10;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.HallowedBar, 20)
            .AddIngredient(ItemID.SoulofMight, 10)
            .Register();
        }
    }
}

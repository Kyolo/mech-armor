using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Body)]
    class PostPTierRangedBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            DisplayName.SetDefault("Glowing Ranger Breastplate");
*/
*/
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("10% increased ranged critical chance\n10% increased ranged damage");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 6, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.10f;
            player.GetCritChance(DamageClass.Ranged) += 10;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.ShroomiteBar, 20)
            .AddIngredient(ItemID.InvisibilityPotion, 10)
            .Register();
        }
    }
}

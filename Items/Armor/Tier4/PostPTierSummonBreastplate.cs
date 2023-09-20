using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Body)]
    class PostPTierSummonBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            DisplayName.SetDefault("Strange Wood Breastplate");
*/
*/
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("10% increased minon damage");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 6, 75, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.10f;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.Cog, 60)
            .AddIngredient(ModContent.ItemType<StrangeWood>(), 5)
            .Register();
        }
    }
}

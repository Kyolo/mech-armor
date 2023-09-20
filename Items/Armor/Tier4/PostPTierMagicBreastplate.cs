using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Body)]
    class PostPTierMagicBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            DisplayName.SetDefault("Shifting Spectral Breastplate");
*/
*/
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("5% increased magical critical chance\n5% increased magical damage");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 7, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Magic) += 5;
            player.GetDamage(DamageClass.Magic) += 0.05f;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.SpectreBar, 20)
            .AddIngredient(ItemID.CobaltShield, 1)
            .Register();
        }
    }
}

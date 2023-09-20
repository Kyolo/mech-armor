using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Legs)]
    class PostPTierMagicLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("Shifting Spectral Leggings");
*/
/* 
Removed because of localization update
            Tooltip.SetDefault("5% increased magical damage\n10% increased movement speed");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 5, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 10;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.GetDamage(DamageClass.Magic) += 0.05f;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.SpectreBar, 15)
            .AddIngredient(ItemID.MagnetSphere)
            .Register();
        }
    }
}

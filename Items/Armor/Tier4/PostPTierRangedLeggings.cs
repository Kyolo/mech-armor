using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Legs)]
    class PostPTierRangedLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("Glowing Ranger Leggings");
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
            Item.value = Item.buyPrice(0, 5, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 15;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.GetDamage(DamageClass.Ranged) += 5;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.ShroomiteBar, 15)
            .AddIngredient(ItemID.InvisibilityPotion, 7)
            .Register();
        }
    }
}

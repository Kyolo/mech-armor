using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Legs)]
    class PostPTierMeleeLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            DisplayName.SetDefault("Configurable Paladin Leggings");
*/
*/
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("5% increased melee critical chance");
*/
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
            player.GetCritChance(DamageClass.Melee) += 5;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ModContent.ItemType<PaladinArmorShard>(), 8)
            .AddIngredient(ItemID.ChlorophyteBar, 8)
            .Register();
        }
    }
}

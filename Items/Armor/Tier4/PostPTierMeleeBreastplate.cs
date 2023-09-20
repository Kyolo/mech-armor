using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Body)]
    class PostPTierMeleeBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            DisplayName.SetDefault("Configurable Paladin Breastplate");
*/
/* 
Removed because of localization update
            Tooltip.SetDefault("5% increased melee critical chance\n5% increased melee damage");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 7, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 25;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetDamage(DamageClass.Melee) += 0.05f;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ModContent.ItemType<PaladinArmorShard>(), 10)
            .AddIngredient(ItemID.ChlorophyteBar, 10)
            .Register();
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PostMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    class NebularMechaLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("10% increased movement speed\n10% increased magical damage");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 12, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 13;

        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.GetDamage(DamageClass.Magic) += 0.10f;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ModContent.ItemType<NebularMechaLeggings>())
            .AddIngredient(ItemID.LunarBar, 10)
            .Register();
        }
    }
}

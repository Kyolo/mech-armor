using MechArmor.Items.Armor.Tier5.PreMoonLord;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PostMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    class VortexianMechaLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("10% increased movement speed\n5% increased ranged critical chance");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 11, 75, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 20;

        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.GetCritChance(DamageClass.Ranged) += 5;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ModContent.ItemType<ProtoVortexianMechaLeggings>())
            .AddIngredient(ItemID.LunarBar, 16)
            .Register();
        }
    }
}

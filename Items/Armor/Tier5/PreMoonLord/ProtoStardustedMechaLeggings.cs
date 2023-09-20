using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    class ProtoStardustedMechaLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("10% increased movement speed\nIncrease minion slots by 2");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 11, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 8;

        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.maxMinions += 2;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ItemID.FragmentStardust, 15)
            .AddIngredient(ItemID.Wire, 15)
            .AddIngredient(ItemID.Cog, 15)
            .Register();
        }
    }
}

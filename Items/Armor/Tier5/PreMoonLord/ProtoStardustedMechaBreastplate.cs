using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Body)]
    class ProtoStardustedMechaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("20% increased minion damage\nIncrease minion slots by 2");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 15, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.20f;
            player.maxMinions += 2;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ItemID.FragmentStardust, 20)
            .AddIngredient(ItemID.Wire, 20)
            .AddIngredient(ItemID.Cog, 20)
            .Register();
        }
    }
}
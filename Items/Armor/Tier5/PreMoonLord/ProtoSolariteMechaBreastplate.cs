using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Body)]
    class ProtoSolariteMechaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("Increased Life Regeneration");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 14, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 3;//TODO: verify value and field
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ItemID.FragmentSolar, 20)
            .AddIngredient(ItemID.Wire, 20)
            .AddIngredient(ItemID.Cog, 20)
            .Register();
        }
    }
}
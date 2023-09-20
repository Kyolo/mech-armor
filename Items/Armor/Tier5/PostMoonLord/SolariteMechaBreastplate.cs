using MechArmor.Items.Armor.Tier5.PreMoonLord;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PostMoonLord
{
    [AutoloadEquip(EquipType.Body)]
    class SolariteMechaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            Tooltip.SetDefault("Increased life regeneration");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 15, 75, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 30;
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
            .AddIngredient(ModContent.ItemType<ProtoSolariteMechaBreastplate>())
            .AddIngredient(ItemID.LunarBar, 16)
            .Register();
        }
    }
}
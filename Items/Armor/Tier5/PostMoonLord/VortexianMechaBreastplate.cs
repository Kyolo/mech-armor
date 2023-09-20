using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MechArmor.Items.Armor.Tier5.PreMoonLord;

namespace MechArmor.Items.Armor.Tier5.PostMoonLord
{
    [AutoloadEquip(EquipType.Body)]
    class VortexianMechaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            Tooltip.SetDefault("10% increased ranged damage\n10% increased ranged critical chance\n-25% Ammo Consumption Chance");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 16, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 26;
        }

        public override void UpdateEquip(Player player)
        {
            player.ammoCost75 = true;
            player.GetDamage(DamageClass.Ranged) += 0.10f;
            player.GetCritChance(DamageClass.Ranged) += 10;
        }

        // Set bonus in helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ModContent.ItemType<ProtoVortexianMechaBreastplate>())
            .AddIngredient(ItemID.LunarBar, 16)
            .Register();
        }
    }
}
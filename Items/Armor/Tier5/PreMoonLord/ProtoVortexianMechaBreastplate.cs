using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Body)]
    class ProtoVortexianMechaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
/* Removed because of localization change
            Tooltip.SetDefault("10% increased ranged damage\n10% increased ranged critical chance\n25% chance not to consume ammo");
*/
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 20;
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
            .AddIngredient(ItemID.FragmentVortex, 20)
            .AddIngredient(ItemID.Wire, 20)
            .AddIngredient(ItemID.Cog, 20)
            .Register();
        }
    }
}
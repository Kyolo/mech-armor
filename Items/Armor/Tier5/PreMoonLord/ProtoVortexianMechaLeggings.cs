using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    class ProtoVortexianMechaLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased movement speed\n5% increased ranged critical chance");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 11, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 13;

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
            .AddIngredient(ItemID.FragmentVortex, 15)
            .AddIngredient(ItemID.Wire, 15)
            .AddIngredient(ItemID.Cog, 15)
            .Register();
        }
    }
}

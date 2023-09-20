using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    class ProtoSolariteMechaLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            Tooltip.SetDefault("15% increased melee speed\n15% increased movement speed");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 11, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 12;

        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
            player.moveSpeed += 0.15f;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ItemID.FragmentSolar, 15)
            .AddIngredient(ItemID.Wire, 15)
            .AddIngredient(ItemID.Cog, 15)
            .Register();
        }
    }
}

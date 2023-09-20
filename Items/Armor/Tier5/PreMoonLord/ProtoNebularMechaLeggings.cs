using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Legs)]
    class ProtoNebularMechaLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            Tooltip.SetDefault("10% increased move Speed\n10% increased magic Damage");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 11, 25, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 9;

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
            .AddIngredient(ItemID.FragmentNebula, 15)
            .AddIngredient(ItemID.Cog, 15)
            .AddIngredient(ItemID.Wire, 15)
            .Register();
        }
    }
}

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
            Tooltip.SetDefault("10% increased move Speed\n10% increased magic Damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 11, 25, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 9;

        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.magicDamage += 0.10f;
        }

        // Set bonus in the helmet(s)

        public override void AddRecipes()
        {
            if (ModContent.GetInstance<MechArmorServerConfig>().UseTestingRecipes)
            {
                ModRecipe r = new ModRecipe(mod);
                r.AddTile(TileID.WorkBenches);
                r.AddRecipeGroup("Wood");
                r.SetResult(this);
                r.AddRecipe();
            }

            ModRecipe regularRecipe = new ModRecipe(mod);
            regularRecipe.AddTile(TileID.LunarCraftingStation);
            regularRecipe.AddIngredient(ItemID.FragmentNebula, 15);
            regularRecipe.AddIngredient(ItemID.Cog, 15);
            regularRecipe.AddIngredient(ItemID.Wire, 15);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

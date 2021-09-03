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
            Tooltip.SetDefault("15% Increased Melee Speed\n15% Increased Movement Speed");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            //TODO: find best values
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 12;

        }

        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed += 0.15f;
            player.moveSpeed += 0.15f;
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
            regularRecipe.AddIngredient(ItemID.FragmentSolar, 15);
            regularRecipe.AddIngredient(ItemID.Wire, 15);
            regularRecipe.AddIngredient(ItemID.Cog, 15);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

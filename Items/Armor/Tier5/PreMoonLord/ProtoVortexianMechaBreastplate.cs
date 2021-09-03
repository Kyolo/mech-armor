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
            Tooltip.SetDefault("10% Increased Melee Damage\n10% Increased Melee Critical Chance\n-25% Ammo Consumption Chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            //TODO: find best values
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.ammoCost75 = true;
            player.rangedDamage += 0.10f;
            player.rangedCrit += 10;
        }

        // Set bonus in helmet(s)

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
            regularRecipe.AddIngredient(ItemID.FragmentVortex, 20);
            regularRecipe.AddIngredient(ItemID.Wire, 20);
            regularRecipe.AddIngredient(ItemID.Cog, 20);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
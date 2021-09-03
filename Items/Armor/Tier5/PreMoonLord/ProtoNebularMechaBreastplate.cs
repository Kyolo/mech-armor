using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Body)]
    class ProtoNebularMechaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% Magical Damage\n10% Magical Critical Chance");
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
            player.magicDamage += 0.10f;
            player.magicCrit += 10;
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
            regularRecipe.AddIngredient(ItemID.FragmentNebula, 20);
            regularRecipe.AddIngredient(ItemID.Cog, 20);
            regularRecipe.AddIngredient(ItemID.Wire, 20);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
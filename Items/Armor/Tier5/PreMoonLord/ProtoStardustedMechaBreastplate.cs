using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier5.PreMoonLord
{
    [AutoloadEquip(EquipType.Body)]
    class ProtoStardustedMechaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased minion damage\nIncrease minion slots by 2");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 15, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.20f;
            player.maxMinions += 2;
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
            regularRecipe.AddIngredient(ItemID.FragmentStardust, 20);
            regularRecipe.AddIngredient(ItemID.Wire, 20);
            regularRecipe.AddIngredient(ItemID.Cog, 20);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier2
{
    [AutoloadEquip(EquipType.Body)]
    class TierAdaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hard Metal Breastplate");
            Tooltip.SetDefault("5% Critical Strike Chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 2, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.magicCrit += 5;
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
            regularRecipe.AddTile(TileID.MythrilAnvil);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT1", 20);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT2", 20);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT3", 20);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

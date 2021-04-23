using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    class ArtilleryArmorBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A fragile breastplate with aim steadying systems");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 5;
        }

        // Set bonus in Helmet

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

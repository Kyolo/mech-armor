using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    class PowerAssistedArmorBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A breastplate with alloyed hellstone and evil metal armor plates");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            //TODO: find best values
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 5;
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

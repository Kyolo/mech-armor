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
            item.defense = 7;
        }

        // Set bonus in Helmet

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddTile(TileID.WorkBenches);
            r.AddRecipeGroup("Wood");
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}

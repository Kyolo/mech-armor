using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    class ArtilleryArmorLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A legging that help to steady your aim");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 11;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 5;
        }



        // Set bonus in PlateProtectedArmorHelmet.cs

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

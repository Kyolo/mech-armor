using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Legs)]
    class PostPTierRangedLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Ranger Leggings");
            Tooltip.SetDefault("10% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 5, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 15;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.rangedCrit += 5;
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
            regularRecipe.AddTile(TileID.MythrilAnvil);
            regularRecipe.AddIngredient(ItemID.ShroomiteBar, 15);
            regularRecipe.AddIngredient(ItemID.InvisibilityPotion, 7);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

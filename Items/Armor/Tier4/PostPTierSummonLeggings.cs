using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Legs)]
    class PostPTierSummonLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strange Wood Leggings");
            Tooltip.SetDefault("10% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 5, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 11;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
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
            regularRecipe.AddIngredient(ItemID.Cog, 20);
            regularRecipe.AddIngredient(ModContent.ItemType<StrangeWood>(), 5);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

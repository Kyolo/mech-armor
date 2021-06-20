using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Body)]
    class PostPTierSummonBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10% Critical Strike Chance");
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
            player.meleeCrit += 10;
            player.rangedCrit += 10;
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
            regularRecipe.AddTile(TileID.MythrilAnvil);
            regularRecipe.AddIngredient(ItemID.Cog, 60);
            regularRecipe.AddIngredient(ModContent.ItemType<StrangeWood>(), 5);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

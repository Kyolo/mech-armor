using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Body)]
    class PostPTierMagicBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shifting Spectral Breastplate");
            Tooltip.SetDefault("5% increased magical critical chance\n5% increased magical damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 7, 25, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicCrit += 5;
            player.magicDamage += 0.05f;
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
            regularRecipe.AddIngredient(ItemID.SpectreBar, 20);
            regularRecipe.AddIngredient(ItemID.CobaltShield, 1);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

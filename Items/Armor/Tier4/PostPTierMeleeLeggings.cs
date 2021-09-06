using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Legs)]
    class PostPTierMeleeLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Configurable Paladin Leggings");
            Tooltip.SetDefault("5% increased melee critical chance");
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
            player.meleeCrit += 5;
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
            regularRecipe.AddIngredient(ModContent.ItemType<PaladinArmorShard>(), 8);
            regularRecipe.AddIngredient(ItemID.ChlorophyteBar, 8);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using MechArmor.Items.Armor.Tier5.PreMoonLord;

namespace MechArmor.Items.Armor.Tier5.PostMoonLord
{
    [AutoloadEquip(EquipType.Body)]
    class VortexianMechaBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased ranged damage\n10% increased ranged critical chance\n-25% Ammo Consumption Chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 16, 25, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 26;
        }

        public override void UpdateEquip(Player player)
        {
            player.ammoCost75 = true;
            player.rangedDamage += 0.10f;
            player.rangedCrit += 10;
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
            regularRecipe.AddTile(TileID.LunarCraftingStation);
            regularRecipe.AddIngredient(ModContent.ItemType<ProtoVortexianMechaBreastplate>());
            regularRecipe.AddIngredient(ItemID.LunarBar, 16);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}
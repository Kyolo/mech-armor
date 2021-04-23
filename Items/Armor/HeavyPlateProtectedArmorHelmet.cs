using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class HeavyPlateProtectedArmorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A helmet with hellstone armor plates");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HeavyPlateProtectedArmorBreastplate>() && legs.type == ItemType<HeavyPlateProtectedArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Like the basic plate protected Armor, but heavier and the Hellstone allow you to light enemies on fire";
            player.magmaStone = true;

            // When health is high, we give a better defense
            if(player.statLife > player.statLifeMax2 *.20f)
            {
                player.setBonus += "\nDefensive Mode : You are protected and slowed down by heavy plates";
                player.statDefense += 11; //(6+7+6)*.6 = 11
                player.moveSpeed -= 0.25f;
            }
            else // Otherwise when health is low, we add move speed to better move away
            {
                player.setBonus += "\nFleeing Mode : You are faster but less protected";
                player.statDefense -= 10;
                player.moveSpeed += 0.5f;

                player.allDamage -= 0.1f;
            }
        }
        public override void AddRecipes()
        {
            // Check if we need to use the testing recipes
            if (ModContent.GetInstance<MechArmorServerConfig>().UseTestingRecipes)
            {
                ModRecipe recipeW = new ModRecipe(mod);
                recipeW.AddRecipeGroup("Wood", 20);
                recipeW.AddTile(TileID.WorkBenches);
                recipeW.SetResult(this);
                recipeW.AddRecipe();
            }

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ModContent.ItemType<PlateProtectedArmorHelmet>());
            recipe.AddIngredient(ItemID.HellstoneBar, 10);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

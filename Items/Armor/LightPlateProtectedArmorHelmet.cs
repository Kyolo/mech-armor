using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class LightPlateProtectedArmorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A helmet with light armor plates");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<LightPlateProtectedArmorBreastplate>() && legs.type == ItemType<LightPlateProtectedArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Functionnally identical to the basic Plate Protected Armor but the new plates allow for easier movement.";

            // When health is high, we give a better defense
            if(player.statLife > player.statLifeMax2 *.15)
            {
                player.setBonus += "\nDefensive Mode : You are protected and slowed down by heavy plates";
                player.statDefense += 6; //(5+6+5)*.4 = 8
                player.moveSpeed -= 0.1f;
            }
            else // Otherwise when health is low, we add move speed to better move away
            {
                player.setBonus += "\nFleeing Mode : You are faster but less protected";
                player.statDefense -= 6;
                player.moveSpeed += 0.6f;

                player.allDamage -= 0.05f;
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
            recipe.AddIngredient(ModContent.ItemType<PlateProtectedArmorLeggings>());
            recipe.AddRecipeGroup("MechArmor:Bars:Evil", 10);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

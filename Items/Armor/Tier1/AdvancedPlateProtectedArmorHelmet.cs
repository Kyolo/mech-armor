using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier1
{
    [AutoloadEquip(EquipType.Head)]
    class AdvancedPlateProtectedArmorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A helmet with alloyed hellstone and evilmetal armor plates");
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
            return body.type == ItemType<AdvancedPlateProtectedArmorBreastplate>() && legs.type == ItemType<AdvancedPlateProtectedArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Like the basic plate protected Armor, but heavier and the Hellstone allow you to light enemies on fire";
            player.magmaStone = true;

            // When health is high, we give a better defense
            if(player.statLife > player.statLifeMax2 *.25f)
            {
                player.setBonus += "\nDefense Mode :\n +50% Armor\n -15% Movement Speed";
                player.statDefense += 11; //(6+7+6)*.6 = 11
                player.moveSpeed -= 0.15f;
            }
            else // Otherwise when health is low, we add move speed to better move away
            {
                player.setBonus += "\nMovement Mode :\n -50% Armor\n +60% Movement Speed\n -10% Damage";
                player.statDefense -= 10;
                player.moveSpeed += 0.6f;

                player.allDamage -= 0.1f;
            }
        }
        public override void AddRecipes()
        {
            // Check if we need to use the testing recipes
            if (ModContent.GetInstance<MechArmorServerConfig>().UseTestingRecipes)
            {
                ModRecipe wRecipe = new ModRecipe(mod);
                wRecipe.AddRecipeGroup("Wood", 10);
                wRecipe.AddTile(TileID.WorkBenches);
                wRecipe.SetResult(this);
                wRecipe.AddRecipe();
            }

            //Upgrade from heavy
            ModRecipe recipeUpgradeHeavy = new ModRecipe(mod);
            recipeUpgradeHeavy.AddTile(TileID.Anvils);
            recipeUpgradeHeavy.AddIngredient(ModContent.ItemType<HeavyPlateProtectedArmorHelmet>());
            recipeUpgradeHeavy.AddRecipeGroup("MechArmor:Bars:Evil", 10);
            recipeUpgradeHeavy.AddIngredient(ItemID.Wire, 10);

            recipeUpgradeHeavy.SetResult(this);
            recipeUpgradeHeavy.AddRecipe();

            //Upgrade from light
            ModRecipe recipeUpgradeLight = new ModRecipe(mod);
            recipeUpgradeLight.AddTile(TileID.Anvils);
            recipeUpgradeLight.AddIngredient(ModContent.ItemType<LightPlateProtectedArmorHelmet>());
            recipeUpgradeLight.AddIngredient(ItemID.HellstoneBar, 10);
            recipeUpgradeLight.AddIngredient(ItemID.Wire, 10);

            recipeUpgradeLight.SetResult(this);
            recipeUpgradeLight.AddRecipe();

            //Upgrade from basic
            ModRecipe recipeUpgradeBasic = new ModRecipe(mod);
            recipeUpgradeBasic.AddTile(TileID.Anvils);
            recipeUpgradeBasic.AddIngredient(ModContent.ItemType<PlateProtectedArmorHelmet>());
            recipeUpgradeBasic.AddIngredient(ItemID.HellstoneBar, 10);
            recipeUpgradeBasic.AddRecipeGroup("MechArmor:Bars:Evil", 10);
            recipeUpgradeBasic.AddIngredient(ItemID.Wire, 10);

            recipeUpgradeBasic.SetResult(this);
            recipeUpgradeBasic.AddRecipe();

        }
    }
}

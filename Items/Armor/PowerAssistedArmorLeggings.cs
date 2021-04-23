﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    class PowerAssistedArmorLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A leggings with alloyed hellstone and evilmetal armor plates");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            //TODO: find best values
            item.value = 70;
            item.rare = ItemRarityID.Orange;
            item.defense = 11;
            
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
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
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT1", 15);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT2", 15);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT3", 15);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

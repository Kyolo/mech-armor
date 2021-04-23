using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    class ArtilleryArmorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A fragile helmet with advanced targeting systems");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 90;
            item.rare = ItemRarityID.Orange;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<ArtilleryArmorBreastplate>() && legs.type == ItemType<ArtilleryArmorLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "This armor systems allow you to either hit hard or flee fast";
            player.ammoCost80 = true;

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 15;
            armorPlayer.ArmorWarmupDuration = 2;
            // And then we allow the use of heavy guns
            armorPlayer.ArmorHeavyGun = true;

            switch(armorPlayer.ArmorState)
            {
                case 0:// Setup mode : allow movement to prepare for aiming
                    {
                        player.setBonus += "\nSetup Mode : Increased movement but slightly reduced damage";
                        player.rangedDamage -= 0.10f;
                        player.moveSpeed += 0.3f;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nFiring Mode : Almost immobilized but highly increased ranged damage";
                        // We wait for the warmup to finish
                        if(!armorPlayer.IsArmorOnWarmup) 
                            player.rangedDamage += 1.0f;//TODO : balance that

                        // Reduce speed by (at most) 1.0
                        player.moveSpeed -= player.moveSpeed > 1.0f ? 1.0f : player.moveSpeed;
                        // Immobilize the player in the air
                        //player.vortexDebuff = true;// didn't work as expected
                    }
                    break;
            }


        }

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
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT1", 10);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT2", 10);
            regularRecipe.AddRecipeGroup("MechArmor:Bars:HMT3", 10);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

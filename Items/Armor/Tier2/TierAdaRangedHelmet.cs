using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier2
{
    [AutoloadEquip(EquipType.Head)]
    class TierAdaRangedHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hard Metal Artillery Helmet");
            Tooltip.SetDefault("5% Ranged Critical Chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 3, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<TierAdaBreastplate>() && legs.type == ItemType<TierAdaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% Chance not to consume ammo";
            player.ammoCost80 = true;

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 15;
            armorPlayer.ArmorWarmupDuration = 2;
                       

            switch(armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nMovement Mode :\n 10% Increaed Ranged Damage\n 30% Increased Movement Speed";
                        player.rangedDamage -= 0.10f;
                        player.moveSpeed += 0.3f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Movement;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nOffense Mode :\n 50% Increased Ranged Damage\n 100% Reduced Move Speed";
                        // We wait for the warmup to finish
                        if(!armorPlayer.IsArmorOnWarmup)
                            player.rangedDamage += 0.5f;//TODO : balance that

                        // Reduce speed by (at most) 1.0
                        player.moveSpeed -= player.moveSpeed > 1.0f ? 1.0f : player.moveSpeed;
                        // Immobilize the player in the air
                        //player.vortexDebuff = true;// didn't work as expected
                        //TODO: check where I can remove the vertical speed
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
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
            regularRecipe.AddIngredient(ItemID.Lens, 10);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

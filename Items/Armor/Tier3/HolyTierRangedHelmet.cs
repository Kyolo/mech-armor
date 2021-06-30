using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier3
{
    [AutoloadEquip(EquipType.Head)]
    class HolyTierRangedHelmet : ModItem
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
            item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedCrit += 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HolyTierBreastplate>() && legs.type == ItemType<HolyTierLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "WIP";
            player.ammoCost80 = true;

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 3;

            // And then we allow the use of heavy guns


            switch(armorPlayer.ArmorState)
            {
                case 0:// Setup mode : allow movement to prepare for aiming
                    {
                        player.setBonus += "\nOffense Mode :\n +20% Ranged Damage\n -20% Movement Speed";
                        player.rangedDamage += 0.20f;
                        player.moveSpeed -= 0.2f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nDefense Mode :\n +20% Movement Speed\n +20% Armor";

                        player.moveSpeed += 0.20f;
                        player.statDefense += 7; // (9 + 15 +11) * 0.2 = 17
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    }
                    break;
                case 2:
                    player.setBonus += "\nMovement Mode :\n +20% Movement Speed\n +25% Wing Time";
                    player.moveSpeed += 0.20f;
                    player.wingTimeMax = (int)((float)player.wingTimeMax * 1.25f);
                    armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Movement;
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
            regularRecipe.AddIngredient(ItemID.HallowedBar, 10);
            regularRecipe.AddIngredient(ItemID.SoulofSight, 5);
            regularRecipe.AddIngredient(ItemID.BlackLens, 2);
            regularRecipe.AddIngredient(ItemID.DarkShard, 2);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier3
{
    [AutoloadEquip(EquipType.Head)]
    class HolyTierSummonHelmet : ModItem
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
            item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.10f;
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HolyTierBreastplate>() && legs.type == ItemType<HolyTierLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "This armor systems allow you to either hit hard or flee fast";
            player.maxMinions += 1;

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 3;

            // And then we allow the use of heavy guns
            //armorPlayer.ArmorHeavyGun = true; //removed for the moment

            switch(armorPlayer.ArmorState)
            {
                case 0:// Setup mode : allow movement to prepare for aiming
                    {
                        player.setBonus += "\nOffensive Mode :\n +10% Minion Damage\n +1 Minion Slot";
                        player.minionDamage += 0.10f;
                        player.maxMinions += 1;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nDefense Mode :\n +2 Armor per minion";//\n +5% Damage Resistance per sentry";
                        player.statDefense += 2 * player.numMinions;
                        //player.endurance += 0.5f * player.

                    }
                    break;
                case 2:
                    player.setBonus += "\nMovement Mode :\n +20% Movement Speed\n +25% Wing Time";
                    player.moveSpeed += 0.20f;
                    player.wingTimeMax = (int)(player.wingTimeMax * 1.25f);
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
            regularRecipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 4);
            regularRecipe.AddIngredient(ItemID.DarkShard, 1);
            regularRecipe.AddIngredient(ItemID.LightShard, 1);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

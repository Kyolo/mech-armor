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
            DisplayName.SetDefault("Holy Invocator Helmet");
            Tooltip.SetDefault("Increase your maximum minion count by 1");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 5, 0, 0);
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
            player.setBonus = "Increase your minion count by an additional 1";
            player.maxMinions += 1;

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 3;

            switch(armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nOffensive Mode :\n 10% increased minion damage\n 1 additional minion slot";
                        player.minionDamage += 0.10f;
                        player.maxMinions += 1;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nDefense Mode :\n Armor increased by 2 per minion";//\n +5% Damage Resistance per sentry";
                        player.statDefense += 2 * player.numMinions;
                        //player.endurance += 0.5f * player.
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;

                    }
                    break;
                case 2:
                    player.setBonus += "\nMovement Mode :\n 20% increased movement speed\n 25% increased wing time";
                    player.moveSpeed += 0.20f;
                    player.wingTimeMax = (int)(player.wingTimeMax * 1.25f);
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
            regularRecipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 4);
            regularRecipe.AddIngredient(ItemID.DarkShard, 1);
            regularRecipe.AddIngredient(ItemID.LightShard, 1);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

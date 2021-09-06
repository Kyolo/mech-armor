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
            DisplayName.SetDefault("Holy Ranger Helmet");
            Tooltip.SetDefault("10% Increased ranged critical chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 5, 0, 0);
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
            player.setBonus = "20% Reduced Ammo Consumption chance";
            player.ammoCost80 = true;

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 3;


            switch(armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nOffense Mode :\n 20% increased ranged damage\n 20% reduced movement speed";
                        player.rangedDamage += 0.20f;
                        player.moveSpeed -= 0.2f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nDefense Mode :\n 20% increased movement speed\n 20% additional armor";

                        player.moveSpeed += 0.20f;
                        player.statDefense += 7; // (9 + 15 +11) * 0.2 = 17
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    }
                    break;
                case 2:
                    player.setBonus += "\nMovement Mode :\n 20% increased movement speed\n 25% increased wing time";
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

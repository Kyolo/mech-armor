using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Head)]
    class PostPTierRangedHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Ranger Helmet");
            Tooltip.SetDefault("15% increased ranged damage\n5% increased ranged critical chance");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(0, 8, 50, 0);
            item.rare = ItemRarityID.Orange;
            item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.15f;
            player.rangedCrit += 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PostPTierRangedBreastplate>() && legs.type == ItemType<PostPTierRangedLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% Chance not to consume ammo";
            player.ammoCost80 = true;

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 2;


            switch(armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nOffense Mode :\n 25% reduced ranged UseTime\n Scope Effect";
                        armorPlayer.RangedUseTimeModifier -= 0.25f;
                        player.scope = true;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\nDefensive Utility Mode :\n 25% increased movement speed\n Invisibility and reduced enemy aggression\n 50% reduced ranged damage";

                        player.moveSpeed += 0.25f;
                        player.rangedDamage -= 0.5f;
                        player.invis = true;
                        player.aggro -= 400;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense | (byte)EnumArmorStateType.Utility;
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
            regularRecipe.AddIngredient(ItemID.ShroomiteBar, 10);
            regularRecipe.AddIngredient(ItemID.SniperScope);
            regularRecipe.SetResult(this);
            regularRecipe.AddRecipe();
        }
    }
}

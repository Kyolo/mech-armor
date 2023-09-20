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

        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 8, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 10;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.GetCritChance(DamageClass.Ranged) += 5;
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
                        player.GetDamage(DamageClass.Ranged) -= 0.5f;
                        player.invis = true;
                        player.aggro -= 400;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense | (byte)EnumArmorStateType.Utility;
                    }
                    break;
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.ShroomiteBar, 10)
            .AddIngredient(ItemID.SniperScope)
            .Register();
        }
    }
}

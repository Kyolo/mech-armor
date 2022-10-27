using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier3
{
    [AutoloadEquip(EquipType.Head)]
    class HolyTierMagicHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Mage Helmet");
            Tooltip.SetDefault("Increased maximum mana by 100\n10% increased magical damage");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 100;
            player.GetDamage(DamageClass.Magic) += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<HolyTierBreastplate>() && legs.type == ItemType<HolyTierLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 5;

            switch (armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "Offense Mode :\n 20% increased magic damage\n 10% reduced mana cost";
                        player.manaCost -= 0.20f;
                        player.manaRegenBonus += 50;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    player.setBonus += "Defense Mode :\n 20% additional armor\n 10% magical damage absorption";
                    player.statDefense += 6; // (5 + 15 +11) * 0.2 = 6
                    armorPlayer.MagicDamageAbsorption = true;
                    armorPlayer.MagicDamageAbsorptionAmount = 0.10f;
                    armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    
                    break;
                case 2:
                    player.setBonus += "Movement Mode :\n 20% increased movement speed\n 25% increased wing time";
                    player.moveSpeed += 0.20f;
                    player.wingTimeMax = (int)((float)player.wingTimeMax * 1.25f);
                    armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Movement;
                    break;
            }


        }

        public override void AddRecipes()
        {

            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddIngredient(ItemID.PixieDust, 20)
            .AddIngredient(ItemID.LightShard, 2)
            .Register();

        }
    }
}

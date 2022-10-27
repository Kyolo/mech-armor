using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier4
{
    [AutoloadEquip(EquipType.Head)]
    class PostPTierMagicHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shifting Spectral Hood");
            Tooltip.SetDefault("Increase maximum mana by 100\n10% increased magical damage\n10% increased magical critical chance");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 8, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 100;
            player.GetDamage(DamageClass.Magic) += 0.10f;
            player.GetCritChance(DamageClass.Magic) += 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<PostPTierMagicBreastplate>() && legs.type == ItemType<PostPTierMagicLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(2);
            armorPlayer.ArmorCooldownDuration = 10;


            switch(armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "Defense Mode :\n 15% Magic Damage Absorption\n 10% reduced magic UseTime";
                        armorPlayer.MagicDamageAbsorption = true;
                        armorPlayer.MagicDamageAbsorptionAmount += 0.15f;
                        armorPlayer.MagicUseTimeModifier -= 0.10f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "Offense Mode :\n Killing an enemy release a Healing orb (WIP)\n 50% increased mana cost\n 10% reduced movement speed";
                        player.ghostHeal = true;
                        player.manaCost += 0.50f;
                        player.moveSpeed -= 0.10f;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.MythrilAnvil)
            .AddIngredient(ItemID.SpectreBar, 10)
            .AddIngredient(ItemID.Diamond, 2)
            .Register();
        }
    }
}

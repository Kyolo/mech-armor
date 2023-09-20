using MechArmor.Items.Armor.Tier5.PreMoonLord;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MechArmor.Items.Armor.Tier5.PostMoonLord
{
    [AutoloadEquip(EquipType.Head)]
    class NebularMechaHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
/* 
Removed because of localization update
            Tooltip.SetDefault("Increase maximum mana by 200\n10% reduced mana cost\n5% increased magical critical chance");
*/
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(0, 8, 75, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 200;
            player.manaCost -= 0.10f;
            player.GetCritChance(DamageClass.Magic) += 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<NebularMechaBreastplate>() && legs.type == ItemType<NebularMechaLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Lunar Drones assists you";

            MechArmorPlayer armorPlayer = player.GetModPlayer<MechArmorPlayer>();
            // We need to indicate the maximum number of states for this armor
            armorPlayer.SetMaxArmorStates(3);
            armorPlayer.ArmorCooldownDuration = 2;

            // We add the lunar drones for the player
            armorPlayer.LunarDroneCount = 8;


            switch (armorPlayer.ArmorState)
            {
                case 0:
                    {
                        player.setBonus += "\nMana Amplifier :\n100% increased magical damage\n200% increased mana cost";
                        player.GetDamage(DamageClass.Magic) += 1.0f;
                        player.manaCost += 2.0f;
                        // Because we use lunar drones, we need to tell them how to behave
                        armorPlayer.LunarDroneMode = LunarDroneModes.ManaAmplifier;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Offense;
                    }
                    break;
                case 1:
                    {
                        player.setBonus += "\n15% Lifesteal with magical weapon\n50% decreased magical damage";
                        player.GetDamage(DamageClass.Magic) -= .5f;
                        armorPlayer.MagicalLifeSteal = true;
                        armorPlayer.MagicalLifeStealAmount = 0.15f;
                        //TODO: healing pulse on drinking mana potion
                        armorPlayer.LunarDroneMode = LunarDroneModes.ManaLifeSteal;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    }
                    break;
                case 2:
                    {
                        player.setBonus += "\nMana Shield :\n10% increased damage reduction\n10% Magical Damage Absorption";
                        player.endurance += 0.10f;
                        armorPlayer.MagicDamageAbsorption = true;
                        armorPlayer.MagicDamageAbsorptionAmount += 0.10f;

                        armorPlayer.LunarDroneMode = LunarDroneModes.ManaShield;
                        armorPlayer.ArmorStateType = (byte)EnumArmorStateType.Defense;
                    }
                    break;
            }


        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddTile(TileID.LunarCraftingStation)
            .AddIngredient(ModContent.ItemType<ProtoNebularMechaHelmet>())
            .AddIngredient(ItemID.LunarBar, 12)
            .Register();
        }
    }
}
